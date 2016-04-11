using SGAM.Elfec.Interfaces;
using SGAM.Elfec.Services;

namespace SGAM.Elfec
{
    public class MainWindowService
    {
        private MainWindowService()
        {
            Navigation = new NavigationService();
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

        public IMainWindow MainWindow { get; set; }

        public INavigationService Navigation { get; set; }
    }
}
