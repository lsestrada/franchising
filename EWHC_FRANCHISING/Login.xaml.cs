using EWHC_FRANCHISING.classes;
using Limilabs.Client.SMTP;
using Limilabs.Mail.Headers;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using static System.Runtime.CompilerServices.RuntimeHelpers;
namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : System.Windows.Window
    {
        public Login()
        {
            InitializeComponent();



            //string[] from;
            //string[] to;
            //string subject;
            //string htmlBody;

            //from = new string[] { "rfm_it03@eastwesthealthcare.com.ph", "Leoleo" };
            //to = new string[] { "ls.estrada@eastwesthealthcare.com.ph"};

            //subject = "Sample Only";
            //htmlBody = "<h1>Good Day! </h1> <br> " +
            //                 "requested for franchise of " +
            //                 "<p>Thanks!</p>";

            //if ((new smtpconfig()).sendEmail(from, to, subject, htmlBody, "").Status == SendMessageStatus.Success)
            //{
            //    MessageBox.Show("Success");
            //}
            //else MessageBox.Show("Failed");


        }
        #region  DECLARATIONS
        public string userlevel;
        DropShadowEffect eff = new DropShadowEffect();
        BackgroundWorker bgworker;
        MySqlDataReader mysqlDr;
        MainWindow mw;
        public int zxc = 30;
        public int getthis;
        MainWindow hp;
        sales_homepage spp;
        admin_window aw;
        splash spl;
        public string uname2_, dept;
        classes.insert_logs il = new classes.insert_logs();
        public string passname, pname, passid;
        classes.binding bind = new classes.binding();
        users currentUser;
        #endregion

        #region EFFECTS ON LOGIN
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_login.Background = new SolidColorBrush(Color.FromRgb(1, 154, 151));
            lbl_login.BorderBrush = Brushes.Black;
            lbl_login.Foreground = Brushes.White;
        }

        private void lbl_login_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_login.Foreground = Brushes.Black;
            lbl_login.Background = null;
            lbl_login.BorderBrush = Brushes.White;
        }

        private void lbl_login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbl_login.Foreground = Brushes.Black;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_user.Effect = eff;
            if (tb_user.Text == null && tb_pass.Password == null)
            {
                uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                uuname.FontWeight = FontWeights.Normal;
                uuname.Foreground = Brushes.Gray;
                uuname.FontSize = 20;
            }
            else
            {
                uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                uuname.FontSize = 10;
                uuname.Foreground = Brushes.Black;
                uuname.FontWeight = FontWeights.ExtraBold;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            tb_user.Effect = null;
            if (tb_user.Text == null || tb_user.Text == "")
            {
                uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                uuname.FontWeight = FontWeights.Normal;
                uuname.Foreground = Brushes.Gray;
                uuname.FontSize = 20;
                //lbl.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                //lbl.FontSize = 10;
            }
            else
            {
                uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                uuname.FontSize = 10;
                uuname.Foreground = Brushes.Black;
                uuname.FontWeight = FontWeights.ExtraBold;
            }

        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            tb_pass.Effect = eff;
            if (tb_user.Text != null)
            {
                ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                ppword.FontSize = 10;
                ppword.Foreground = Brushes.Black;
                ppword.FontWeight = FontWeights.ExtraBold;
            }
            else
            {
                if (tb_pass.Password == null || tb_pass.Password == "")
                {

                    ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                    ppword.FontWeight = FontWeights.Normal;
                    ppword.Foreground = Brushes.Gray;
                    ppword.FontSize = 20;
                    //lbl.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                    //lbl.FontSize = 10;
                }
            }
        }

        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            tb_pass.Effect = null;
            if (tb_pass.Password == null || tb_pass.Password == "")
            {
                ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                ppword.FontSize = 20;
                ppword.Foreground = Brushes.Gray;
                ppword.FontWeight = FontWeights.Normal;
                //lbl.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                //lbl.FontSize = 10;
            }
            else
            {
                ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                ppword.FontSize = 10;
                //ppword.Foreground = Brushes.White;
                ppword.FontWeight = FontWeights.ExtraBold;
            }
        }


        #endregion

        #region login function
        private void lbl_login_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lbl_login.Foreground = Brushes.White;
            logins();
        }
        public string getuname;
        public void logins()
        {
            try
            {
                MySqlConnection mysqlConn = new MySqlConnection();
                MySqlCommand mysqlComm = new MySqlCommand();
                string CmdString;
                //mysqlConn.ConnectionString = "Server=localhost;user=root;password=password;Database=franchising";
                //mysqlConn.ConnectionString = "server=192.168.254.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";
                 mysqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString;

               //  mysqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["conStringCloud"].ConnectionString;
                mysqlComm.Connection = mysqlConn;


                // mysqlComm.CommandText = "Select username,password,user_level,CONCAT(sales,'(',salesgroup,'-',LEFT(branchoffice,LOCATE(' ',branchoffice) - 1),') ' ) As `saless`, lastname, firstname, agent_code from agent_broker where username = '" + tb_user.Text.Replace("'", "''") + "' AND password = '" + tb_pass.Password.Replace("'", "''") + "' ";
                CmdString = "select username, password, lastname, firstname, department, user_level, email, id " +
                            "from user_login " +
                            "where username='" + tb_user.Text + "'" +
                            "and password='" + tb_pass.Password + "'";
                mysqlComm.CommandText = CmdString;
                mysqlComm.CommandType = CommandType.Text;
                mysqlConn.Open();
                mysqlDr = mysqlComm.ExecuteReader();
                mysqlDr.Read();

                if (mysqlDr.HasRows)
                {
                    if (mysqlDr["username"].ToString() == tb_user.Text && mysqlDr["password"].ToString() == tb_pass.Password)
                    {
                        dept = "";
                        //
                        bind._username = tb_user.Text;
                        bind._password = tb_pass.Password;
                        bind._name = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString();
                        pname = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString();
                        //passname = mysqlDr["saless"].ToString();
                        //passid = mysqlDr["agent_code"].ToString();
                        dept = mysqlDr["department"].ToString();

                        currentUser = new users()
                        {
                            userLevel = mysqlDr["user_level"].ToString(),
                            userName = tb_user.Text,
                            firstName = mysqlDr["firstname"].ToString(),
                            lastName = mysqlDr["lastname"].ToString(),
                            email = mysqlDr["email"].ToString(),
                            id = int.Parse(mysqlDr["id"].ToString())

                        };

                        il.insert_logs1(bind);
                        hideItems();
                        bgworker = new BackgroundWorker();
                        bgworker.DoWork += bgworker_DoWork;
                        bgworker.ProgressChanged += bgworker_ProgressChanged;
                        bgworker.RunWorkerCompleted += bgworker_RunWorkerCompleted;
                        bgworker.WorkerReportsProgress = true;
                        bgworker.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username / Password. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username / Password. Please try again.");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        #endregion

        #region PERCENTAGE LOADING
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                int x = RandomNumber(1, 2);
                bgworker.ReportProgress(i);
                i += x;
                System.Threading.Thread.Sleep(zxc);
            }
        }
        private void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pct.Content = e.ProgressPercentage.ToString() + "%";
            getthis = e.ProgressPercentage;
            if (getthis > 93)
            {
                zxc = 800;
                if (getthis > 98)
                {
                    //plswt.FontWeight = FontWeights.Bold;
                    plswt.Foreground = Brushes.Green;
                    plswt.Content = "Verification Success";
                    pct.Content = "100%";
                }
            }

        }
        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            showWin();
        }
        private void showWin()
        {
            System.Threading.Thread.Sleep(900);
            hp = new MainWindow();
            spp = new sales_homepage();
            aw = new admin_window();

            hp.currentUser = currentUser;

            this.Close();
            //if (currentUser.userLevel == "SALES")
            //{
            //    hp.grdDateRequest.Height = 0;
            //    hp.grdDetails.Height = 0;
            //    hp.grdButtons.Height = 0;
            //    hp.grdApproval.Height = 0;

            //    hp.addref.Visibility = Visibility.Hidden;
            //    hp.addrefimg.Visibility= Visibility.Hidden;

            //    hp.Show();
            //}
            //else
            //{
            //    hp.addref.Visibility = Visibility.Visible;
            //    hp.addrefimg.Visibility = Visibility.Visible;

            //    searchwin sw = new searchwin();
            //    sw.currentUser = this.currentUser;
            //    sw._sql = "Select c.code, c.date_request,c.company_name, c.franchisee, c.franchisee_contact,c.industry," +
            //            " c.complete_address, c.contact_numbers, c.contact_person, c.designation, h.contract_expiry, " +
            //            " h.existing_hmo, h.prins_count,h.deps_count, h.employee_count, rf.status,c.subgroup, s.date_of_approval, " +
            //            " s.expiry_date, s.approving_officer, s.remarks, s.actuarial, t.program_type, " +
            //            " c.add_bldg, c.add_street, c.add_brgy, c.add_city, c.add_region, u.email, a.username as approver " +
            //            " from franchise_client c " +
            //            " inner join user_login u on (c.created_by_userlogin_autokey = u.id) " +
            //            " INNER JOIN franchise_history h ON (c.code = h.franchise_key) " +
            //            " INNER JOIN ref_franchise_status s ON (h.franchise_key = s.code) " +
            //            " INNER JOIN ref_type_of_plan t ON (c.code = t.code) " +
            //            " LEFT JOIN ref_fstatus rf ON (s.franchise_status = rf.autocode) " +
            //            " LEFT JOIN user_login a on (a.id = s.approving_officer_id) " +
            //            " where rf.`status` = 'Pending' " +
            //            " order by  s.expiry_date desc ";


            //    //      hp.Show();

            //    sw.searchfunc();
            //    sw.ShowDialog();
            //}



                //mw.uname_ = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString();
                //uname2_ = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString(); 
                //hp.uname.Content = "User: " + uname2_;
                //hp.tb_actuarial.Text = passname;
                //hp.acb.SelectedItem = passname;
                // hp.acode.Content = passid;
                hp.tb_actuarial.Text = pname;
                //hp.lb.Content = pname;
                userlevel = "1";
               
            
            if (dept == "0")
                {

                    spp.Show();
                    spp.testname.Content = passname;
                    spp.testcode.Content = passid;
                    //spp.sales.Content = pname;


                }
                else if (dept == "A")
                {
                    aw.Show();
                    userlevel = "A";
                }


                //hp.qwe.Content = "User:" + passname;
                //hp.getpass1 = passname;
            }

            private int RandomNumber(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max);
            }
            #endregion

            #region HIDE ITEMS ON SUCCESSFUL LOGIN
            private void hideItems()
            {
                tb_user.Visibility = Visibility.Hidden;
                tb_pass.Visibility = Visibility.Hidden;
                img1.Visibility = Visibility.Hidden;
                img2.Visibility = Visibility.Hidden;
                lbl_login.Visibility = Visibility.Hidden;
                esc.Visibility = Visibility.Hidden;
                plswt.Visibility = Visibility.Visible;
                ellipse.Visibility = Visibility.Visible;
                pct.Visibility = Visibility.Visible;
                uuname.Visibility = System.Windows.Visibility.Hidden;
                ppword.Visibility = System.Windows.Visibility.Hidden;
            }
            #endregion

            #region press ESCAPE to exit form
            private void tb_user_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    exx();
                }
                if (e.Key == Key.Enter)
                {
                    logins();
                }

            }

            private void exx()
            {
                if (MessageBox.Show("Do you really want to Exit?", "CLOSING THE FORM", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            private void tb_pass_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    if (MessageBox.Show("Do you really want to Exit?", "CLOSING THE FORM", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                }

                if (e.Key == Key.Enter)
                {
                    logins();
                }
            }

            private void Window_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    if (MessageBox.Show("Do you really want to Exit?", "CLOSING THE FORM", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                }
            }
            #endregion

            #region text box functions
            private void uuname_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (tb_user.Text != null || tb_user.Text != "")
                {
                    uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                    uuname.FontSize = 10;
                    uuname.Foreground = Brushes.Black;
                    uuname.FontWeight = FontWeights.ExtraBold;
                    tb_user.Focus();
                }
                else
                {
                    uuname.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                    uuname.FontWeight = FontWeights.Normal;
                    uuname.Foreground = Brushes.Gray;
                    uuname.FontSize = 20;
                }
            }

            private void ppword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (tb_pass.Password != null || tb_pass.Password != "")
                {
                    ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                    ppword.FontSize = 10;
                    ppword.Foreground = Brushes.Black;
                    ppword.FontWeight = FontWeights.ExtraBold;
                    tb_pass.Focus();
                }
                else
                {
                    ppword.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                    ppword.FontWeight = FontWeights.Normal;
                    ppword.Foreground = Brushes.Gray;
                    ppword.FontSize = 20;
                }
            }

            private void img1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                uuname_MouseLeftButtonDown(sender, e);
            }

            private void img2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                ppword_MouseLeftButtonDown(sender, e);
            }
            #endregion

            #region click to Exit
            private void esc_MouseEnter(object sender, MouseEventArgs e)
            {
                esc.Foreground = Brushes.Red;
            }

            private void esc_MouseLeave(object sender, MouseEventArgs e)
            {
                esc.Foreground = Brushes.Gray;
            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                tb_user.Focus();
            }

            private void tb_user_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (tb_user.Text.ToString() == "ADMIN")
                {
                    loginyouraccount.Content = "ADMIN LOGIN!";
                    loginyouraccount.FontWeight = FontWeights.ExtraBold;
                    loginyouraccount.Foreground = Brushes.Blue;
                    lbl_login.BorderBrush = Brushes.Blue;
                }
                else
                {
                    loginyouraccount.Content = "LOGIN YOUR ACCOUNT";
                    loginyouraccount.FontWeight = FontWeights.Bold;
                    loginyouraccount.Foreground = new SolidColorBrush(Color.FromRgb(220, 85, 79));
                    lbl_login.BorderBrush = Brushes.White;
                }
            }

            private void esc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                exx();
            }
            #endregion

        }
    }



