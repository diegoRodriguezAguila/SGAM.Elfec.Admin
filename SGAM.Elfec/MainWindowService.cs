using SGAM.Elfec.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    public class MainWindowService
    {
        private MainWindowService()
        {
            Navigation = new Stack<Control>();
        }
        private static MainWindowService _instance;
        public static MainWindowService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainWindowService();
                return _instance;
            }
        }

        public IMainWindowView MainWindowView { get; set; }

        public Control CurrentView
        {
            get
            {
                if (Navigation.Count > 0)
                    return Navigation.Peek();
                return null;
            }
        }

        public Stack<Control> Navigation { get; set; }
    }
}
