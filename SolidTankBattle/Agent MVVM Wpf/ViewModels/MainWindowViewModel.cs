using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using Agent_MVVM_Wpf.DTO;
using Agent_MVVM_Wpf.Infrastructure.Commands.Base;
using Agent_MVVM_Wpf.ViewModels.Base;

namespace Agent_MVVM_Wpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Constructor

        public MainWindowViewModel()
        {
            #region Commands

            #region StopMoveUpCommand in Constructors

            StopMoveUpCommand = new LambdaCommand(OnStopMoveUpCommandExecuted, CanStopMoveUpCommandExecute);

            #endregion

            #region OnMoveUpCommandExecutedCommand in Constructors

            OnMoveUpCommandExecutedCommand = new LambdaCommand(OnOnMoveUpCommandExecutedCommandExecuted, CanOnMoveUpCommandExecutedCommandExecute);

            #endregion

            #region CloseApplicationCommand

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion

            #region ShowMessageHelloCommand in Constructors

            ShowMessageHelloCommand = new LambdaCommand(OnShowMessageHelloCommandExecuted, CanShowMessageHelloCommandExecute);

            #endregion

            #endregion

            #region Custom Code

             

            #endregion
        }

        #endregion

        #region Notify Properties

        //private AgentService _eventBus = new AgentService();

        #region Title

        private string _Title = "Пульт дистанционного управления танком";

        /// <summary> Title - свойство </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status
        /// <summary> Status - свойство </summary>
        private string _Status = "Я готов!";

        

        /// <summary> Status - свойство </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion  

        #endregion

        #region Normal Properties

        public string Field { get; set; }

        #endregion

        #region Commands

        #region StopMoveUpCommand

        public ICommand StopMoveUpCommand { get; }

        private bool CanStopMoveUpCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnStopMoveUpCommandExecuted(object p)
        {
            var a = 1;
        }

        #endregion

        #region OnMoveUpCommandExecutedCommand

        public ICommand OnMoveUpCommandExecutedCommand { get; }

        private bool CanOnMoveUpCommandExecutedCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnOnMoveUpCommandExecutedCommandExecuted(object p)
        {
            //_eventBus.Publish("move", new Message { Direction = "up", Action = "StartMove", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion
        
        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region ShowMessageHelloCommand

        public ICommand ShowMessageHelloCommand { get; }

        private bool CanShowMessageHelloCommandExecute(object p) => true;

        private void OnShowMessageHelloCommandExecuted(object p)
        {
            MessageBox.Show("Hello, Jo!");
        }

        #endregion

        #endregion
    }
}
