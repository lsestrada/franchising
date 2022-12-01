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
    /// Interaction logic for view_request.xaml
    /// </summary>
    public partial class view_request : Window
    {
        public view_request()
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
            sqlmain = "Select * from franchise_request where franchise_status = 'PENDING'";
            dosearch();
        }

        private void dosearch()
        {
            dataGrid.ItemsSource = null;
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
            //MessageBox.Show(sqlmain);
            q = new ObservableCollection<classes.binding>();
          
            DataTable dtmain = new DataTable();
            gg = new classes.getData();          
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
                                   _franchisee = nullValues(ctr["franchisee"].ToString()),
                                   _contact_person = nullValues(ctr["contact"].ToString()),
                                   _contact_person_number = nullValues(ctr["contact_no"].ToString()),
                                   _designation = nullValues(ctr["designation"].ToString()),
                                   _industry = nullValues(ctr["industry"].ToString()),
                                   _fcontact_person_no = nullValues(ctr["franchisee_contact"].ToString()),
                                   _address = nullValues(ctr["address"].ToString()),
                                   _existing_provider = nullValues(ctr["existing_hmo"].ToString()),
                                   _years_with_provider = (ctr["contract_expiry"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["contract_expiry"].ToString())),
                                   _prins_count = Convert.ToInt32(nullValues(ctr["principal_count"].ToString())),
                                   _deps_count = Convert.ToInt32(nullValues(ctr["dependents_count"].ToString())),
                                   _employee_count = Convert.ToInt32(ctr["employee_count"].ToString()),
                                   _type_of_plan = nullValues(ctr["type_of_plan"].ToString()),
                                   _fstatus = nullValues(ctr["franchise_status"].ToString()),
                                   _subgroup = nullValues(ctr["subgroup"].ToString()),
                                   _approval_date = (ctr["approval_date"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["approval_date"].ToString())),
                                   _expiry_date = (ctr["expiry_date"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(nullValues(ctr["expiry_date"].ToString())),
                                   _approving_officer = nullValues(ctr["approving_officer"].ToString()),
                                   _remarks = nullValues(ctr["remarks"].ToString()),
                                   _sales = nullValues(ctr["agent_code"].ToString())
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
            //comboBox.ItemsSource = x;
            //comboBox.SelectedIndex = 0;
            tb1.Focus();
            cls();
        }
        // mw;
        private classes.binding Selectedsale, Selecteddate, selectedActuarial, selectedStatus;

       

    private string xx;
    private string yy;
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            { 
                if(tb1.Text.ToString() == "")
                {
                    xx = "";
                }
                else
                {
                    xx = " AND company_name LIKE '" + tb1.Text.ToString() + "%' ";
                }
                filterThis();  
            }
        }

        private void tb1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tb1.Text.ToString() == "")
            {
                xx = "";
            }
            else
            {
                xx = " AND company_name LIKE '" + tb1.Text.ToString() + "%' ";
            }
        }

        private void tb1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb1.Text.ToString() == "")
            {
                xx = "";
            }
            else
            {
                xx = " AND company_name LIKE '" + tb1.Text.ToString() + "%' ";
            }
        }



        private void tb2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb2.Text.ToString() == "")
            {
                yy = "";
            }
            else
            {
                yy = " AND franchisee LIKE '" + tb2.Text.ToString() + "%'";
            }
        }

        private void tb2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tb2.Text.ToString() == "")
            {
                yy = "";
            }
            else
            {
                yy = " AND franchisee LIKE '" + tb2.Text.ToString() + "%'";
            }
        }

        private void tb2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tb2.Text.ToString() == "")
                {
                    yy = "";
                }
                else
                {
                    yy = " AND franchisee LIKE '" + tb2.Text.ToString() + "%'";
                }
                filterThis();
            }
        }

        private void filterThis()
        {
            sqlmain = "Select * from franchise_request where ( franchise_status = 'PENDING ' " +
                        xx +
                        yy + " )";
            dosearch();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.Items.Count > 0)
            {
                if (MessageBox.Show("Do you want to review request?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row == null) return;
                    classes.binding rw;
                    rw = (classes.binding)dataGrid.SelectedItem;
                    //mw = (MainWindow)Application.Current.MainWindow;
                    //mw.tb_code.Text = rw._code.ToString();
                    for (int i = 0; i <= 99999; i++)
                    {
                        try
                        {
                            ((MainWindow)Application.Current.Windows[i]).tbtb.Text = "y";
                            ((MainWindow)Application.Current.Windows[i]).Cancel.Visibility = Visibility.Visible;
                            ((MainWindow)Application.Current.Windows[i]).tb_code.Text = rw._code.ToString();
                            ((MainWindow)Application.Current.Windows[i]).dtrequest.Text = rw._request_date.ToString("yyyy-MM-dd");
                            ((MainWindow)Application.Current.Windows[i]).tb_company.Text = rw._company_name.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_industry.Text = rw._industry.ToString();
                            ((MainWindow)Application.Current.Windows[i]).txt_bldg.Text = rw._add_bldg;
                            ((MainWindow)Application.Current.Windows[i]).txt_brgy.Text = rw._add_brgy;
                            ((MainWindow)Application.Current.Windows[i]).txt_city.Text = rw._add_city;
                            ((MainWindow)Application.Current.Windows[i]).txt_region.Text = rw._add_region;
                        //    ((MainWindow)Application.Current.Windows[i]).txt_city.Text = rw._address.ToString();

                            ((MainWindow)Application.Current.Windows[i]).tb_contact.Text = rw._contact_person.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_contact_no.Text = rw._contact_person_number.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_position.Text = rw._designation.ToString();

                            ((MainWindow)Application.Current.Windows[i]).tb_fstatus.Text = rw._fstatus.ToString().ToUpper();
                            //MessageBox.Show(rw._fstatus.ToString());
                            ((MainWindow)Application.Current.Windows[i]).tb_existingprovider.Text = rw._existing_provider.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_years.Text = rw._years_with_provider.ToString();
                            ((MainWindow)Application.Current.Windows[i]).cb_plan.Text = rw._type_of_plan.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_principal.Text = rw._prins_count.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_dependents.Text = rw._deps_count.ToString();
                            ((MainWindow)Application.Current.Windows[i]).cb_plan.Text = rw._type_of_plan.ToString().ToUpper(); ;
                            ((MainWindow)Application.Current.Windows[i]).sgroup.Text = rw._subgroup.ToString();
                            ((MainWindow)Application.Current.Windows[i]).acb.Text = rw._franchisee.ToString();
                            try
                            {
                                Selectedsale = (classes.binding)((MainWindow)Application.Current.Windows[i]).acb.SelectedItem;
                                ((MainWindow)Application.Current.Windows[i]).acode.Content = Selectedsale._agent_code.ToString();
                                Selecteddate = (classes.binding)((MainWindow)Application.Current.Windows[i]).tb_actuarial.SelectedItem;
                                selectedActuarial = (classes.binding)((MainWindow)Application.Current.Windows[i]).tb_actuarial.SelectedItem;
                                //selectedStatus = (classes.binding)((MainWindow)Application.Current.Windows[i]).tb_fstatus.SelectedItem;
                            }
                            catch (Exception err)
                            {
                                //MessageBox.Show(err.ToString());
                            }

                            ((MainWindow)Application.Current.Windows[i]).tb_fcontact.Text = rw._fcontact_person_no.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_employee_count.Text = rw._employee_count.ToString();
                            ((MainWindow)Application.Current.Windows[i]).tb_fstatus.SelectedItem = rw._fstatus.ToString().ToUpper();
                            ((MainWindow)Application.Current.Windows[i]).dtapproval.Text = rw._approval_date.ToString("yyyy-MM-dd");
                            ((MainWindow)Application.Current.Windows[i]).dtexpiry.Text = rw._expiry_date.ToString("yyyy-MM-dd");
                            ((MainWindow)Application.Current.Windows[i]).tb_remarks.Text = rw._remarks.ToString();
                            //((MainWindow)Application.Current.Windows[i]).tb_actuarial.Text = rw._actuarial.ToString();
                            //((MainWindow)Application.Current.Windows[i]).tb_approving.Text = rw._approving_officer.ToString();

                           
                            this.Close();
                            return;

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.ToString());
                            this.Close();
                            return;
                        }
                       
                    }
                     
                }
            }
        }


        private void cls()
        {
            l.Close();
        }


    }
}
