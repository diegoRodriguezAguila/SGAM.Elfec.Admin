using System.Windows.Data;

namespace SGAM.Elfec.Utils
{
    public class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Source = Properties.WindowSettings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
}
