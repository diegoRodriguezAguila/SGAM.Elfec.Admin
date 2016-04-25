using System.Windows;

namespace SGAM.Elfec.Services.Dialogs
{
    public class InformationDialog : DialogWindow
    {
        private InformationDialogContent _content;

        public string Message { get { return _content.Message; } set { _content.Message = value; } }
        public IconType IconType { get { return _content.IconType; } set { _content.IconType = value; } }

        public InformationDialog() : base()
        {
            _content = new InformationDialogContent();
            Loaded += InitializeInfoContent;
            ResizeMode = ResizeMode.NoResize;
        }

        private void InitializeInfoContent(object sender, RoutedEventArgs arg)
        {
            DialogContent = _content;
            _content.BtnOk.Click += (s, e) => { Close(); };
        }

    }
}
