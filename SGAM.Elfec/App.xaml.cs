using System.Windows;
using System.Windows.Media.Animation;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
             typeof(Timeline),
              new FrameworkPropertyMetadata { DefaultValue = 30 }
                );
            var instance = HighlighterManager.Instance;
            Exit += (s, e) => { SGAM.Elfec.Properties.WindowSettings.Default.Save(); };
        }
    }
}
