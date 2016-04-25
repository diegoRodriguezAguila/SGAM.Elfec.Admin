using System;
using System.Windows;
namespace SGAM.Elfec.Services.Dialogs
{
    public class ConfirmationDialog : DialogWindow
    {
        private ConfirmationDialogContent _content;

        public string Message { get { return _content.Message; } set { _content.Message = value; } }
        public UIElement ConfirmationContent
        {
            get { return _content.ConfirmationContent; }
            set { _content.ConfirmationContent = value; }
        }

        public ConfirmationDialog() : base()
        {
            Loaded +=InitializeConfrimationContent;
            _content = new ConfirmationDialogContent();
            ResizeMode = ResizeMode.NoResize;
            Closed += (s, e) =>
            {
                if ((bool)DialogResult)
                    OnConfirmed(this);
                else OnCanceled(this);
            };
        }

        private void InitializeConfrimationContent(object sender, RoutedEventArgs arg)
        {            
            DialogContent = _content;
            _content.BtnCancel.Click += (s, e) => { Close(); };
            _content.BtnOk.Click += (s, e) =>
            {
                DialogResult = true;
            };
        }

        #region Events
        #region Confirmed
        public static readonly RoutedEvent ConfirmedEvent =
                EventManager.RegisterRoutedEvent(
        "Confirmed",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(ConfirmationDialog));

        /// <summary>
        /// Evento que se ejecuta cuando se confirmó la acción
        /// </summary>
        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        private void OnConfirmed(object sender)
        {
            RaiseEvent(new RoutedEventArgs(ConfirmedEvent, sender));
        }
        #endregion
        #region Canceled
        public static readonly RoutedEvent CanceledEvent =
                EventManager.RegisterRoutedEvent(
        "Canceled",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(ConfirmationDialog));

        /// <summary>
        /// Evento que se ejecuta cuando se confirmó la acción
        /// </summary>
        public event RoutedEventHandler Canceled
        {
            add { AddHandler(CanceledEvent, value); }
            remove { RemoveHandler(CanceledEvent, value); }
        }

        private void OnCanceled(object sender)
        {
            RaiseEvent(new RoutedEventArgs(CanceledEvent, sender));
        }
        #endregion
        #endregion
    }
}
