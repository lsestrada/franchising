using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for notifbox.xaml
    /// </summary>
    /// 
    public partial class notifbox : Window
    {
        public notifbox()
        {
            InitializeComponent();
        }
       
        public BackgroundWorker bgworker;
        public int xnum;
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            bgworker = new BackgroundWorker();
            bgworker.DoWork += bgworker_DoWork;
            bgworker.ProgressChanged += bgworker_ProgressChanged;
            bgworker.RunWorkerCompleted += bgworker_RunWorkerCompleted;
            bgworker.WorkerReportsProgress = true;
            bgworker.RunWorkerAsync();            
        }    
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            xnum = 1042;
            for (int z = 1; z <= 50;z++)
            {
                bgworker.ReportProgress(xnum);
                System.Threading.Thread.Sleep(15);
                xnum += 20;
            }
        }
        private void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Left = xnum;
        }
        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
          
            this.Close();
        }
    }
}
