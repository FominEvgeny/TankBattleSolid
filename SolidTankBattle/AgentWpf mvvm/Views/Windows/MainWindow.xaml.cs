using System.Windows;
using System.Windows.Input;
using AgentWpf_mvvm.ViewModels;

namespace AgentWpf_mvvm.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;
        }

        private void UIElement_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _vm.OnMoveUpCommandExecutedCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _vm.OnStopMoveCommandExecutedCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseUp_clockwise(object sender, MouseButtonEventArgs e)
        {
            _vm.StopRotateClockwiseCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseDown_clockwise(object sender, MouseButtonEventArgs e)
        {
            
            _vm.RotateClockwiseCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseUp_counterclockwise(object sender, MouseButtonEventArgs e)
        {
            _vm.StopRotateCounterClockwiseCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseDown_counterclockwise(object sender, MouseButtonEventArgs e)
        {
            _vm.RotateCounterClockwiseCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseDown_Shoot(object sender, MouseButtonEventArgs e)
        {
            _vm.ShootCommand.Execute(null);
        }
    }
}
