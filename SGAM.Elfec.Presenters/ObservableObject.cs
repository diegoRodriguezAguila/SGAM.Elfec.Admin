using System.ComponentModel;

namespace SGAM.Elfec.Presenters
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

        #endregion
    }
}
