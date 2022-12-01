using MySql.Data.MySqlClient;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
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
                mysqlConn.ConnectionString = "server=192.168.2.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";
                mysqlComm.Connection = mysqlConn;


                // mysqlComm.CommandText = "Select username,password,user_level,CONCAT(sales,'(',salesgroup,'-',LEFT(branchoffice,LOCATE(' ',branchoffice) - 1),') ' ) As `saless`, lastname, firstname, agent_code from agent_broker where username = '" + tb_user.Text.Replace("'", "''") + "' AND password = '" + tb_pass.Password.Replace("'", "''") + "' ";
                CmdString = "select username, password, lastname, firstname, department " + 
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
            
            //mw.uname_ = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString();
            //uname2_ = mysqlDr["firstname"].ToString() + " " + mysqlDr["lastname"].ToString(); 
            this.Close();
            //hp.uname.Content = "User: " + uname2_;
                hp.Show();
                //hp.tb_actuarial.Text = passname;
                //hp.acb.SelectedItem = passname;
                // hp.acode.Content = passid;
                hp.tb_actuarial.Text = pname;
                //hp.lb.Content = pname;
                userlevel = "1";
       if(dept == "0")
            {
                
                spp.Show();
                spp.testname.Content = passname;
                spp.testcode.Content = passid;
                //spp.sales.Content = pname;
                

            }
            else if(dept == "A")
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
                Application.Current.Shutdown();
            }
        }
        private void tb_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (MessageBox.Show("Do you really want to Exit?", "CLOSING THE FORM", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
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
                    Application.Current.Shutdown();
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

