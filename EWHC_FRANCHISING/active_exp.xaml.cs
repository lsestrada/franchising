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
    /// Interaction logic for active_exp.xaml
    /// </summary>
    public partial class active_exp : Window
    {
        public active_exp()
        {
            InitializeComponent();
        }

        BackgroundWorker bgSrch1;
        ObservableCollection<classes.binding> qSearch1;
        sales_inquiry si;
        loading l;
        string sq;
        public string holder;
        string cb = " > CURDATE() ";
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            qSearch1 = new ObservableCollection<classes.binding>();
            //dgrid_srch.ItemsSource = new ObservableCollection<classes.binding>
            if (search.Text.ToString() == "" || search.Text.ToString() == null)
            {
                dgrid_srch.ItemsSource = null;
                dgrid_srch.Items.Clear();
                sq = "SELECT c.code, s.date_of_approval, s.expiry_date, h.contract_expiry , c.company_name, " +
                        " s.franchise_status , rf.status FROM franchise_client c INNER JOIN franchise_history h " +
                        " ON(c.code = h.franchise_key) INNER JOIN ref_franchise_status s ON(h.franchise_key = s.code) " +
                        " INNER JOIN ref_type_of_plan t ON(c.code = t.code) LEFT JOIN ref_fstatus rf ON(s.franchise_status = rf.autocode) " +
                        " WHERE s.expiry_date " + cb +
                        " order BY s.expiry_date desc ";
            }
            else
            {
                sq = "SELECT c.code, s.date_of_approval, s.expiry_date, h.contract_expiry , c.company_name, " +
                       "  rf.status FROM franchise_client c INNER JOIN franchise_history h " +
                       " ON(c.code = h.franchise_key) INNER JOIN ref_franchise_status s ON(h.franchise_key = s.code) " +
                       " INNER JOIN ref_type_of_plan t ON(c.code = t.code) LEFT JOIN ref_fstatus rf ON(s.franchise_status = rf.autocode) " +
                       " WHERE s.expiry_date " + cb +
                       " AND c.company_name LIKE '" + search.Text.ToString() + "%' order BY s.expiry_date desc ";
            }


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
                                                 _fstatus = ctr["status"].ToString(),
                                                 _expiry_date = (ctr["expiry_date"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["expiry_date"].ToString())),
                                                 _approval_date = (ctr["date_of_approval"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["date_of_approval"].ToString())),
                                                 _contract_expiry = (ctr["contract_expiry"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["contract_expiry"].ToString())),
                                                 _code = nullValues(ctr["code"].ToString()),
                                                 
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

        private void bgSrch_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            dgrid_srch.ItemsSource = qSearch1;

        }

        public string nullValues(string Value)
        {
            string val;
            if (Value == "")
            {
                val = "";
            }
            else
            {
                val = Value.ToString();
            }
            return val;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            search.Focus();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox.SelectedIndex == 1)
            {
                cb = " < CURDATE() ";
                dgrid_srch.ItemsSource = null;
                search.Text = "";
            }
            else
            {
                cb = " > CURDATE() ";
                dgrid_srch.ItemsSource = null;
                search.Text = "";
            }
        }
    }
}
