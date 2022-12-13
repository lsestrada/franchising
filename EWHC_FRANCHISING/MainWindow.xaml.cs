using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
            srchng();
            //tb_address.Focus();         
        }
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        searchwin sw;
        DropShadowEffect eff = new DropShadowEffect();
        public String addupdate="ADD";
        classes.insertrecord ins = new classes.insertrecord();
        classes.getData gd = new classes.getData();
        
        notifbox nb;

        public string maxcode = "";
        public void getMaxcode()
        {
        string dtime = DateTime.Today.ToString("MMddyy") + '-' + DateTime.Now.ToString("hhss");     
        maxcode ="F" + dtime;     
        }
       
        #region SearchButton
        private void search_MouseEnter(object sender, MouseEventArgs e)
        {
            search.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            search.Foreground = Brushes.Blue;
        }
       

        private void search_MouseLeave(object sender, MouseEventArgs e)
        {
            search.Background = null;
            search.Foreground = Brushes.Black;
         
        }


        private void addmenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            br.Visibility = Visibility.Hidden;
            btn_docs.Visibility = Visibility.Visible;
            //view.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
          //  tb_address.Text = "";
            txt_bldg.Clear();
            txt_brgy.Clear();
            txt_city.Clear();
            txt_region.Clear();
            txt_street.Clear();
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
            tb_fstatus.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            tb_actuarial.Text = "";
            tb_approving.Text = lb.Content.ToString();
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;

            proc.Content = "ADDITION OF FRANCHISE";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb_company.Focus();
        }

        private void addmenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        
        private void search_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb_code.Text = "";
            proc.Content = "UPDATE OF FRANCHISE";
            addupdate = "UPDATE";
            add.Visibility = System.Windows.Visibility.Hidden;
            update.Visibility = System.Windows.Visibility.Visible;
            Cancel.Visibility = System.Windows.Visibility.Visible;
           search.Foreground = Brushes.White;
           add.Visibility = System.Windows.Visibility.Hidden;
           update.Visibility = System.Windows.Visibility.Visible;
           sw = new searchwin();        
           sw.Show();
            sw.lb.Content = lb.Content.ToString();     
           this.Close();
        }

        private void search_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            search.Foreground = Brushes.Blue;
        }

        //EXIT
        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
           exit.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            exit.Foreground = Brushes.Blue;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Background = null;
            exit.Foreground = Brushes.Black;
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you really want to Exit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                System.Windows.Application.Current.Shutdown();
            }
        }

        private void exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            exit.Foreground = Brushes.Blue;
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
      //      tb_address.Effect = eff;
    //        tb_address.Background = new SolidColorBrush(Color.FromRgb(255, 251, 177));
        }

        private void tb_address_LostFocus(object sender, RoutedEventArgs e)
        {
      //      tb_address.Effect = null;
      //      tb_address.Background = null;
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
                if (tb_fstatus.Text == "Approved")
                {
                    if (acb.Text.ToString() == "")
                    {
                        MessageBox.Show("Please select a franchisee!", "Franchise Transfer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }                
                }

                //if (tb_years.SelectedDate < DateTime.Now)
                //{
                //    MessageBox.Show("Can't Franchise! Expiry date exceeded!", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //    return;
                //}

                if (tb_fstatus.Text == "Approved" || tb_fstatus.Text == "Reservation")
                {
                    if (acb.Text.ToString() == "")
                    {
                        MessageBox.Show("Please select a franchisee!", "Franchise Transfer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }



                if (tb_years.SelectedDate == null || tb_years.SelectedDate < Convert.ToDateTime("0001-02-02"))
                {
                    if (tb_existingprovider.Text.ToString().ToUpper() == "NONE")
                    {
                        goto skipp;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Existing HMO Expiry Date!", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }


            skipp:
            addupdate = "ADD";
            update.Visibility = System.Windows.Visibility.Hidden;
            Cancel.Visibility = System.Windows.Visibility.Hidden;
            add.Visibility = System.Windows.Visibility.Visible;

           //'add_Copy.Visibility = System.Windows.Visibility.Visible;
                //INSERT UPDATE FUNCTION HERE

                classes.binding bind1 = new classes.binding();
            bind1._code = tb_code.Text;
            bind1._request_date = (dtrequest.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtrequest.Text.ToString());
            bind1._franchisee = acb.Text.Replace("'", "\'");
            bind1._company_name = tb_company.Text.Replace("'", "\'");
            bind1._contact_person_number = tb_contact_no.Text.Replace("'", "\'"); 
            bind1._industry = tb_industry.Text.Replace("'", "\'");
            bind1._contact_person = tb_contact.Text.Replace("'", "\'"); 
            bind1._designation = tb_position.Text.Replace("'", "\'");
                bind1._add_bldg = txt_bldg.Text;
                bind1._add_brgy = txt_brgy.Text;
                bind1._add_city = txt_city.Text;
                bind1._add_region = txt_region.Text;
                bind1._add_street = txt_street.Text;
                bind1._address = bind1._add_bldg + " " + bind1._add_street + " " +  bind1._add_brgy +  " " + bind1._add_city  + " " + bind1._add_region;
                //bind1._contact_person_number = tb_contact.Text;
                bind1._type_of_plan = cb_plan.Text;
            bind1._existing_provider = tb_existingprovider.Text;
            bind1._years_with_provider = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
            bind1._prins_count = Convert.ToInt32(tb_principal.Text);
            bind1._contract_expiry = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
            bind1._deps_count = Convert.ToInt32(tb_dependents.Text);
            bind1._employee_count = Convert.ToInt32(tb_employee_count.Text);
            bind1._fstatus = Convert.ToInt32(tb_fstatus.SelectedIndex + 1).ToString();
            bind1._subgroup = sgroup.Text.Replace("'", "\'");
            bind1._approving_officer = tb_approving.Text.Replace("'", "\'"); 
            bind1._fcontact_person_no = tb_fcontact.Text.Replace("'", "\'"); 
            bind1._approval_date = (dtapproval.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtapproval.Text.ToString());
            bind1._expiry_date = (dtexpiry.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtexpiry.Text.ToString());
            bind1._remarks = getDocs.Text.TrimEnd('/');
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

                classes.updaterecord urr;
                urr = new classes.updaterecord();
                foreach (classes.plan_count x in plan_grid.Items)
                {
                    x._fcode = tb_code.Text.ToString();
                    if (x._id > 0)
                    {
                        ur.updaterecord_plan(x);
                    }
                    else if(x._id == 0)
                    {           
                         ins.insertrecord_plan(x);
                    }                  
                }

            plan_grid.ItemsSource = null;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
                //tb_address.Text = "";
                txt_bldg.Clear();
                txt_brgy.Clear();
                txt_city.Clear();
                txt_region.Clear();
                txt_street.Clear();

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
            tb_fstatus.Text = "";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            getDocs.Text = "";
            tb_remarks.Text = "";
            tb_actuarial.Text = "";
            tb_approving.Text = lb.Content.ToString();
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            proc.Content = "ADDITION OF FRANCHISE";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb_company.Focus();
        }
        }

        public string stringy;
        private void add_Click(object sender, RoutedEventArgs e)
        {
            //getMaxcode();
            //tb_code.Text = maxcode;
            addupdate = "ADD";
            if (tb_company.Text.ToString() == "" || dtrequest.Text.ToString() == "" || acb.Text.ToString() == "" || tb_fstatus.Text.ToString() == "" || tb_actuarial.Text.ToString() == "")
            {
                if(tb_years.SelectedDate == null)
                {
                    if (tb_existingprovider.Text.ToString() == "NONE")
                    {
                        goto heree;
                    }
                    else
                    {
                        MessageBox.Show("COMPANY NAME, DATE REQUEST, FRANCHISEE, and STATUS are required! Please check if there's a blank field.", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }

                heree:
                if (tb_fstatus.SelectedIndex == 4 && tb_company.Text.ToString() != "" && dtrequest.Text.ToString() != "" && tb_actuarial.Text.ToString() != "")
                        {
                            goto SkipToThis;
                        }
                        else if (tb_fstatus.SelectedIndex == 5 && tb_company.Text.ToString() != "" && dtrequest.Text.ToString() != "" )
                        {
                            if (acb.SelectedItem == null || acb.Text.ToString() == "")
                            {
                                MessageBox.Show("Please select a franchisee!", "Franchise Transfer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                return;
                            }
                            else
                            {
                                goto SkipToThis;
                            }
                    
                }
                else
                {
                    MessageBox.Show("COMPANY NAME, DATE REQUEST, FRANCHISEE, and STATUS are required! Please check if there's a blank field.", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }              
            }

            if (tb_years.SelectedDate == null || tb_years. SelectedDate < Convert.ToDateTime("0001-02-02"))
            {
                if(tb_existingprovider.Text.ToString().ToUpper() == "NONE")
                {
                    goto SkipToThis;
                }
                else
                {
                    MessageBox.Show("Invalid Existing HMO Expiry Date!", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }


            SkipToThis:          
            if (tb_fstatus.Text.ToString() == "Approved" || tb_fstatus.Text.ToString() == "Reservation")
            {
                if(acb.Text.ToString() == "")
                {
                    MessageBox.Show("Please select a franchisee!", "Franchise Transfer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }  
            classes.binding bind = new classes.binding();
            bind._code = tb_code.Text;
            bind._request_date = (dtrequest.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtrequest.Text.ToString());
            bind._franchisee = acb.Text.ToString().Replace("'", "\'");
            bind._company_name = tb_company.Text;
            bind._contact_person_number = tb_contact_no.Text;
            bind._industry = tb_industry.Text;
            bind._contact_person = tb_contact.Text;
            bind._designation = tb_position.Text;
            bind._add_bldg = txt_bldg.Text;
            bind._add_brgy = txt_brgy.Text;
            bind._add_city = txt_city.Text;
            bind._add_region = txt_region.Text;
            bind._add_street = txt_street.Text;
            bind._address = bind._add_bldg + ' ' + bind._add_street + ' ' + bind._add_brgy + ' ' + bind._add_city + ' ' + bind._add_region;
            
            //bind._contact_person_number = tb_contact.Text;
            bind._type_of_plan = cb_plan.Text;
            bind._existing_provider = tb_existingprovider.Text;
            bind._years_with_provider =  (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
            bind._contract_expiry = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
            bind._prins_count = Convert.ToInt32(tb_principal.Text);
            bind._deps_count = Convert.ToInt32(tb_dependents.Text);
            bind._employee_count = Convert.ToInt32(tb_employee_count.Text);
            bind._fstatus = Convert.ToInt32(tb_fstatus.SelectedIndex + 1).ToString();
            bind._subgroup = sgroup.Text;
            bind._approving_officer = tb_approving.Text;
            bind._fcontact_person_no = tb_fcontact.Text;
            bind._approval_date = (dtapproval.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtapproval.Text.ToString());
            bind._expiry_date = (dtexpiry.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(dtexpiry.Text.ToString());
            bind._contract_expiry = (tb_years.Text == "") ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(tb_years.Text.ToString());
            bind._remarks = getDocs.Text.TrimEnd('/');
            bind._actuarial = tb_actuarial.Text;
            ins.getacode = acode.Content.ToString();


            foreach(classes.plan_count x in plan_grid.Items)
            {
                x._fcode = tb_code.Text.ToString();
                if(x._id == 0)
                {
                    ins.insertrecord_plan(x);
                }
                
            }


            nb = new notifbox();
            nb.ShowDialog();



            //REMOVE COMMENT!!!!

            ins.insertrecord_fclient(bind);
            ins.insertrecord_fhistory(bind);
            ins.insertrecord_fstatus(bind);
            ins.insertrecord_fpayment(bind);
            ins.insertrecord_tracking(bind);
            ins.insertrecord_trackcode(bind);
            if (tbtb.Text.ToString() == "y")
            {
                classes.updaterecord uu = new classes.updaterecord();
                uu.update_request(bind);
            }

            //******************************************//




            //ins.
            //INSERT ADD FUNCTION HERE
            //code, date_request, franchisee, company_name, industry, contact_person,
            //designation, complete_address, contact_numbers
            ///


            //SENDING WITH EMAIL HEHE
            try
            {
                if (cb_email.IsChecked == true)
                {
                    smtp_server = new System.Net.Mail.SmtpClient();
                    thisEmail = new System.Net.Mail.MailMessage();
                    smtp_server.UseDefaultCredentials = false;
                    smtp_server.Credentials = new System.Net.NetworkCredential(myEMAIL.Text.ToString(), "345Tw45T");
                    smtp_server.Port = 587;
                    smtp_server.EnableSsl = true;
                    smtp_server.Host = "eastwesthealthcare.com.ph";

                    thisEmail = new MailMessage();
                    thisEmail.From = new MailAddress(myEMAIL.Text.ToString());
                    thisEmail.To.Add(act_email.Content.ToString());
                    thisEmail.Subject = "Subject for Pricing: " + tb_company.Text.ToString();
                    thisEmail.IsBodyHtml = false;
                    thisEmail.Body = "Good Day " + tb_actuarial.Text.ToString() + ", " + "\n \n" + "This email is to notify you that you have a request for Pricing." + "\n \n" + "Thanks!";
                    try
                    {
                        thisEmail.Attachments.Add(attachment);
                    }
                    catch (Exception err)
                    {
                        //MessageBox.Show("Email sent, but there's NO ATTACHMENT!");
                    }

                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                         (s, cert, chain, sslPolicyErrors) => true;
                    smtp_server.Send(thisEmail);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Data was saved! No email sent!");
            }
            //*********************************************************************************
           
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";

            //tb_address.Text = "";
            txt_bldg.Clear();
            txt_brgy.Clear();
            txt_city.Clear();
            txt_region.Clear();
            txt_street.Clear();

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
            tb_fstatus.Text = "";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            tb_actuarial.Text = "";
            tb_approving.Text = lb.Content.ToString();
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            proc.Content = "ADDITION OF FRANCHISE";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");

            lb_filename.Content = "";
            tb_company.Focus();

            plan_grid.ItemsSource = null;

            srchng();
            tbtb.Text = "n";

            try
            {
                thisEmail.Attachments.Clear();
                lb_filename.Content = "";

            }
            catch (Exception err)
            {

            }
            
        }

        public string myEmail;

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {          
            addupdate = "ADD";
            update.Visibility = System.Windows.Visibility.Hidden;
            //Cancel.Visibility = System.Windows.Visibility.Hidden;
            add.Visibility = System.Windows.Visibility.Visible;
            //add_Copy.Visibility = System.Windows.Visibility.Visible;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";
            //tb_address.Text = "";
            txt_bldg.Clear();
            txt_brgy.Clear();
            txt_city.Clear();
            txt_region.Clear();
            txt_street.Clear();
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
            tb_fstatus.Text = "";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            getDocs.Text = "";
            tb_remarks.Text = "";
            tb_actuarial.Text = "";
            tb_approving.Text = lb.Content.ToString();
            acode.Content = "";
            getMaxcode();
            tb_code.Text = maxcode;
            proc.Content = "Addition of Franchise";
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
            plan_grid.ItemsSource = null;
            tb_company.Focus();
        }
        #endregion

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            //GetIPv4Address();
            //MessageBox.Show(strHostname  + "   " + strIPAddress);
            if (tb_company.Text == "")
            {
                getMaxcode();
                tb_code.Text = maxcode;              
                srchng();
                //tb_company.Focus();
                tb_company.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dtexpiry.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                tb_principal.Text = "0";
                tb_dependents.Text = "0";
                tb_employee_count.Text = "0";
                //tb_company.Focus();
            }
        }

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
    
        private void add_MouseEnter1(object sender, MouseEventArgs e)
        {
            //add_Copy.Foreground = Brushes.Black;
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
        private classes.binding Selectedsale, Selectedactuarial, SelectedEmail, SelectedProv, SelectedCompany;
        private void acb_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                Selectedsale = (classes.binding)acb.SelectedItem;                  
                acode.Content = Selectedsale._agent_code.ToString();
                tb_fcontact.Text = Selectedsale._contact_person_number.ToString();
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
                if(acb.SelectedItem == null)
                {
                    Selectedsale = (classes.binding)acb.SelectedItem;
                    acode.Content = "";
                }
                else
                {

                }            
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message, err.Source, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public string sql1, sql2, sql4, sql5, sql6, sql7, sql8, sql9, sql10;
        public DataTable zxc;
        public string sql, sql3;
        //public BackgroundWorker bgworker;
        public BackgroundWorker bg;
        loading l;
        public void srchng()
        {
            l = new loading();      
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            //bg.ProgressChanged += bg_ProgressChanged;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
            l.ShowDialog();
        }
       
        public List<classes.binding> cname = new List<classes.binding>();
        List<classes.binding> cname2 = new List<classes.binding>();
        List<classes.binding> cname3 = new List<classes.binding>();
        List<classes.binding> cname4 = new List<classes.binding>();
        List<classes.binding> cname5 = new List<classes.binding>();
        List<classes.binding> cname6 = new List<classes.binding>();
        List<classes.binding> cname7 = new List<classes.binding>();
        List<classes.binding> cname8 = new List<classes.binding>();
        List<classes.binding> cname9 = new List<classes.binding>();
        List<classes.binding> cname10 = new List<classes.binding>();
        List<classes.cities> lstcity;
        List<classes.regions> lstregion;
        classes.regions r;
        classes.cities c;
        public void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            cname = new List<classes.binding>();
            cname2 = new List<classes.binding>();
            cname3 = new List<classes.binding>();
            cname4 = new List<classes.binding>();
            cname5 = new List<classes.binding>();
            cname6 = new List<classes.binding>();
            cname7 = new List<classes.binding>();
            cname8 = new List<classes.binding>();
            cname9 = new List<classes.binding>();
            cname10 = new List<classes.binding>();

            classes.getData gdata = new classes.getData();

            r = new classes.regions();
            c = new classes.cities();
            lstregion = r.getlist();
            lstcity = c.getlist();
            DataTable lbData,actData, statData, planData, aOfficer, prov, companyName, docs, indData, tb_rnb;

            sql7 = "Select DISTINCT TRIM(company_name) as `company_name` from franchise_client " +
                    "union ALL SELECT DISTINCT TRIM(sa.partner_name) AS `company_name` FROM hmo_application sa WHERE sa.date_expiry > CURDATE() ORDER BY company_name asc";

            sql1 = "Select TRIM(CONCAT(sales,'    (',salesgroup,'-',LEFT(branchoffice,LOCATE(' ',branchoffice) - 1),') ' )) As `saless`,agent_code, contact from `agent_broker`";
            sql2 = "Select initial, name, email from ref_actuarial";
            sql3 = "Select * from ref_fstatus";
            sql4 = "Select * from program_type where status = '1'";
            sql5 = "Select * from ref_approving_officer";
            sql6 = "Select TRIM(hmo) as `hmo` from ref_providers";
            sql8 = "Select docs from ref_docs";
            sql9 = "Select * from ref_industry";
            sql10 = "Select * from ref_rnb";

            companyName = new DataTable();
            companyName = gdata.getdatasource(sql7);
            foreach (DataRow ctr3 in companyName.Rows)
            {
                cname7.Add(new classes.binding
                {
                    _company_name = ctr3["company_name"].ToString()
                });
                //System.Threading.Thread.Sleep(1);
            }

            docs = new DataTable();
            docs = gdata.getdatasource(sql8);
            foreach (DataRow ctr3 in docs.Rows)
            {
                cname8.Add(new classes.binding
                {
                    _remarks = ctr3["docs"].ToString()
                });
                //System.Threading.Thread.Sleep(1);
            }



            lbData = gdata.getdatasource(sql1);
            foreach (DataRow ctr in lbData.Rows)
            {
                cname.Add(new classes.binding
                {
                    _sales = ctr["saless"].ToString(),
                    _agent_code = ctr["agent_code"].ToString(),
                    _contact_person_number = ctr["contact"].ToString()
                });
            }

            actData = new DataTable();
            actData = gdata.getdatasource(sql2);
            foreach (DataRow ctr1 in actData.Rows)
            {
                cname2.Add(new classes.binding
                {
                    _actuarial = ctr1["initial"].ToString(),
                    _aname = ctr1["name"].ToString(),
                    _aemail = ctr1["email"].ToString()
                });             
            }

            statData = new DataTable();
            statData = gdata.getdatasource(sql3);
            foreach (DataRow ctr2 in statData.Rows)
            {
                cname3.Add(new classes.binding
                {
                    _fstatus = ctr2["status"].ToString()
                });

            }


            planData = new DataTable();
            planData = gdata.getdatasource(sql4);
            foreach (DataRow ctr2 in planData.Rows)
            {
                cname4.Add(new classes.binding
                {
                    _type_of_plan = ctr2["program_type"].ToString()
                });

            }

            aOfficer = new DataTable();
            aOfficer = gdata.getdatasource(sql5);
            foreach (DataRow ctr2 in aOfficer.Rows)
            {
                cname5.Add(new classes.binding
                {
                    _actuarial  = ctr2["initial"].ToString(),
                    _aname = ctr2["officer"].ToString()
                });

            }

            prov = gdata.getdatasource(sql6);
            foreach (DataRow ctr2 in prov.Rows)
            {
                cname6.Add(new classes.binding
                {                
                    _existing_provider = ctr2["hmo"].ToString()
                });

            }

            indData = new DataTable();
            indData = gdata.getdatasource(sql9);
            foreach (DataRow ctr3 in indData.Rows)
            {
                cname9.Add(new classes.binding
                {
                    _industry = ctr3["industry"].ToString()
                });
                //System.Threading.Thread.Sleep(1);
            }

            tb_rnb = new DataTable();
            tb_rnb = gdata.getdatasource(sql10);
            foreach (DataRow ctr4 in tb_rnb.Rows)
            {
                cname10.Add(new classes.binding
                {
                    _rnb = ctr4["rnb"].ToString()
                });
            }

        }

        public void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            acb.ItemsSource = cname;
            tb_actuarial.ItemsSource = cname2;
            tb_fstatus.ItemsSource = cname3;
            cb_plan.ItemsSource = cname4;
            cb_plan.SelectedIndex = 1;
            tb_approving.ItemsSource = cname5;
            tb_existingprovider.ItemsSource = cname6;
            tb_company.ItemsSource = cname7;
            sgroup.ItemsSource = cname7;
            tb_remarks.ItemsSource = cname8;
            tb_industry.ItemsSource = cname9;
            txt_city.ItemsSource = lstcity;
            txt_region.ItemsSource = lstregion;
            //dg_rnb.ItemsSource = cname10;
            l.Close();
            //tb_address.Focus();
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
            //srchng();
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


        private void homepage_Closing(object sender, CancelEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
        }


        view_request vr;

        private void tb_existingprovider_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedProv = (classes.binding)tb_existingprovider.SelectedItem;
            }
            catch (Exception err)
            {
            }
        }

        private void tb_company_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
               SelectedCompany = (classes.binding)tb_company.SelectedItem;
            }
            catch (Exception err)
            {
            }
        }

        private void tb_company_GotFocus_1(object sender, RoutedEventArgs e)
        {
            //srchng();
        }

        private void tb_existingprovider_GotFocus_1(object sender, RoutedEventArgs e)
        {
            //srchng();
        }

        private void tb_fstatus_DropDownClosed(object sender, EventArgs e)
        {

           
        }

 

        private void tb_remarks_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
          

            //string addSelect;
            //foreach(classes.binding x in getR)
            //{
            //    MessageBox.Show(x._remarks.ToString());
            //}
         

        }



        classes.binding cbox_item;
        public string get_cbox;
        public List<classes.binding> getR = new List<classes.binding>();
        private void cbox_Checked_1(object sender, RoutedEventArgs e)
        {
          
        }

        private void tb_remarks_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_remarks.Text.ToString().Contains("/"))
                {
                    tb_remarks.IsDropDownOpen = true;                
                    getDocs.Text +=  tb_remarks.Text.ToString();
                    //getDocs.Text += tb_remarks.Text.ToString();                    
                    tb_remarks.Text = null;

                }
            }
            catch (Exception err) { };
           
        }

        private void btn_docs_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Clear Document Submitted field?","Clearing field...",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                tb_remarks.Text = null;
                getDocs.Text = null;
            }
           
        }

        private void getDocs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb_remarks.Focus();
        }
        active_exp v = new active_exp();
        private void view_Click(object sender, RoutedEventArgs e)
        {
            v = new active_exp();
            v.ShowDialog();
        }

        private void tb_fstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tb_fstatus.SelectedIndex == 0)
                {
                    if (tb_years.SelectedDate > DateTime.Now.AddDays(30))
                    {
                        dtexpiry.SelectedDate = Convert.ToDateTime(dtapproval.SelectedDate).AddDays(30);
                    }
                    else
                    {
                        if (Convert.ToDateTime(tb_years.SelectedDate).ToString() == Convert.ToDateTime("0001-01-01").ToString())
                        {

                            dtexpiry.SelectedDate = Convert.ToDateTime(dtapproval.SelectedDate).AddDays(30);
                        }
                        else
                        {
                            MessageBox.Show("Exisiting HMO Expiry date cant be less than the Current Date!");
                            dtexpiry.SelectedDate = Convert.ToDateTime(dtapproval.SelectedDate).AddDays(30);
                            return;
                        }

                    }

                }
                else if (tb_fstatus.SelectedIndex == 1)
                {
                    if (tb_years.SelectedDate > DateTime.Now.AddDays(30))
                    {
                        dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(10);
                    }
                    else
                    {
                        if (Convert.ToDateTime(tb_years.SelectedDate).ToString() == Convert.ToDateTime("0001-01-01").ToString())
                        {
                            dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(10);
                        }
                        else
                        {
                            dtexpiry.SelectedDate = tb_years.SelectedDate;
                        }

                    }


                }


                else if (tb_fstatus.SelectedIndex == 2)
                {
                    dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(60);
                    if (tb_years.SelectedDate < dtexpiry.SelectedDate)
                    {                    
                        if(tb_years.SelectedDate < Convert.ToDateTime("01/01/2010"))
                        {
                            dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(60);
                        }
                        else
                        {
                            if(tb_years.SelectedDate < Convert.ToDateTime("01/01/2020"))
                            {
                                dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(60);
                            }
                            else
                            {
                                MessageBox.Show("Maximum days Extension", "Extension of Franchise Validity", MessageBoxButton.OK, MessageBoxImage.Information);
                                dtexpiry.SelectedDate = tb_years.SelectedDate;
                            }
                            
                        }                     
                    }
                }


                else if (tb_fstatus.SelectedIndex == 3)
                {
                    dtexpiry.SelectedDate = dtapproval.SelectedDate.Value.AddDays(90);
                }
                else if (tb_fstatus.SelectedIndex == 4)
                {
                    if (MessageBox.Show("Are you sure?", "Open Franchise Prompt!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (tb_years.SelectedDate == null)
                        {
                            acb.SelectedItem = null;
                            acb.Text = null;
                            acode.Content = null;
                            dtexpiry.SelectedDate = null;
                        }
                        else
                        {
                            if (tb_years.SelectedDate > DateTime.Today)
                            {
                                acb.SelectedItem = null;
                                acb.Text = "";
                                acode.Content = "";
                                tb_fcontact.Text = "";
                                getMaxcode();
                                tb_code.Text = maxcode;
                                dtexpiry.SelectedDate = null;
                                add.Visibility = Visibility.Visible;
                                update.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                acb.SelectedItem = null;
                                acb.Text = "";
                                acode.Content = "";
                                tb_fcontact.Text = "";
                                getMaxcode();
                                tb_code.Text = maxcode;
                                dtexpiry.SelectedDate = tb_years.SelectedDate;
                                add.Visibility = Visibility.Visible;
                                update.Visibility = Visibility.Hidden;
                            }
                        }

                    }

                }
                else if (tb_fstatus.SelectedIndex == 5)
                {
                    if (MessageBox.Show("Transfer Franchise?", "Franchise Transfer Prompt!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        acb.Text = "";
                        acode.Content = "";
                        tb_fcontact.Text = "";
                        getMaxcode();
                        tb_code.Text = maxcode;
                        dtapproval.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (tb_years.SelectedDate > DateTime.Now.AddDays(30))
                        {
                            dtexpiry.SelectedDate = DateTime.Now.AddDays(30);
                        }
                        else
                        {
                            dtexpiry.SelectedDate = tb_years.SelectedDate;
                        }
                        dtexpiry.SelectedDate = DateTime.Now.AddDays(30);
                        add.Visibility = Visibility.Visible;
                        update.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, err.Source, MessageBoxButton.OK, MessageBoxImage.Warning); };
        }



        private void ref_MouseEnter(object sender, MouseEventArgs e)
        {
            addref.Background = new SolidColorBrush(Color.FromRgb(168, 225, 251));
            addref.Foreground = Brushes.Blue;
        }

        private void ref_MouseLeave(object sender, MouseEventArgs e)
        {
            addref.Background = null;
            addref.Foreground = Brushes.Black;
        }

        win_addref ar;
        win_auth au;
        //public MainWindow mw;
        private void ref_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            au = new win_auth();           
            addref.Foreground = Brushes.White;
            if(au.ShowDialog() == true)
            { 
                ar = new win_addref();
                if (ar.ShowDialog() == true)
                {
                    srchng();
                }
            }                 
        }

        private void tb_actuarial_TextChanged(object sender, RoutedEventArgs e)
        {
            if(tb_actuarial.Text.ToString() == "")
            {
                act_email.Content = "";
            }
        }

        public SmtpClient smtp_server;
        public MailMessage thisEmail;
        public System.Net.Mail.Attachment attachment;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            smtp_server = new System.Net.Mail.SmtpClient();
            thisEmail = new System.Net.Mail.MailMessage();
            if (cb_email.IsChecked == false)
            {
                MessageBox.Show("Please check the box first before attaching a file!");
                return;
            }
            string filePath = string.Empty;
            string fileExt = string.Empty;
            //try
            //{
                OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
                file.Filter = "*.csv|*.xls|All Files|*.*";


                if (file.ShowDialog() == true)//if there is a file choosen by the user
                {
                    filePath = file.FileName;
                    attachment = new System.Net.Mail.Attachment(filePath);
                    thisEmail.Attachments.Add(attachment);
                    lb_filename.Content = filePath;
                }
            //}
            //catch(Exception err) {}
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (cb_email.IsChecked == false)
            {
                MessageBox.Show("Please check the box first!!");
                return;
            }
            if (MessageBox.Show("Remove attached file?", "WARNING!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //try
                //{
                if(lb_filename.Content.ToString() != "")
                {
                    thisEmail.Attachments.Clear();
                    lb_filename.Content = "";
                }
                    
                //}
                //catch (Exception err) { }
            }         
        }


        public ObservableCollection<classes.plan_count> b_data = new ObservableCollection<classes.plan_count>();
        public List<string> GVComboBox { get; set; }
        public List<string> rnb_list { get; set; }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            b_data.Add(new classes.plan_count()
            {
                _id = 0,
                _fcode = tb_code.Text.ToString()
            });
            //plan_grid.ItemsSource = b_data;
            try
            {
                plan_grid.ItemsSource = b_data;
            }
            catch(Exception err)
            {

                plan_grid.Items.Add(b_data);
                MessageBox.Show(err.Message, err.Source, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            //GVComboBox = new List<string>() { "Principal", "Dependent" };

            //rnb_list = new List<string>() { "Large Suite","Open Suite", "Regular Suite",
            //                                    "Small Suite","Large Private","Open Private",
            //                                    "Regular Private", "Small Private","Semi-Private",
            //                                    "Ward" };

            rnb_list = new List<string>();
            foreach(classes.binding x in cname10)
            {
                rnb_list.Add(x._rnb.ToString());
            }
            //rnbox.ItemsSource = GVComboBox;
            
        }

        classes.deleterecord dr = new classes.deleterecord();
        private void plan_grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                if(MessageBox.Show("Delete this record?", "Deletion of Plan Availment", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    classes.plan_count x;
                    x = (classes.plan_count)plan_grid.SelectedItem;

                    if (x._id == 0)
                    {
                        b_data.Remove(x);
                    }
                    else if (x._id > 0)
                    {
                        b_data.Remove(x);
                        dr.deleterecord_plan(x);
                    }
                    MessageBox.Show("DELETED!");
                }              
            }
        }

        private void cb_email_Checked(object sender, RoutedEventArgs e)
        {
            if(myEMAIL.Text.ToString() == "")
            {
                MessageBox.Show("Please set your email address first!!!");
                cb_email.IsChecked = false;
                myEMAIL.BorderBrush = Brushes.Black;
                myEMAIL.Focus();
            }
        }
    
        private void myEMAIL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Setup Email?", "EMAIL SETUP", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                myEMAIL.BorderBrush = null;
                tb_company.Focus();
            }

        }
        win_emails we;

        private void txt_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classes.cities c;
            classes.regions r;
            if (txt_city.SelectedItem != null) {
                c = (classes.cities)txt_city.SelectedItem;
                r = new classes.regions
                {
                    _region = c._region
                };

                txt_region.SelectedItem = r;


            }
        }

        private void plan_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void myEMAIL_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F3)
            {
                we = new win_emails();
                if(we.ShowDialog() == true)
                {
                    myEMAIL.Text = lb.Content.ToString();
                }
            }

            if(e.Key == Key.Enter)
            {
                if(MessageBox.Show("Setup Email?","EMAIL SETUP", MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    myEMAIL.BorderBrush = null;
                    tb_company.Focus();
                }
            }
        }

        private void tb_actuarial_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                SelectedEmail = (classes.binding)tb_actuarial.SelectedItem;
                act_email.Content = SelectedEmail._aemail.ToString();
                // acode.Content = Selectedsale._agent_code.ToString();
                         
            }
            catch (Exception err)
            {
            }
        }

        private void addref_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            addref.Foreground = Brushes.Blue;
        }

        public string xx;
        private void tb_company_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {

                //SelectedCompany = (classes.binding)tb_company.SelectedItem;
                //tb_company.Text = SelectedCompany._company_name.ToString();
                // acode.Content = Selectedsale._agent_code.ToString();         
            }
            catch (Exception err)
            {
            }
        }



        private void tb_existingprovider_TextChanged_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedProv = (classes.binding)tb_existingprovider.SelectedItem;
            }
            catch (Exception err)
            {
            }
        }

        private void tb_existingprovider_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            try
            {
                //SelectedProv = (classes.binding)tb_existingprovider.SelectedItem;
                // acode.Content = Selectedsale._agent_code.ToString();         
            }
            catch (Exception err)
            {
            }
        }

        private void add_Copy_Click(object sender, RoutedEventArgs e)
        {
            vr = new view_request();
            vr.Show();
            //vr.Owner = this;
        }

        private void add_MouseEnter(object sender, MouseEventArgs e)
        {
            add.Foreground = Brushes.Black;
        }

        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            exit_MouseLeftButtonUp(sender, e);
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
 
            br.Visibility = System.Windows.Visibility.Visible;
            addupdate = "ADD";
            update.Visibility = System.Windows.Visibility.Hidden;
            Cancel.Visibility = System.Windows.Visibility.Hidden;
            add.Visibility = System.Windows.Visibility.Visible;
            //add_Copy.Visibility = Visibility.Visible;
            dtrequest.Text = "";
            tb_company.Text = "";
            tb_industry.Text = "";

//            tb_address.Text = "";
            txt_bldg.Clear();
            txt_brgy.Clear();
            txt_city.Clear();
            txt_region.Clear();
            txt_street.Clear();

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
            tb_fstatus.Text = "";
            sgroup.Text = "";
            dtapproval.Text = "";
            dtexpiry.Text = "";
            tb_remarks.Text = "";
            tb_actuarial.Text = "";
            tb_approving.Text = lb.Content.ToString();
            acode.Content = "";
            getMaxcode();
            btn_docs.Visibility = Visibility.Hidden;
            view.Visibility = Visibility.Hidden;
            tb_code.Text = maxcode;
            dtrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtapproval.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtexpiry.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

    
    }
}
