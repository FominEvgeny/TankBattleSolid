using System;
using System.ComponentModel;

namespace Agent_MVVM_Wpf.Infrastructure.Commands.Base
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> execute;

        private readonly Func<object, bool> canExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            this.execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            this.canExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => this.execute(parameter);

    }
}
