using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for sales_fran.xaml
    /// </summary>
    public partial class sales_fran : Window
    {
        public sales_fran()
        {
            InitializeComponent();
        }

        //private const int GWL_STYLE = -16;
        //private const int WS_SYSMENU = 0x80000;
        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        //[DllImport("user32.dll")]
        //private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        searchwin sw;
        DropShadowEffect eff = new DropShadowEffect();
        public String addupdate = "ADD";
        classes.insertrecord ins = new classes.insertrecord();
        classes.getData gd = new classes.getData();
        public string maxcode = "";
        notifbox nb;

        public void getMaxcode()
        {
            string dtime = DateTime.Today.ToString("MMddyy") + '-' + DateTime.Now.ToString("hhss");
            maxcode = "F" + dtime;
        }

        #region SearchButton
        private void search_MouseEnter(object sender, MouseEventArgs e)
        {
            //search.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            //search.Foreground = Brushes.Blue;
        }


        private void search_MouseLeave(object sender, MouseEventArgs e)
        {
           // search.Background = null;
            //search.Foreground = Brushes.Black;

        }


        private void addmenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //br.Visibility = Visibility.Hidden;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
            tb_address.Text = "";
            tb_contact.Text = "";
            tb_contact_no.Text = "";
            tb_position.Text = "";
            tb_existingprovider.Text = "";
            tb_years.Text = "";
            cb_plan.Text = "";
            tb_principal.Text = "0";
            tb_dependents.Text = "0";
            tb_employee_count.Text = "0";
            acb.Text = "";
            tb_fcontact.Text = "";
            tb_fstatus.Text = "PENDING";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            //tb_actuarial.Text = "";
            //tb_approving.Text = "";
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            //proc.Content = "ADDITION OF FRANCHISE";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb_company.Focus();
        }

        private void addmenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }


        private void search_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb_code.Text = "";
            //proc.Content = "UPDATE OF FRANCHISE";
            addupdate = "UPDATE";
            add.Visibility = System.Windows.Visibility.Hidden;
            update.Visibility = System.Windows.Visibility.Visible;
            Cancel.Visibility = System.Windows.Visibility.Visible;
            //search.Foreground = Brushes.White;
            add.Visibility = System.Windows.Visibility.Hidden;
            update.Visibility = System.Windows.Visibility.Visible;
            sw = new searchwin();
            sw.Show();
            this.Close();
        }

        private void search_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //search.Foreground = Brushes.Blue;
        }

        //EXIT
        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            //exit.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            //exit.Foreground = Brushes.Blue;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            //exit.Background = null;
            //exit.Foreground = Brushes.Black;
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you really want to Exit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            }
        }

        private void exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //exit.Foreground = Brushes.Blue;
        }
        #endregion

        #region gotfocus Effects

        private void sgroup_LostFocus(object sender, RoutedEventArgs e)
        {
            sgroup.Effect = null;
            sgroup.Background = null;
        }

        private void sgroup_GotFocus(object sender, RoutedEventArgs e)
        {
            sgroup.Effect = eff;
            sgroup.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_company_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_company.Effect = eff;
            tb_company.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_company_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_company.Effect = null;
            tb_company.Background = null;
        }

        private void tb_industry_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_industry.Effect = eff;
            tb_industry.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_industry_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_industry.Effect = null;
            tb_industry.Background = null;
        }

        private void tb_address_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_address.Effect = eff;
            tb_address.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_address_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_address.Effect = null;
            tb_address.Background = null;
        }

        private void tb_contact_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_contact.Effect = eff;
            tb_contact.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_contact_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_contact.Effect = null;
            tb_contact.Background = null;
        }

        private void tb_contact_no_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_contact_no.Effect = eff;
            tb_contact_no.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_contact_no_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_contact_no.Effect = null;
            tb_contact_no.Background = null;
        }

        private void tb_position_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_position.Effect = eff;
            tb_position.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_position_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_position.Effect = null;
            tb_position.Background = null;
        }

        private void tb_existingprovider_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_existingprovider.Effect = eff;
            tb_existingprovider.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_existingprovider_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_existingprovider.Effect = null;
            tb_existingprovider.Background = null;
        }

        private void tb_years_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_years.Effect = eff;
            tb_years.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_years_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_years.Effect = null;
            tb_years.Background = null;
        }

        private void tb_years_Copy_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_principal.Effect = eff;
            tb_principal.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_years_Copy_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_principal.Effect = null;
            tb_principal.Background = null;
            try
            {
                int prins = int.Parse(tb_principal.Text);
                int deps = int.Parse(tb_dependents.Text);
                tb_employee_count.Text = (prins + deps).ToString();
            }
            catch (Exception err)
            {
            }
        }

        private void tb_years_Copy1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_dependents.Effect = eff;
            tb_dependents.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_years_Copy1_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_dependents.Effect = null;
            tb_dependents.Background = null;
            try
            {
                int prins = int.Parse(tb_principal.Text);
                int deps = int.Parse(tb_dependents.Text);
                tb_employee_count.Text = (prins + deps).ToString();
            }
            catch (Exception err)
            {
            }
        }

        //private void tb_franchisee_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    tb_franchisee.Effect = eff;
        //    tb_franchisee.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        //}

        //private void tb_franchisee_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    tb_franchisee.Effect = null;
        //    tb_franchisee.Background = null;
        //}

        private void acb_GotFocus(object sender, RoutedEventArgs e)
        {
            acb.Effect = eff;
            acb.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void acb_LostFocus(object sender, RoutedEventArgs e)
        {
            acb.Effect = null;
            acb.Background = null;
        }

        private void tb_fstatus_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_fstatus.Effect = eff;
            tb_fstatus.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_fstatus_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_fstatus.Effect = null;
            tb_fstatus.Background = null;
        }

        private void tb_remarks_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_remarks.Effect = eff;
            tb_remarks.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_remarks_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_remarks.Effect = null;
            tb_remarks.Background = null;
        }

        private void tb_actuarial_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_actuarial.Effect = eff;
            tb_actuarial.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_actuarial_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_actuarial.Effect = null;
            tb_actuarial.Background = null;
        }

        private void tb_fcontact_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_fcontact.Effect = eff;
            tb_fcontact.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_fcontact_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_fcontact.Effect = null;
            tb_fcontact.Background = null;
        }

        private void tb_approving_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_approving.Effect = eff;
            tb_approving.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_approving_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_approving.Effect = null;
            tb_approving.Background = null;


        }

        private void tb_employee_count_GotFocus(object sender, RoutedEventArgs e)
        {

            tb_employee_count.Effect = eff;
            tb_employee_count.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
            try
            {
                int prins = int.Parse(tb_principal.Text);
                int deps = int.Parse(tb_dependents.Text);
                tb_employee_count.Text = (prins + deps).ToString();
            }
            catch (Exception err)
            {
            }
        }

        private void tb_employee_count_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_employee_count.Effect = null;
            tb_employee_count.Background = null;
        }

        #endregion      

        classes.updaterecord ur = new classes.updaterecord();

        #region CLICK EVENTS ADD UPDATE CANCEL

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to update this Franchise?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                addupdate = "ADD";
                update.Visibility = System.Windows.Visibility.Hidden;
                Cancel.Visibility = System.Windows.Visibility.Hidden;
                add.Visibility = System.Windows.Visibility.Visible;

                //INSERT UPDATE FUNCTION HERE

                classes.binding bind1 = new classes.binding();
                bind1._code = tb_code.Text;
                bind1._request_date = (dtrequest.Text == "") ? DateTime.Now : Convert.ToDateTime(dtrequest.Text.ToString());
                bind1._franchisee = acb.Text.Replace("'", "\'");
                //bind1._agent_code = acode.Content.ToString();
                bind1._company_name = tb_company.Text.Replace("'", "\'");
                bind1._contact_person_number = tb_contact_no.Text.Replace("'", "\'");
                bind1._industry = tb_industry.Text.Replace("'", "\'");
                bind1._contact_person = tb_contact.Text.Replace("'", "\'");
                bind1._designation = tb_position.Text.Replace("'", "\'");
              //  bind1._address = tb_address.Text.Replace("'", "\'"); ;
                //bind1._contact_person_number = tb_contact.Text;
                bind1._type_of_plan = cb_plan.Text;
                bind1._existing_provider = tb_existingprovider.Text;
                bind1._years_with_provider = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
                bind1._contract_expiry = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
                bind1._prins_count = Convert.ToInt32(tb_principal.Text);
                bind1._deps_count = Convert.ToInt32(tb_dependents.Text);
                bind1._employee_count = Convert.ToInt32(tb_employee_count.Text);
                bind1._fstatus = tb_fstatus.Text.Replace("'", "\'");
                bind1._subgroup = sgroup.Text.Replace("'", "\'");
                bind1._approving_officer = tb_approving.Text.Replace("'", "\'");
                bind1._fcontact_person_no = tb_fcontact.Text.Replace("'", "\'");
                bind1._approval_date = (dtapproval.Text == "") ? DateTime.Now : Convert.ToDateTime(dtapproval.Text.ToString());
                bind1._expiry_date = (dtexpiry.Text == "") ? DateTime.Now : Convert.ToDateTime(dtexpiry.Text.ToString());
                bind1._remarks = tb_remarks.Text.Replace("'", "\'");
                bind1._actuarial = tb_actuarial.Text.Replace("'", "\'");
                ins.getacode = acode.Content.ToString();
                nb = new notifbox();
                nb.ShowDialog();
                ur.updaterecord_fclient(bind1);
                ur.updaterecord_fhistory(bind1);
                ur.updaterecord_fstatus(bind1);
                ur.updaterecord_fpayment(bind1);
                ur.insertrecord_tracking(bind1);

                //***//


                dtrequest.Text = "";
                tb_company.Text = "";
                tb_industry.Text = "";
                tb_address.Text = "";
                tb_contact.Text = "";
                tb_contact_no.Text = "";
                tb_position.Text = "";
                tb_existingprovider.Text = "";
                tb_years.Text = "";
                cb_plan.Text = "";
                tb_principal.Text = "0";
                tb_dependents.Text = "0";
                tb_employee_count.Text = "0";
                acb.Text = "";
                tb_fcontact.Text = "";
                tb_fstatus.Text = "PENDING";
                sgroup.Text = "";
                dtapproval.Text = "";
                dtexpiry.Text = "";
                tb_remarks.Text = "";
                //tb_actuarial.Text = "";
                tb_approving.Text = "";
                acode.Content = "";
                getMaxcode();
                tb_code.Text = maxcode;
                //proc.Content = "ADDITION OF FRANCHISE";
                dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
                tb_company.Focus();
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            getMaxcode();
            tb_code.Text = maxcode;
            addupdate = "ADD";
            if (tb_company.Text == "" || dtrequest.Text == "" || acb.Text == "" || tb_fstatus.Text == "")
            {
                MessageBox.Show("COMPANY NAME, DATE REQUEST, FRANCHISEE, and STATUS are required! Please check if there's a blank field.", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                tb_company.Focus();
                //this.Owner = App.Current.MainWindow;               
            }
            else
            {
                classes.binding bind = new classes.binding();
                bind._code = tb_code.Text;
                bind._request_date = (dtrequest.Text == "") ? DateTime.Now : Convert.ToDateTime(dtrequest.Text.ToString());
                bind._franchisee = acb.Text.Replace("'", "\'");
                bind._company_name = tb_company.Text;
                bind._contact_person_number = tb_contact_no.Text;
                bind._industry = tb_industry.Text;
                bind._contact_person = tb_contact.Text;
                bind._designation = tb_position.Text;
             
                bind._address = tb_address.Text;
                //bind._contact_person_number = tb_contact.Text;
                bind._type_of_plan = cb_plan.Text;
                bind._existing_provider = tb_existingprovider.Text;
                bind._years_with_provider = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
                bind._contract_expiry = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
                bind._prins_count = Convert.ToInt32(tb_principal.Text);
                bind._deps_count = Convert.ToInt32(tb_dependents.Text);
                bind._employee_count = Convert.ToInt32(tb_employee_count.Text);
                bind._fstatus = tb_fstatus.Text;
                bind._subgroup = sgroup.Text;
                bind._approving_officer = tb_approving.Text;
                bind._fcontact_person_no = tb_fcontact.Text;
                bind._approval_date = (dtapproval.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtapproval.Text.ToString());
                bind._expiry_date = (dtexpiry.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtexpiry.Text.ToString());
                bind._remarks = tb_remarks.Text;
                bind._actuarial = tb_actuarial.Text;
                ins.getacode = acode.Content.ToString();
                nb = new notifbox();
                nb.ShowDialog();
                ins.insertrecord_fclient(bind);
                ins.insertrecord_fhistory(bind);
                ins.insertrecord_fstatus(bind);
                ins.insertrecord_fpayment(bind);
                ins.insertrecord_request(bind);
                ins.insertrecord_trackcode(bind);

                //ins.
                //INSERT ADD FUNCTION HERE
                //code, date_request, franchisee, company_name, industry, contact_person,
                //designation, complete_address, contact_numbers
                ///

                dtrequest.Text = "";
                tb_company.Text = "";
                tb_industry.Text = "";
                tb_address.Text = "";
                tb_contact.Text = "";
                tb_contact_no.Text = "";
                tb_position.Text = "";
                tb_existingprovider.Text = "";
                tb_years.Text = "";
                cb_plan.Text = "";
                tb_principal.Text = "0";
                tb_dependents.Text = "0";
                tb_employee_count.Text = "0";
                acb.Text = "";
                tb_fcontact.Text = "";
                tb_fstatus.Text = "PENDING";
                sgroup.Text = "";
                dtapproval.Text = "";
                dtexpiry.Text = "";
                tb_remarks.Text = "";
                //tb_actuarial.Text = "";
                tb_approving.Text = "";
                acode.Content = "";
                getMaxcode();
                tb_code.Text = maxcode;
                srchng();
               
                //proc.Content = "ADDITION OF FRANCHISE";
                dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
                tb_company.Focus();
               
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            addupdate = "ADD";
            update.Visibility = System.Windows.Visibility.Hidden;
            Cancel.Visibility = System.Windows.Visibility.Hidden;
            add.Visibility = System.Windows.Visibility.Visible;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
            tb_address.Text = "";
            tb_contact.Text = "";
            tb_contact_no.Text = "";
            tb_position.Text = "";
            tb_existingprovider.Text = "";
            tb_years.Text = "";
            cb_plan.Text = "";
            tb_principal.Text = "0";
            tb_dependents.Text = "0";
            tb_employee_count.Text = "0";
            acb.Text = "";
            tb_fcontact.Text = "";
            tb_fstatus.Text = "PENDING";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            //tb_actuarial.Text = "";
            //tb_approving.Text = "ANDREA";
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            //proc.Content = "ADDITION OF FRANCHISE";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb_company.Focus();
        }
        #endregion

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
           // SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            //GetIPv4Address();
            //MessageBox.Show(strHostname  + "   " + strIPAddress);
            if (tb_company.Text == "")
            {
                getMaxcode();
                tb_code.Text = maxcode;
                tb_company.Focus();
                dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtexpiry.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                tb_principal.Text = "0";
                tb_dependents.Text = "0";
                tb_employee_count.Text = "0";
                tb_company.Focus();
            }
        }

        #region button mouse leave Effects

        private void addmenu_MouseEnter(object sender, MouseEventArgs e)
        {
            //addmenu.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            //addmenu.Foreground = Brushes.Blue;
        }

        private void addmenu_MouseLeave(object sender, MouseEventArgs e)
        {
            //addmenu.Background = null;
           // addmenu.Foreground = Brushes.Black;
        }

        private void add_MouseEnter(object sender, MouseEventArgs e)
        {
            add.Foreground = Brushes.Black;
        }

        private void add_MouseLeave(object sender, MouseEventArgs e)
        {
            add.Foreground = Brushes.White;
        }

        private void update_MouseEnter(object sender, MouseEventArgs e)
        {
            update.Foreground = Brushes.Black;
        }

        private void update_MouseLeave(object sender, MouseEventArgs e)
        {
            update.Foreground = Brushes.White;
        }

        private void Cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            Cancel.Foreground = Brushes.Black;
        }

        private void Cancel_MouseLeave(object sender, MouseEventArgs e)
        {
            Cancel.Foreground = Brushes.White;
        }
        #endregion 


        #region AutoCompleteBox Event
        private classes.binding Selectedsale, Selectedactuarial;
        private void acb_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                Selectedsale = (classes.binding)acb.SelectedItem;
                acode.Content = Selectedsale._agent_code.ToString();
                // acode.Content = Selectedsale._agent_code.ToString();         
            }
            catch (Exception err)
            {
            }
        }

        private void acb_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                Selectedsale = (classes.binding)acb.SelectedItem;
                acode.Content = "";
            }
            catch (Exception err)
            {
            }
        }

        public string sql, sql3;
        //public BackgroundWorker bgworker;
        public BackgroundWorker bg;
        public void srchng()
        {
            sql1 = "Select CONCAT(sales,'    (',salesgroup,'-',LEFT(branchoffice,LOCATE(' ',branchoffice) - 1),') ' ) As `saless`,agent_code from `agent_broker`";
            sql2 = "Select DISTINCT actuarial from transaction_tracking";
            sql3 = "Select * from ref_fstatus order by autocode";
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            //bg.ProgressChanged += bg_ProgressChanged;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }
        public string sql1, sql2;
        public DataTable zxc;
        public List<classes.binding> cname = new List<classes.binding>();
        public List<classes.binding> cname2 = new List<classes.binding>();
        public List<classes.binding> cname3 = new List<classes.binding>();
        public void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            cname = new List<classes.binding>();
            cname2 = new List<classes.binding>();
            cname3 = new List<classes.binding>();
            classes.getData gdata = new classes.getData();
            DataTable lbData, actData, statData;
            lbData = gdata.getdatasource(sql1);

            foreach (DataRow ctr in lbData.Rows)
            {
                cname.Add(new classes.binding
                {
                    _sales = ctr["saless"].ToString(),
                    _agent_code = ctr["agent_code"].ToString()
                });
            }

            actData = gdata.getdatasource(sql2);
            foreach (DataRow ctr1 in actData.Rows)
            {
                cname2.Add(new classes.binding
                {
                    _actuarial = ctr1["actuarial"].ToString()
                });

            }

            statData = gdata.getdatasource(sql3);
            foreach (DataRow ctr2 in statData.Rows)
            {
                cname3.Add(new classes.binding
                {
                    _fstatus = ctr2["status"].ToString()
                });

            }

        }
        public void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            acb.ItemsSource = cname;
            tb_actuarial.ItemsSource = cname2;
            tb_fstatus.ItemsSource = cname3;
            tb_fstatus.Text = "PENDING";
        }
        private void acb1_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                Selectedactuarial = (classes.binding)tb_actuarial.SelectedItem;
                // acode.Content = Selectedsale._agent_code.ToString();         
            }
            catch (Exception err)
            {
            }
        }
        private void acb1_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_actuarial.Effect = eff;
            tb_actuarial.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
            srchng();
        }
        private void acb1_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_actuarial.Effect = null;
            tb_actuarial.Background = null;
        }
        private void acb1_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                Selectedactuarial = (classes.binding)tb_actuarial.SelectedItem;
            }
            catch (Exception err)
            {
            }
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
        //**********//
        private void Image_MouseEnter_1(object sender, MouseEventArgs e)
        {
            search_MouseEnter(sender, e);
        }

        private void Image_MouseLeave_1(object sender, MouseEventArgs e)
        {
            search_MouseLeave(sender, e);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            search_MouseLeftButtonDown(sender, e);
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            search_MouseLeftButtonUp(sender, e);
        }
        //*********//
        private void Image_MouseEnter_2(object sender, MouseEventArgs e)
        {
            exit_MouseEnter(sender, e);
        }

        private void Image_MouseLeave_2(object sender, MouseEventArgs e)
        {
            exit_MouseLeave(sender, e);
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            exit_MouseLeftButtonDown(sender, e);
        }

        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            exit_MouseLeftButtonUp(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (tb_company.Text == "")
            {
                srchng_();
                getMaxcode();
                tb_code.Text = maxcode;
                tb_company.Focus();
                dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //dtexpiry.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                tb_principal.Text = "0";
                tb_dependents.Text = "0";
                tb_employee_count.Text = "0";
                tb_company.Focus();
            }                    
        }

        string sql_, sql2_,sql3_,sql4_;
        //public BackgroundWorker bgworker;
        public BackgroundWorker bg_;
        public void srchng_()
        {
            sql_ = "Select CONCAT(sales,'    (',salesgroup,'-',LEFT(branchoffice,LOCATE(' ',branchoffice) - 1),') ' ) As `saless`,agent_code from `agent_broker`";
            sql2_ = "Select DISTINCT actuarial from transaction_tracking";
            sql3_ = "Select * from ref_fstatus order by autocode";
            sql4_ = "Select * from program_type";
            dtrequest.SelectedDate = DateTime.Now.Date;
            bg_ = new BackgroundWorker();
            bg_.WorkerReportsProgress = true;
            bg_.DoWork += bg_DoWorks;
            //bg.ProgressChanged += bg_ProgressChanged;
            bg_.RunWorkerCompleted += bg_RunWorkerCompleteds;
            bg_.RunWorkerAsync();
        }

        public DataTable zxc_;
        public List<classes.binding> cname_ = new List<classes.binding>();
        List<classes.binding> cname2_ = new List<classes.binding>();
        List<classes.binding> cname3_ = new List<classes.binding>();

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Focus();
        }

        List<classes.binding> cname4_ = new List<classes.binding>();
        public void bg_DoWorks(object sender, DoWorkEventArgs e)
        {
            classes.getData gdata_ = new classes.getData();
            DataTable lbData_;
            DataTable actData, statData, planData;
            lbData_ = gdata_.getdatasource(sql_);

            foreach (DataRow ctr in lbData_.Rows)
            {
                cname_.Add(new classes.binding
                {
                    _sales = ctr["saless"].ToString(),
                    _agent_code = ctr["agent_code"].ToString()
                });
            }

            actData = gdata_.getdatasource(sql2_);
            foreach (DataRow ctr1 in actData.Rows)
            {
                cname2_.Add(new classes.binding
                {
                    _actuarial = ctr1["actuarial"].ToString()
                });

            }

            statData = gdata_.getdatasource(sql3_);
            foreach (DataRow ctr2 in statData.Rows)
            {
                cname3_.Add(new classes.binding
                {
                    _fstatus = ctr2["status"].ToString()
                });

            }

            planData = gdata_.getdatasource(sql4_);
            foreach (DataRow ctr2 in planData.Rows)
            {
                cname4_.Add(new classes.binding
                {
                    _type_of_plan = ctr2["program_type"].ToString()
                });

            }
        }


        public void bg_RunWorkerCompleteds(object sender, RunWorkerCompletedEventArgs e)
        {

            //sh = new sales_homepage();
            acb.ItemsSource = cname_;
            //acb.SelectedItem = m.Content.ToString();
            //acode.Content = n.Content.ToString();
            tb_actuarial.ItemsSource = cname2_;
            tb_fstatus.ItemsSource = cname3_;
            cb_plan.ItemsSource = cname4_;
            tb_fstatus.SelectedIndex = 0;
            
            //sh.ac = cname;
        }

        #endregion

        #region clear events
        private void clear_MouseEnter(object sender, MouseEventArgs e)
        {
            clear.Foreground = Brushes.Red;
        }

        private void clear_MouseLeave(object sender, MouseEventArgs e)
        {
            clear.Foreground = Brushes.Black;
        }

        private void clear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //br.Visibility = System.Windows.Visibility.Visible;
           // addupdate = "ADD";
            update.Visibility = System.Windows.Visibility.Hidden;
            Cancel.Visibility = System.Windows.Visibility.Hidden;
            add.Visibility = System.Windows.Visibility.Visible;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
            tb_address.Text = "";
            tb_contact.Text = "";
            tb_contact_no.Text = "";
            tb_position.Text = "";
            tb_existingprovider.Text = "";
            tb_years.Text = "";
            cb_plan.Text = "";
            tb_principal.Text = "0";
            tb_dependents.Text = "0";
            tb_employee_count.Text = "0";
            acb.Text = "";
            tb_fcontact.Text = "";
            tb_fstatus.Text = "PENDING";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            //tb_actuarial.Text = "";
            tb_approving.Text = "";
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion



    }
}

