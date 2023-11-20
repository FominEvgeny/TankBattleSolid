using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LibraryClasses.GeneratorAdapters;

public class AdapterGenerator
{
    public string GenerateAdapter(Type @interface)
    {
        var adapterName = @interface.Name + "Adapter";

        var adapterClass = SyntaxFactory.ClassDeclaration(adapterName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("GeneratedCode")))))
            .WithBaseList(
                SyntaxFactory.BaseList(
                    SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                        SyntaxFactory.SimpleBaseType(
                            SyntaxFactory.IdentifierName(@interface.Name)))));

        var dictionaryField = SyntaxFactory.FieldDeclaration(
            SyntaxFactory.VariableDeclaration(
                SyntaxFactory.IdentifierName("Dictionary<string, object?>"))
            .WithVariables(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("_obj")))))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));

        adapterClass = adapterClass.AddMembers(dictionaryField);

        var methods = @interface.GetMethods();

        foreach (var method in methods)
        {
            var adapterMethod = GenerateAdapterMethod(method);
            adapterClass = adapterClass.AddMembers(adapterMethod);
        }

        var namespaceSyntax = SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.IdentifierName(@interface.Namespace))
            .AddMembers(adapterClass);

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddMembers(namespaceSyntax)
            .NormalizeWhitespace();

        return compilationUnit.ToFullString();
    }

    private MethodDeclarationSyntax GenerateAdapterMethod(MethodInfo methodInfo)
    {
        var returnType = methodInfo.ReturnType;
        var methodName = methodInfo.Name;
        var parameters = methodInfo.GetParameters();

        var adapterMethod = SyntaxFactory.MethodDeclaration(
            returnType == typeof(void) ? SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)) : SyntaxFactory.ParseTypeName(returnType.Name),
            SyntaxFactory.Identifier(methodName))
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList())
            .AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Override")))));

        if (parameters.Length > 0)
        {
            var methodParameters = parameters.Select(parameter =>
                SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                    .WithType(SyntaxFactory.ParseTypeName(parameter.ParameterType.Name)));

            var parameterList = SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(methodParameters));

            adapterMethod = adapterMethod.WithParameterList(parameterList);
        }

        var bodyStatements = new List<StatementSyntax>();

        if (returnType != typeof(void))
        {
            var returnStatement = SyntaxFactory.ReturnStatement(SyntaxFactory.ParseExpression($"((object) _obj)[\"{methodName}\"] as {returnType.Name}")!);
            bodyStatements.Add(returnStatement);
        }
        else
        {
            var assignmentExpression = SyntaxFactory.AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.ParseExpression($"((object) _obj)[\"{methodName}\"]"),
                SyntaxFactory.IdentifierName(parameters[0].Name));

            var expressionStatement = SyntaxFactory.ExpressionStatement(assignmentExpression);
            bodyStatements.Add(expressionStatement);
        }

        var body = SyntaxFactory.Block(bodyStatements);

        adapterMethod = adapterMethod.WithBody(body);

        return adapterMethod;
    }

    private static Type[] GetInterfaces(Assembly assembly)
    {
        return assembly.GetTypes().Where(t => t.IsInterface &&
                                              AllowedNamespaces.Any(n => t.Namespace.StartsWith(n))).ToArray();
    }

    private static readonly string[] AllowedNamespaces =
    {
        "Hw_2",
    };
}