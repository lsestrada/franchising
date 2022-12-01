using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for sales_homepage.xaml
    /// </summary>
    public partial class sales_homepage : Window
    {
        public sales_homepage()
        {
            InitializeComponent();
        }
        sales_fran sf;
        #region button mouse leave Effects

        private void addmenu_MouseEnter(object sender, MouseEventArgs e)
        {
            addmenu.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            addmenu.Foreground = Brushes.Blue;
        }

        private void addmenu_MouseLeave(object sender, MouseEventArgs e)
        {
            addmenu.Background = null;
            addmenu.Foreground = Brushes.Black;
        }

        private void addmenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void addmenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sf = new sales_fran();

            sf.Owner = this;
            
            sf.Show();

            sf.acb.SelectedItem = testname.Content.ToString();
            sf.acode.Content = testcode.Content.ToString();
            //sf.tb_actuarial.Text = sales.Content.ToString();

            //sf.ShowDialog();
            //sf.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //sf.ResizeMode = ResizeMode.NoResize;


        }
        #endregion

        #region img btn
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            addmenu_MouseEnter(sender, e);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            addmenu_MouseLeave(sender, e);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addmenu_MouseLeftButtonDown(sender, e);
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            addmenu_MouseLeftButtonUp(sender, e);
        }


        private void Image_MouseEnter1(object sender, MouseEventArgs e)
        {
            view_MouseEnter(sender, e);
        }

        private void Image_MouseLeave1(object sender, MouseEventArgs e)
        {
            view_MouseLeave(sender, e);
        }

        private void Image_MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            view_MouseLeftButtonDown(sender, e);
        }

        private void Image_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            view_MouseLeftButtonUp(sender, e);
        }



        #endregion



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }



        private void view_MouseEnter(object sender, MouseEventArgs e)
        {
            view.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            view.Foreground = Brushes.Blue;
        }

        private void view_MouseLeave(object sender, MouseEventArgs e)
        {
            view.Background = null;
            view.Foreground = Brushes.Black;
        }

        BackgroundWorker bgSrch;
        ObservableCollection<classes.binding> qSearch;
        sales_inquiry si;
        loading l;
        private void view_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bgSrch = new BackgroundWorker();
            bgSrch.WorkerReportsProgress = true;
            bgSrch.DoWork += bgSrch_DoWork;
            bgSrch.ProgressChanged += bgSrch_ProgressChanged;
            bgSrch.RunWorkerCompleted += bgSrch_RunWorkerCompleted;
            bgSrch.RunWorkerAsync();
             l = new loading();
           
            l.ShowDialog();
           
        }
        DataTable dtverf;
        classes.binding verf;
        classes.getData gSearch;
        private void bgSrch_DoWork(object sender, DoWorkEventArgs e)
        {
            qSearch = new ObservableCollection<classes.binding>();
            verf = new classes.binding();
            gSearch = new classes.getData();
            
            dtverf = new DataTable();
            dtverf = gSearch.getdatasource("SELECT DISTINCT c.company_name, UPPER(s.franchise_status) as `status` "
                        + "FROM franchise_client c "
                        + "INNER JOIN ref_franchise_status s ON(c.code = s.code)" 
                        + " order by c.company_name");
            int i = 1;
            foreach (DataRow ctr in dtverf.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               qSearch.Add(new classes.binding()
                               {
                                   _company_name = ctr["company_name"].ToString(),
                                   _fstatus = ctr["status"].ToString()
                               });
                               bgSrch.ReportProgress(i, qSearch);
                               i++;
                               //System.Threading.Thread.Sleep(2);
                           }));
            }

        }

        private void bgSrch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            l.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / dtverf.Rows.Count) * 100) + "%";
        }

        private void bgSrch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cls();
            si = new sales_inquiry();
            si.Show();
            //si.Owner = this;
            si.dgrid_srch.ItemsSource = qSearch;
        }
        public void cls()
        {
            l.Close();
        }
        private void view_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            view.Foreground = Brushes.Blue;
        }

        private void list_MouseEnter(object sender, MouseEventArgs e)
        {
            list.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            list.Foreground = Brushes.Blue;
        }

        private void list_MouseLeave(object sender, MouseEventArgs e)
        {
            list.Background = null;
            list.Foreground = Brushes.Black;
        }

        sales_view_req vr;
        private void list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vr = new sales_view_req();
            vr.meee.Content = testcode.Content.ToString();
            vr.Show();          
            vr.Owner = this;
        }

        private void list_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            list.Foreground = Brushes.Blue;
        }

        private void Image_MouseEnter_1(object sender, MouseEventArgs e)
        {
            list_MouseEnter(sender, e);
        }
      

        private void Image_MouseLeave_1(object sender, MouseEventArgs e)
        {
            list_MouseLeave(sender, e);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            list_MouseLeftButtonDown(sender, e);
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            list_MouseLeftButtonUp(sender, e);
        }
    }
}
