using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for splash.xaml
    /// </summary>
    public partial class splash : Window
    {
        public splash()
        {
            InitializeComponent();
            
        }
        MainWindow mw = new MainWindow();
        sales_homepage sh;
        public string userlevel;
        public BackgroundWorker bgworker;   
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            srchng();
            bgworker = new BackgroundWorker();
            bgworker.DoWork += bgworker_DoWork;
            bgworker.ProgressChanged += bgworker_ProgressChanged;
            bgworker.RunWorkerCompleted += bgworker_RunWorkerCompleted;
            bgworker.WorkerReportsProgress = true;
            bgworker.RunWorkerAsync();           
        }
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i <= 100; i++)
            {
                int x = RandomNumber(1, 2);
                bgworker.ReportProgress(i);
                i += x;
                System.Threading.Thread.Sleep(80);
            }
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //dgrid.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
            pb.Value = e.ProgressPercentage; 
            pb.Foreground = Brushes.Red;
            loading.Content = e.ProgressPercentage.ToString() + "%";
        }

        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            pb.Value = 100;
            System.Threading.Thread.Sleep(1000);
            //if(userlevel == "1" || userlevel == "A")
            //{
                mw.Show();
            //}
            //else
            //{
            //    sh.Show();
            //}
                    
            this.Close();
        }

        //** AUTO COMPLETE BOX FOR FRANCHISEE **//
        public string sql;
        //public BackgroundWorker bgworker;
        public BackgroundWorker bg;
        public void srchng()
        {
           
            sql = "Select CONCAT(sales,' (',producer,')' ) As `saless`,agent_code from ewhc_underwriting.agent_broker";
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            //bg.ProgressChanged += bg_ProgressChanged;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }
       
        public DataTable zxc;
        public List<classes.binding> cname = new List<classes.binding>();
        public void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            classes.getData gdata = new classes.getData();
            DataTable lbData;
            lbData = gdata.getdatasource(sql);

            foreach (DataRow ctr in lbData.Rows)
            {
               cname.Add(new classes.binding { _sales = ctr["saless"].ToString(), 
                   _agent_code = ctr["agent_code"].ToString() });
            }
        }
      
        
        public void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {                  
            mw = new MainWindow();
            //sh = new sales_homepage();
            mw.acb.ItemsSource = cname;
            //sh.ac = cname;
        }

    }
}
