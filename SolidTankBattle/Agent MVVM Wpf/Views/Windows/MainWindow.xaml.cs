using System.Windows;
using System.Windows.Input;
using Agent_MVVM_Wpf.ViewModels;

namespace Agent_MVVM_Wpf
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
            _vm.StopMoveUpCommand.Execute(null);
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var b = 1;
        }
    }
}
