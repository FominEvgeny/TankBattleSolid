using System.Windows;
using System.Windows.Input;
using AgentWpf_mvvm.DTO;
using AgentWpf_mvvm.Infrastructure.Commands.Base;
using AgentWpf_mvvm.ViewModels.Base;

namespace AgentWpf_mvvm.ViewModels
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

            #region OnStopMoveCommandExecutedCommand in Constructors

            OnStopMoveCommandExecutedCommand = new LambdaCommand(OnOnStopMoveCommandExecutedCommandExecuted, CanOnStopMoveCommandExecutedCommandExecute);

            #endregion

            #region RotateClockwiseCommand in Constructors

            RotateClockwiseCommand = new LambdaCommand(OnRotateClockwiseCommandExecuted, CanRotateClockwiseCommandExecute);

            #endregion


            #region StopRotateClockwiseCommand in Constructors

            StopRotateClockwiseCommand = new LambdaCommand(OnStopRotateClockwiseCommandExecuted, CanStopRotateClockwiseCommandExecute);

            #endregion

            #region RotateCounterClockwiseCommand in Constructors

            RotateCounterClockwiseCommand = new LambdaCommand(OnRotateCounterClockwiseCommandExecuted, CanRotateCounterClockwiseCommandExecute);

            #endregion

            #region StopRotateCounterClockwiseCommand in Constructors

            StopRotateCounterClockwiseCommand = new LambdaCommand(OnStopRotateCounterClockwiseCommandExecuted, CanStopRotateCounterClockwiseCommandExecute);

            #endregion


            #region ShootCommand in Constructors

            ShootCommand = new LambdaCommand(OnShootCommandExecuted, CanShootCommandExecute);

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

        private AgentService _eventBus = new AgentService();

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
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StopMove", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region OnStopMoveCommandExecutedCommand    

        public ICommand OnStopMoveCommandExecutedCommand { get; }

        private bool CanOnStopMoveCommandExecutedCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnOnStopMoveCommandExecutedCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StartMove", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region RotateClockwiseCommand

        public ICommand RotateClockwiseCommand { get; }

        private bool CanRotateClockwiseCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnRotateClockwiseCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StartRotateClockwise", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region StopRotateClockwiseCommand

        public ICommand StopRotateClockwiseCommand { get; }

        private bool CanStopRotateClockwiseCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnStopRotateClockwiseCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StopRotateClockwise", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region RotateCounterClockwiseCommand

        public ICommand RotateCounterClockwiseCommand { get; }

        private bool CanRotateCounterClockwiseCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnRotateCounterClockwiseCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StartRotateCounterClockwise", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region StopRotateCounterClockwiseCommand

        public ICommand StopRotateCounterClockwiseCommand { get; }

        private bool CanStopRotateCounterClockwiseCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnStopRotateCounterClockwiseCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "StopRotateCounterClockwise", ObjectId = "1", TypeObject = "TankOneType" });
        }

        #endregion

        #region ShootCommand

        public ICommand ShootCommand { get; }

        private bool CanShootCommandExecute(object p) => true;
        //!ErrorForms.Values.Any(x => x != null);

        private void OnShootCommandExecuted(object p)
        {
            _eventBus.Publish("move", new Message { Direction = "up", Action = "ShootCommand", ObjectId = "1", TypeObject = "FireShoot" });
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
