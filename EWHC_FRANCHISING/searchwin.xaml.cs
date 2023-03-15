using EWHC_FRANCHISING.classes;
using Microsoft.Win32;
using Microsoft.Windows.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using excel = Microsoft.Office.Interop.Excel;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for searchwin.xaml
    /// </summary>
    public partial class searchwin : Window
    {
        public searchwin()
        {
            InitializeComponent();
           
            
        }
        public users currentUser;

        public string _sql;
        loading lsss;
        classes.getData gd = new classes.getData();
        BlurEffect blrr = new BlurEffect();
        public BackgroundWorker bgworker;

        excel.Application xlapp;
        excel.Range xlrng;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        Object misvalue = System.Reflection.Missing.Value;
        Stream myStream;

        public int rows = 2;
        public int cols = 1;

        private void TextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //searchfunc();
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {

            string myRequestQuery;

            myRequestQuery = "";
            if(currentUser.userLevel == "SALES")
            {
                myRequestQuery = " AND c.created_by_userlogin_autokey = " + currentUser.id;
            }


            if (tbsearch.Text == "")
            {
                if (cb.IsChecked == true)
                {
                    if (MessageBox.Show("Do you want to display all Closed Accounts?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _sql = "Select c.code, c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                         " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                         " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, rf.status, c.subgroup, s.date_of_approval, " +
                         " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type, " +
                         " c.add_bldg, c.add_street, c.add_brgy, c.add_city, c.add_region, u.email, a.username as approver " +
                         " from franchise_client c " +
                         " inner join user_login u on (c.created_by_userlogin_autokey = u.id) " +
                         " INNER JOIN franchise_history h ON (c.code = h.franchise_key) " +
                         " INNER JOIN ref_franchise_status s ON (h.franchise_key = s.code) " +
                         " INNER JOIN ref_type_of_plan t ON (c.code = t.code) " +
                         " LEFT JOIN ref_fstatus rf ON (s.franchise_status = rf.autocode) " +
                         " LEFT JOIN user_login a on (a.id = s.approving_officer_id) " +
                         " WHERE s.remarks LIKE '%CLOSED%' " +
                         myRequestQuery +
                         " order by  s.expiry_date  desc";
              
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to display all?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _sql = "Select c.code, c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                         " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                         " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, rf.status, c.subgroup, s.date_of_approval, " +
                         " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type, " +
                         " c.add_bldg, c.add_street, c.add_brgy, c.add_city, c.add_region, u.email, a.username as approver " +
                         " from franchise_client c " +
                         " inner join user_login u on (c.created_by_userlogin_autokey = u.id) " +
                         " INNER JOIN franchise_history h  " +
                         " ON (c.code = h.franchise_key) " +
                         " INNER JOIN ref_franchise_status s " +
                         " ON (h.franchise_key = s.code) " +
                         " INNER JOIN ref_type_of_plan t " +
                         " ON (c.code = t.code) " +
                         " LEFT JOIN ref_fstatus rf ON (s.franchise_status = rf.autocode) " +
                         " LEFT JOIN user_login a on (a.id = s.approving_officer_id) " +
                         myRequestQuery +
                         " order by s.expiry_date desc ";
                 
                    }
                    else
                    {
                        tbsearch.Focus();
                    }
                }
            }

            else
            {
                if (cb.IsChecked == true)
                {
                    _sql = "Select c.code, c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                     " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                     " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, rf.status, c.subgroup, s.date_of_approval, " +
                     " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type, " +
                     " c.add_bldg, c.add_street, c.add_brgy, c.add_city, c.add_region, u.email, a.username as approver " +
                     " from franchise_client c " +
                     " inner join user_login u on (c.created_by_userlogin_autokey = u.id) " +
                     " INNER JOIN franchise_history h  " +
                     " ON (c.code = h.franchise_key) " +
                     " INNER JOIN ref_franchise_status s " +
                     " ON (h.franchise_key = s.code) " +
                     " INNER JOIN ref_type_of_plan t " +
                     " ON (c.code = t.code) " +
                     " LEFT JOIN ref_fstatus rf ON (s.franchise_status = rf.autocode) " +
                     " LEFT JOIN user_login a on (a.id = s.approving_officer_id) " +
                     " where c.company_name LIKE '%" + tbsearch.Text.Replace("'", "''").TrimEnd().ToString().Replace("'\'", "") + "%' OR c.code = '" + tbsearch.Text.Replace("'", "''").TrimEnd() + "' " +
                     " AND s.remarks LIKE '%CLOSED%' " +
                     myRequestQuery +
                     " order by  s.expiry_date desc ";
                   
                }
                else
                {
                    _sql = "Select c.code, c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                     " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                     " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, rf.status,c.subgroup, s.date_of_approval, " +
                     " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type, " +
                     " c.add_bldg, c.add_street, c.add_brgy, c.add_city, c.add_region, u.email, a.username as approver " +
                     " from franchise_client c " +
                     " inner join user_login u on (c.created_by_userlogin_autokey = u.id) " +
                     " INNER JOIN franchise_history h  " +
                     " ON (c.code = h.franchise_key) " +
                     " INNER JOIN ref_franchise_status s " +
                     " ON (h.franchise_key = s.code) " +
                     " INNER JOIN ref_type_of_plan t " +
                     " ON (c.code = t.code) " +
                     " LEFT JOIN ref_fstatus rf ON (s.franchise_status = rf.autocode) " +
                     " LEFT JOIN user_login a on (a.id = s.approving_officer_id) " +
                     " where c.company_name LIKE '%" + tbsearch.Text.Replace("'", "''").TrimEnd().ToString().Replace("'\'", "") + "%' OR c.code = '" + tbsearch.Text.Replace("'", "''").TrimEnd() + "' " +
                     myRequestQuery +
                     " order by  s.expiry_date desc ";
 
                }
            }

            searchfunc();
        }      

        public void searchfunc()
        {
            dgrid.ItemsSource = "";
            count.Content = "0";
            dgrid.Focus();
            runWorker();

        }

        public void runWorker()
        {
            lsss = new loading();
            dgrid.Effect = blrr;
            bgworker = new BackgroundWorker();
            bgworker.WorkerReportsProgress = true;
            bgworker.DoWork += bgworker_DoWork;
            bgworker.ProgressChanged += bgworker_ProgressChanged;
            bgworker.RunWorkerCompleted += bgworker_RunWorkerCompleted;
            bgworker.RunWorkerAsync();      
            lsss.ShowDialog();
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

        public ObservableCollection<classes.binding> q = new ObservableCollection<classes.binding>();
        public DataTable alldata;
        public string bghandler;
      
        public void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            alldata = gd.getdatasource(_sql);
            q = new ObservableCollection<classes.binding>();
            int i = 1;
            foreach (DataRow ctr in alldata.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               q.Add(new classes.binding()
                               {
                                   _code = nullValues(ctr["code"].ToString()),
                                   _request_date = (ctr["date_request"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["date_request"].ToString())),
                                   _company_name = nullValues(ctr["company_name"].ToString()),
                                   _franchisee = nullValues(ctr["franchisee"].ToString()),
                                   _contact_person = nullValues(ctr["contact_person"].ToString()),
                                   _contact_person_number = nullValues(ctr["contact_numbers"].ToString()),
                                   _designation = nullValues(ctr["designation"].ToString()),
                                   _industry = nullValues(ctr["industry"].ToString()),
                                   _fcontact_person_no = nullValues(ctr["franchisee_contact"].ToString()),
                                   _address = nullValues(ctr["complete_address"].ToString()),
                                   _existing_provider = nullValues(ctr["existing_hmo"].ToString()),
                                   _contract_expiry = (ctr["contract_expiry"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["contract_expiry"].ToString())),
                                   _prins_count = Convert.ToInt32(nullValues(ctr["prins_count"].ToString())),
                                   _deps_count = Convert.ToInt32(nullValues(ctr["deps_count"].ToString())),
                                   _employee_count = Convert.ToInt32(ctr["employee_count"].ToString()),
                                   _type_of_plan = nullValues(ctr["program_type"].ToString()),
                                   _fstatus = nullValues(ctr["status"].ToString()),
                                   _subgroup = nullValues(ctr["subgroup"].ToString()),
                                   _approval_date = (ctr["date_of_approval"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["date_of_approval"].ToString())),
                                   _expiry_date = (ctr["expiry_date"].ToString() == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(nullValues(ctr["expiry_date"].ToString())),
                                   _approving_officer = ctr["approving_officer"].ToString(),
                                   _remarks = ctr["remarks"].ToString(),
                                   _actuarial = ctr["actuarial"].ToString(),
                                   _add_bldg = nullValues(ctr["add_bldg"].ToString()),
                                   _add_brgy = nullValues(ctr["add_brgy"].ToString()),
                                   _add_street = nullValues(ctr["add_street"].ToString()),
                                   _add_city = nullValues(ctr["add_city"].ToString()),
                                   _add_region = nullValues(ctr["add_region"].ToString()),
                                   requestEmail = nullValues(ctr["email"].ToString()),
                                   approvingOfficer = nullValues(ctr["approver"].ToString())
                               }); 

                               bghandler = ctr["remarks"].ToString();
                               
                               //MessageBox.Show(bghandler);
                               bgworker.ReportProgress(i, q);
                               i++;
                               //System.Threading.Thread.Sleep(2);
                           }));
            }
        }
        
        public void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {           
            dgrid.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
            //CType(dgrid.ItemsSource, ObservableCollection(Of binding)).Add(CType(e.UserState, binding))
            lsss.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / alldata.Rows.Count) * 100) + "%";
            count.Content = Convert.ToInt32(e.ProgressPercentage.ToString());
        }

        public void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dgrid.Items.Count == 0)
            {
                MessageBox.Show("No Records Found!", "No Records", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            dgrid.Effect = null;
        
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(cls));
        }

        public void cls()
        {
            lsss.Close();
        }

        DropShadowEffect eff = new DropShadowEffect();
        private void tbsearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbsearch.Effect = efff;
            tbsearch.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tbsearch_LostFocus(object sender, RoutedEventArgs e)
        {
            tbsearch.Effect = null;
            tbsearch.Background = null;
        }

        BackgroundWorker bgz;
        loading lss;
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            xtemp = "n";
            lss = new loading();

            tbsearch.Focus();
            bgz = new BackgroundWorker();
            bgz.WorkerReportsProgress = true;
            bgz.DoWork += bgz_DoWork;
            //bg.ProgressChanged += bg_ProgressChanged;
            bgz.RunWorkerCompleted += bgz_RunWorkerCompleted;
            bgz.RunWorkerAsync();
       
            lss.ShowDialog();
            // mw.add.Visibility = System.Windows.Visibility.Hidden;
            //mw.update.Visibility = System.Windows.Visibility.Visible;
            //MessageBox.Show(mw.addupdate);
        }
       
            classes.getData gdata = new classes.getData();
        List<classes.binding> cname = new List<classes.binding>();
        List<classes.binding> cname_list = new List<classes.binding>();
        public List<string> rnb_list2 { get; set; }
        public void bgz_DoWork(object sender, DoWorkEventArgs e)
        {
            cname = new List<classes.binding>();
            String sql1 = "Select DISTINCT company_name from franchise_client";
            DataTable lbData;
            lbData = new DataTable();
            lbData = gdata.getdatasource(sql1);

            foreach (DataRow ctr in lbData.Rows)
            {
                cname.Add(new classes.binding
                {
                    _company_name = ctr["company_name"].ToString()
                });
            }

            rnb_list2 = new List<string>();
            string sql2 = "Select * from ref_rnb";
            DataTable dtt = new DataTable();
            dtt = gd.getdatasource(sql2);
            foreach (DataRow x in dtt.Rows)
            {
                cname_list.Add(new classes.binding
                {
                    _rnb = x["rnb"].ToString()
                });
            }
        }

        public void bgz_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tbsearch.ItemsSource = cname;
            lss.Close();                
        }

            MainWindow mw = new MainWindow();

        //classes.insertrecord ins;

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            if (xtemp == "y")
            {
                mw.add.Visibility = System.Windows.Visibility.Hidden;
                mw.update.Visibility = System.Windows.Visibility.Visible;
                mw.Cancel.Visibility = System.Windows.Visibility.Visible;
                mw.br.Visibility = Visibility.Hidden;
                mw.proc.Content = "UPDATE OF FRANCHISE";
                //mw.acode.Content = ins.getacode.ToString();
            }
            mw.Show();
            //mw.tb_actuarial.Text = lb.Content.ToString();
        }
        public string xtemp;
        loading ll;
        string get_plan;
        BackgroundWorker bgg;
        ObservableCollection<classes.plan_count> pcc;
        private classes.binding Selectedsale, Selecteddate, SelectedStatus;
        private void dgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgrid.Items.Count > 0)
            {
                if (MessageBox.Show("Do you want to update this Franchise?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                  //  xtemp = "y";
                    //DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                   // if (row == null) return;
                    mw.currentUser = this.currentUser;
                    mw.addupdate = "UPDATE";
                    mw.grdDateRequest.Height = Double.NaN;
                    mw.grdDetails.Height = Double.NaN;
                    mw.grdButtons.Height = Double.NaN;
                    mw.grdApproval.Height = Double.NaN;
                    mw.proc.Content = "UPDATE OF FRANCHISE";
                    mw.addupdate = "UPDATE";
                    mw.add.Visibility = System.Windows.Visibility.Hidden;
                    mw.update.Visibility = System.Windows.Visibility.Visible;
                    mw.Cancel.Visibility = System.Windows.Visibility.Visible;
                    mw.search.Foreground = Brushes.White;
                    mw.add.Visibility = System.Windows.Visibility.Hidden;
                    mw.update.Visibility = System.Windows.Visibility.Visible;

                    if (currentUser.userLevel == "SALES")
                    {
                        mw.grdApproval.Height = 0;
                    }
                    
                    classes.binding rw;
                    rw = (classes.binding)dgrid.SelectedItem;
                    mw.tb_code.Text = rw._code.ToString();
                    mw.dtrequest.Text = rw._request_date.ToString("yyyy-MM-dd HH:mm:ss");
                    mw.tb_company.Text = rw._company_name.ToString();
                    mw.tb_industry.Text = rw._industry.ToString();
                    //mw.tb_address.Text = rw._address.ToString();
                    mw.tb_contact.Text = rw._contact_person.ToString();
                    mw.tb_contact_no.Text = rw._contact_person_number.ToString();
                    mw.tb_position.Text = rw._designation.ToString();
                    mw.tb_fstatus.Text = rw._fstatus.ToString();
                    mw.tb_existingprovider.Text = rw._existing_provider.ToString();
                    mw.tb_years.Text = rw._contract_expiry.ToString("yyyy-MM-dd");
                    mw.cb_plan.Text = rw._type_of_plan.ToString();
                    mw.tb_principal.Text = rw._prins_count.ToString();
                    mw.tb_dependents.Text = rw._deps_count.ToString();
                    mw.cb_plan.Text = rw._type_of_plan.ToString().ToUpper(); 
                    mw.sgroup.Text = rw._subgroup.ToString();
                    mw.acb.Text = rw._franchisee.ToString();
                    mw.txt_bldg.Text = rw._add_bldg;
                    mw.txt_brgy.Text = rw._add_brgy;
                    mw.txt_street.Text = rw._add_street;

                    mw.myEMAIL.Items.Add(rw.requestEmail);
                    mw.myEMAIL.Text = rw.requestEmail;
                    
                    mw.txt_city.SelectedItem = new classes.cities { _city = rw._add_city, _region = rw._add_region };
                    mw.txt_region.SelectedItem = new classes.regions { _region = rw._add_region };

                    try
                    {
                        Selectedsale = (classes.binding)mw.acb.SelectedItem;
                        if (Selectedsale != null) 
                            mw.acode.Content = Selectedsale._agent_code.ToString();
    

                        Selecteddate = (classes.binding)mw.tb_actuarial.SelectedItem;
                        //SelectedStatus = (classes.binding)mw.                        
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message + "\n" + err.StackTrace);

                    }

                    mw.tb_fcontact.Text = rw._fcontact_person_no.ToString();
                    mw.tb_employee_count.Text = rw._employee_count.ToString();
                    //mw.tb_fstatus.SelectedItem = rw._fstatus.ToString().ToUpper();
                    mw.dtapproval.Text = rw._approval_date.ToString("yyyy-MM-dd");

                    if(rw._expiry_date.ToString() == "0001-01-01" || rw._expiry_date.ToString()=="")
                    {
                        mw.dtexpiry.Text = null;
                        //mw.temp_exp.Text = null;
                    }
                    else
                    {
                        mw.dtexpiry.Text = rw._expiry_date.ToString("yyyy-MM-dd");
                        //mw.temp_exp.Text = rw._contract_expiry.ToString("yyyy-MM-dd");
                    }
                   

                    if(rw._remarks.ToString() != "" && rw._remarks.ToString() != null)
                    {
                        mw.getDocs.Text =  rw._remarks.ToString() + "/ ";
                    }
                    else
                    {
                        mw.getDocs.Text = rw._remarks.ToString();
                    }
                    //mw.tb_remarks.ItemsSource = null;
                    //mw.tb_remarks.Text = rw._remarks.ToString();
                    mw.tb_actuarial.Text = rw._actuarial.ToString();
                    mw.tb_approving.Text = rw._approving_officer.ToString();
                    //mw.add_Copy.Visibility = Visibility.Hidden;

                    mw.plan_grid.ItemsSource = null;
                    ll = new loading();
                    get_plan = "Select * from ref_plan_count where fcode = '" + rw._code.ToString() + "'";
                    bgg = new BackgroundWorker();
                    bgg.WorkerReportsProgress = true;
                    bgg.DoWork += bgg_DoWork;
                    bgg.ProgressChanged += bgg_ProgressChanged;
                    bgg.RunWorkerCompleted += bgg_RunWorkerCompleted;

                    //mw.GVComboBox = new List<string>() { "Principal", "Dependent" };

                    //mw.rnb_list = cname_list;

                    mw.rnb_list = new List<string>();
                    foreach (classes.binding x in cname_list)
                    {
                        mw.rnb_list.Add(x._rnb.ToString());
                    }

                    // mw.rnbox.ItemsSource = mw.GVComboBox;
                    bgg.RunWorkerAsync();
                    ll.ShowDialog();

                    this.Close();
                }
            }
        }

        private void bgg_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dtt = new DataTable();
            classes.getData gdd = new classes.getData();
            pcc = new ObservableCollection<classes.plan_count>();
            dtt = gdd.getdatasource(get_plan);
            int i = 1;
            mw.b_data = new ObservableCollection<classes.plan_count>();
           
            foreach (DataRow x in dtt.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                          new Action(() =>
                          {
                              mw.b_data.Add(new classes.plan_count()
                              {
                                  _description = x["description"].ToString(),
                                  _level = x["level"].ToString(),
                                  _count =x["count"].ToString(),
                                  _mbl = Convert.ToDouble(x["mbl"]),
                                  _rnb = x["rnb"].ToString(),
                                  _id = Convert.ToInt32(x["autokey"])
                              });
                              bgg.ReportProgress(i, mw.b_data);
                              i++;
                          }));
               
            }
        }

        private void bgg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {          
            mw.plan_grid.ItemsSource = ((ObservableCollection<classes.plan_count>)e.UserState);
        }

        private void bgg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            ll.Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("(1) Type the Franchise's Name then [Press Enter] to Search. \n" + "(2) To DISPLAY ALL the Franchise: Leave the TEXTBOX blank," + "\n" + "      then [Press ENTER] \n" + "(3) To Display Closed Account/s: Check the `Check box` below then               search ", "HOW TO SEARCH FRANCHISE", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
         
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            sp.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
        }

        private void sp_MouseLeave(object sender, MouseEventArgs e)
        {
            sp.Background = null;
        }

        private void sp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            exp.Foreground = Brushes.White;
        }

        #region Excel Events
        public SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        public BackgroundWorker bgwexp;
        public DataTable tempexp;
        DropShadowEffect efff = new DropShadowEffect();
        public classes.getData gtdt = new classes.getData();
        public string ssql = "";
        string finalPath;
        string fileName;
        private void sp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) /*****EXPORTING*****/  
        {
            exp.Foreground = Brushes.Black;
            if (expAll.IsChecked == false && (dtto.SelectedDate == null || dtfrom.SelectedDate == null))
            {
                MessageBox.Show("Please enter a valid Date! OR Put a check on the `Checkbox`");
            }
            else
            {
                if (MessageBox.Show("Do you  want to Export Excel file", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (expAll.IsChecked == true)
                    {
                        ssql = "Select c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                                 " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                                 " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, s.franchise_status,subgroup, s.date_of_approval, " +
                                 " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type from franchise_client c " +
                                 " INNER JOIN franchise_history h  " +
                                 " ON (c.code = h.franchise_key) " +
                                 " INNER JOIN ref_franchise_status s " +
                                 " ON (h.franchise_key = s.code) " +
                                 " INNER JOIN ref_type_of_plan t " +
                                 " ON (c.code = t.code) " +
                                 " order by c.company_name";
                    }
                    else
                    {
                        ssql = "Select c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
                                 " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
                                 " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, s.franchise_status,subgroup, s.date_of_approval, " +
                                 " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type from franchise_client c " +
                                 " INNER JOIN franchise_history h  " +
                                 " ON (c.code = h.franchise_key) " +
                                 " INNER JOIN ref_franchise_status s " +
                                 " ON (h.franchise_key = s.code) " +
                                 " INNER JOIN ref_type_of_plan t " +
                                 " ON (c.code = t.code) " +
                                 " WHERE (c.date_request BETWEEN '" + Convert.ToDateTime(dtfrom.SelectedDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(dtto.SelectedDate).ToString("yyyy-MM-dd") + "' ) " +
                                 " order by c.company_name";
                    }
                    //Selectedsale = (classes.binding)mw.acb.SelectedItem;
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;
                    dgrid.Effect = blrr;
                    bgwexp = new BackgroundWorker();
                    bgwexp.DoWork += bgwexp_DoWork;
                    bgwexp.ProgressChanged += bgwexp_ProgressChanged;
                    bgwexp.RunWorkerCompleted += bgwexp_RunWorkerCompleted;
                    bgwexp.WorkerReportsProgress = true;
                    bgwexp.WorkerSupportsCancellation = true;
                    bgwexp.RunWorkerAsync();           
                    //ls = new loading(); 
                   // ls.ShowDialog();              
                }
            }
        }      
        private void bgwexp_DoWork(object sender, DoWorkEventArgs e) //Filling Data in Excel 
        {        
           xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlapp.Workbooks.Add(misvalue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "List of Accounts";
            int ctr = 0;
           // xlapp = new Microsoft.Office.Interop.Excel.Application();
            tempexp = gtdt.getdatasource(ssql);
            if (tempexp.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to export!");            
                //dgrid.Effect = null;
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(cls));
                bgwexp.CancelAsync();
            }
            for (int j = 0; j <= tempexp.Columns.Count - 1; j++)
            {
                xlrng = (excel.Range)xlWorkSheet.Cells[1, j + 1];

               xlrng.Value = tempexp.Columns[j].ColumnName.ToUpper();
            }
            xlrng = (excel.Range)xlWorkSheet.Cells[1, 1];
            xlrng.EntireRow.Font.Bold = true;
            rows = 2;  
       
            foreach (DataRow x in tempexp.Rows)
            {
                ctr += 1;
                cols = 1;
                for (int j = 0; j <= tempexp.Columns.Count - 1; j++)
                {
                    //if (x[j].ToString().Contains("CLOSED"))
                    //{
                    //    xlWorkSheet.Range("A" + rows.ToString() + ": V" + rows.ToString()).
                    //}
                    xlWorkSheet.Cells[rows, cols] = x[j].ToString();
                    cols += 1;                 
                }
                rows += 1;
                bgwexp.ReportProgress(ctr);
            }
         }       


        private void bgwexp_ProgressChanged(object sender, ProgressChangedEventArgs e) //Percentage  
        {
           //ls.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / tempexp.Rows.Count) * 100) + "%";
        }
        private void bgwexp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //Saving File 
        {
            if (saveFileDialog1.ShowDialog() == true)
            {
                fileName = saveFileDialog1.FileName.ToString();
                xlWorkBook.SaveAs(fileName, excel.XlFileFormat.xlWorkbookNormal, Type.Missing, 
                        Type.Missing, Type.Missing, Type.Missing, excel.XlSaveAsAccessMode.xlExclusive,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkBook.Close();
            xlapp.Quit();
            releaseObject(xlapp);
            releaseObject(xlWorkBook);
            releaseObject(xlWorkSheet);
            }
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(cls));
            MessageBox.Show("File exported!");
            dgrid.Effect = null;
        }





        private classes.binding SelectedCom;
        private void tbsearch_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                //SelectedCom = (classes.binding)tbsearch.SelectedItem;
                //tbsearch.Text = SelectedCom._company_name.ToString();
                //if(tbsearch.IsDropDownOpen == false)
                //{
                //    searchfunc();
                //}
                
                // acode.Content = Selectedsale._agent_code.ToString();         
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + "\n" + err.StackTrace);
            }
        }

        private void tbsearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                search_Click(search, new RoutedEventArgs());
              //  searchfunc();
            }
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception err) 
            {
                MessageBox.Show(err.Message + "\n" + err.StackTrace);

            }
        }
        private void expAll_Click(object sender, RoutedEventArgs e)
        {
            if (expAll.IsChecked == true)
            {
                dtfrom.IsEnabled = false;
                dtto.IsEnabled = false;
                dtfrom.SelectedDate = null;
                dtto.SelectedDate = null;
            }
            else
            {
                dtfrom.IsEnabled = true;
                dtto.IsEnabled = true;
            }
        }
        #endregion 
    }
}




