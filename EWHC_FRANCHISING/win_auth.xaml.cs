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
    /// Interaction logic for win_auth.xaml
    /// </summary>
    public partial class win_auth : Window
    {
        public win_auth()
        {
            InitializeComponent();
        }

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



        private void btn_login_Click(object sender, RoutedEventArgs e)
        {          
                if (tb_user.Text.ToString() == "" || tb_pw.Password == "")
                {
                    MessageBox.Show("Please Enter your Username/Password!!!");
                    return;
                }
                else
                {
                    logins();
                }            
        }

        private void tb_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tb_user.Text.ToString() == "" && tb_pw.Password == "")
                {
                    MessageBox.Show("Please Enter your Username/Password!!!");
                    return;
                }
                else
                {
                    logins();
                }
            }
        }

        private void tb_pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tb_user.Text.ToString() == "" || tb_pw.Password == "")
                {
                    MessageBox.Show("Please Enter your Username/Password!!!");
                    return;
                }
                else
                {
                    logins();
                }
            }
        }

        public string getuname;

        public void logins()
        {
          
            try
            {
                MySqlConnection mysqlConn = new MySqlConnection();
                MySqlCommand mysqlComm = new MySqlCommand();
                //mysqlConn.ConnectionString = "Server=localhost;user=root;password=password;Database=franchising";
                //mysqlConn.ConnectionString = "server=192.168.254.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";
                mysqlConn.ConnectionString = "server=192.168.2.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";
                mysqlComm.Connection = mysqlConn;
                mysqlComm.CommandText = "Select * from user_login where username = '" + tb_user.Text.Replace("'", "''") + "' AND password = '" + tb_pw.Password.Replace("'", "''") + "' ";
                mysqlComm.CommandType = CommandType.Text;
                mysqlConn.Open();
                mysqlDr = mysqlComm.ExecuteReader();
                mysqlDr.Read();
                if (mysqlDr.HasRows)
                {
                    if (mysqlDr["username"].ToString() == tb_user.Text && mysqlDr["password"].ToString() == tb_pw.Password)
                    {
                        this.DialogResult = true;
                        this.Close();
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

      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_user.Focus();
        }
    }
}
