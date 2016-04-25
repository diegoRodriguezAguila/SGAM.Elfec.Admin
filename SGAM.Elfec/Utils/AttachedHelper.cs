using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.Utils
{
    public class AttachedHelper
    {

        public static UIElement GetBindableChild(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(BindableChildProperty);
        }

        public static void SetBindableChild(DependencyObject obj, UIElement value)
        {
            obj.SetValue(BindableChildProperty, value);
        }

        public static readonly DependencyProperty BindableChildProperty =
            DependencyProperty.RegisterAttached("BindableChild", typeof(UIElement), typeof(AttachedHelper), new UIPropertyMetadata(null, BindableChildPropertyChanged));

        static void BindableChildPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ele = sender as Decorator;
            ele.Child = (UIElement)e.NewValue;
        }
    }
}
