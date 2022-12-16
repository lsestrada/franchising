using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Globalization;
using System.Threading;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void OnStartUp(object sender, StartupEventArgs e) {

            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(textbox_gotfocus));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.LostFocusEvent, new RoutedEventHandler(textbox_lostfocus));

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.LongDatePattern = "MMMM dd, yyyy HH:mm:ss";

            Thread.CurrentThread.CurrentCulture = ci;
        }

        private void textbox_gotfocus(object sender, RoutedEventArgs e) {
            TextBox tb;

            tb = (TextBox)sender;
            tb.Effect = new DropShadowEffect();
            tb.Background = Brushes.LemonChiffon;
            tb.Foreground = Brushes.Black;
            if (tb.Text.Length > 0) {
                tb.SelectAll();
            }
        }

        private void textbox_lostfocus(object sender, RoutedEventArgs e) {
            TextBox tb;
            tb = (TextBox)sender;
            tb.Effect = null;
            tb.Background = Brushes.White;
            tb.Foreground = Brushes.Gray;
        }
    }
}
