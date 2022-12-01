using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for admin_window.xaml
    /// </summary>
    public partial class admin_window : Window
    {
        public admin_window()
        {
            InitializeComponent();
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Do you want to close the application?", "Closing...", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
