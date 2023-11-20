using System.Windows;

using Agent_MVVM_Wpf.Infrastructure.Commands.Base;

namespace Agent_MVVM_Wpf.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
