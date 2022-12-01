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
    /// Interaction logic for sales_inquiry.xaml
    /// </summary>
    public partial class sales_inquiry : Window
    {
        public sales_inquiry()
        {
            InitializeComponent();
        }
        BackgroundWorker bgSrch1;
        ObservableCollection<classes.binding> qSearch1;
        sales_inquiry si;
        loading l;
        string sq;
        public string holder;
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {

            qSearch1 = new ObservableCollection<classes.binding>();
            //dgrid_srch.ItemsSource = new ObservableCollection<classes.binding>
            if (search.Text.ToString() == "" || search.Text.ToString() == null)
            {
                dgrid_srch.ItemsSource = null;
                dgrid_srch.Items.Clear();
                sq = "SELECT DISTINCT company_name, UPPER(franchise_status) as `status` "
                        + "FROM sales_view "                       
                        + "where company_name like '" + search.Text.ToString() + "%'"
                        + "order by company_name limit 0 ";
            } 
            else
            {
                sq = "SELECT DISTINCT company_name, UPPER(franchise_status) as `status` "
                        + "FROM sales_view "
                        + "where company_name like '" + search.Text.ToString() + "%'"
                        + "order by company_name";
            }
                
            //}
            //else
            //{
            //    sq = "SELECT DISTINCT c.company_name, UPPER(s.franchise_status) as `status` "
            //                            + "FROM franchise_client c "
            //                            + "INNER JOIN ref_franchise_status s ON(c.code = s.code) "
            //                            + "where c.company_name like '%" + search.Text.ToString() + "%'"
            //                            + "order by c.company_name  limit 50";
            //}

            bgSrch1 = new BackgroundWorker();
            //bgSrch1.CancelAsync();
            bgSrch1.WorkerReportsProgress = true;
            bgSrch1.WorkerSupportsCancellation = true;
            bgSrch1.DoWork += bgSrch_DoWork1;          
            bgSrch1.RunWorkerCompleted += bgSrch_RunWorkerCompleted1;
            bgSrch1.RunWorkerAsync();
            
        }
        DataTable dtverf1;
        private void bgSrch_DoWork1(object sender, DoWorkEventArgs e)
        {
           
            qSearch1 = new ObservableCollection<classes.binding>();
            classes.binding verf1 = new classes.binding();
            classes.getData gSearch1 = new classes.getData();

            dtverf1 = new DataTable();
            dtverf1 = gSearch1.getdatasource(sq);
            int i = 1;
            foreach (DataRow ctr in dtverf1.Rows)
            {
               
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                             new Action(() =>
                                             {
                                                 qSearch1.Add(new classes.binding()
                                                 {
                                                     _company_name = ctr["company_name"].ToString(),
                                                     _fstatus = ctr["status"].ToString()
                                                 });
                                             try
                                             {
                                                 bgSrch1.ReportProgress(i, qSearch1);
                                             }
                                              catch (Exception err) { }
                                             i++;
                                                 //System.Threading.Thread.Sleep(2);
                                             }));
               
            }

        }

        private void bgSrch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //l.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / dtverf.Rows.Count) * 100) + "%";
        }

        private void bgSrch_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            dgrid_srch.ItemsSource = qSearch1;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            search.Focus();
        }
    }
}
