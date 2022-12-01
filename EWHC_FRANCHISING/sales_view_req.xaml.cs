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
    /// Interaction logic for sales_view_req.xaml
    /// </summary>
    public partial class sales_view_req : Window
    {
        public sales_view_req()
        {
            InitializeComponent();
        }
        loading l;
        classes.getData gg;
        classes.binding bd;
        string sql, sqlmain;
        BackgroundWorker bg;
        ObservableCollection<classes.binding> q;
        List<classes.binding> x = new List<classes.binding>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sql = "Select * from ref_fstatus order by autocode";
            sqlmain = "Select * from sales_view where agent_code = '" + meee.Content.ToString() + "'";
            //MessageBox.Show(meee.Content.ToString());
            dosearch();
        }

        private void dosearch()
        {
            cnt.Content = "0";
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            bg.ProgressChanged += bg_ProgressChanged;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
            l = new loading();
            l.ShowDialog();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
           
            q = new ObservableCollection<classes.binding>();
            DataTable dt = new DataTable();
            DataTable dtmain = new DataTable();
            gg = new classes.getData();
            dt = gg.getdatasource(sql);
            x = new List<classes.binding>();
            foreach (DataRow ctr1 in dt.Rows)
            {
                x.Add(new classes.binding
                {
                    _fstatus = ctr1["status"].ToString()
                });
               

                }
           
            dtmain = gg.getdatasource(sqlmain);
            int i = 1;
            foreach (DataRow ctr in dtmain.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               q.Add(new classes.binding()
                               {
                                   _code = nullValues(ctr["code"].ToString()),
                                   _request_date = (ctr["date_request"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["date_request"].ToString())),
                                   _company_name = nullValues(ctr["company_name"].ToString()),
                                   _address = nullValues(ctr["address"].ToString()),
                                   _agent_code = nullValues(ctr["agent_code"].ToString()),
                                   _contact_person = nullValues(ctr["contact"].ToString()),
                                   _contact_person_number = nullValues(ctr["contact_no"].ToString()),
                                   _industry = nullValues(ctr["industry"].ToString()),
                                   _prins_count = Convert.ToInt32(nullValues(ctr["principal_count"].ToString())),
                                   _deps_count = Convert.ToInt32(nullValues(ctr["dependents_count"].ToString())),
                                   _type_of_plan = nullValues(ctr["type_of_plan"].ToString()),
                                   _fstatus = nullValues(ctr["franchise_status"].ToString()),
                                   _subgroup = nullValues(ctr["subgroup"].ToString()),
                                   _approval_date = (ctr["approval_date"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["approval_date"].ToString())),
                                   _expiry_date = (ctr["expiry_date"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["expiry_date"].ToString())),
                                   _approving_officer = ctr["approving_officer"].ToString(),
                                   _remarks = ctr["remarks"].ToString()
                               });

                                   //MessageBox.Show(bghandler);
                                   bg.ReportProgress(i, q);
                               i++;
                           }));
            }
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


        public void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dataGrid.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
            //CType(dgrid.ItemsSource, ObservableCollection(Of binding)).Add(CType(e.UserState, binding))
            //ls.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / alldata.Rows.Count) * 100) + "%";
            //count.Content = Convert.ToInt32(e.ProgressPercentage.ToString());
        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {    
            if(comboBox.Items.Count == 0)
            {
                comboBox.ItemsSource = x;
            }

            //comboBox.SelectedIndex = 0;
            cnt.Content = dataGrid.Items.Count.ToString();
            textBox.Focus();
            cls();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            //comboBox.ItemsSource = null;
            if (e.Key == Key.Enter)
            {
                if(textBox.Text.ToString() == null || textBox.Text.ToString() == "")
                {
                    MessageBox.Show("Please enter valid client name");
                    return;
                }
                //comboBox.Text = null;
                sqlmain = "Select * from sales_view where agent_code = '" + meee.Content.ToString() + "' AND company_name LIKE '" + textBox.Text.ToString() + "%'";
                //dataGrid.ItemsSource = new ObservableCollection<classes.binding>();
                dosearch();
            }
        }
        string getcb;
        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            //comboBox.ItemsSource = null;
            try
            {
                if (getcb == comboBox.Text.ToString())
                {
                    return;
                }
                if (textBox.Text.ToString() == "")
                {
                    sqlmain = "Select * from sales_view where agent_code = '" + meee.Content.ToString() + "' AND franchise_status = '" + comboBox.Text.ToString() + "'";
                    dataGrid.ItemsSource = new ObservableCollection<classes.binding>();
                    dosearch();
                }
                else
                {
                    sqlmain = "Select * from sales_view where  agent_code = '" + meee.Content.ToString() + "' AND franchise_status = '" + comboBox.Text.ToString() + "'  AND company_name LIKE '" + textBox.Text.ToString() + "%'";
                    dataGrid.ItemsSource = new ObservableCollection<classes.binding>();
                    dosearch();
                }
            }
            catch (Exception err) { }
        }

        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                getcb = comboBox.Text.ToString();
            }
            catch (Exception err) { }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Focus();
        }

        private void cls()
        {
            l.Close();
        }
        
    }
}
