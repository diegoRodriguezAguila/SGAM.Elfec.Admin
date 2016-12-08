using System.Windows;

namespace SGAM.Elfec.Interfaces
{
    public interface ISearchable
    {
        void OnRequestSearch(object sender, RoutedEventArgs routedEventArgs);
        void OnCancelSearch(object sender, RoutedEventArgs routedEventArgs);
    }
}