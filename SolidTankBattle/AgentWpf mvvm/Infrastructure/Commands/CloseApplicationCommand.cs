using System.Windows;
using AgentWpf_mvvm.Infrastructure.Commands.Base;

namespace AgentWpf_mvvm.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
