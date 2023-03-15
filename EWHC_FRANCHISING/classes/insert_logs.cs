using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EWHC_FRANCHISING.classes
{
    public class insert_logs
    {

        private MySqlConnection mysqlConn;
        private MySqlCommand mysqlComm;
        //private String conn_string = "Server=localhost;user=root;password=password;database=franchising";
        private String conn_string = ConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString;
      //  private String conn_string = ConfigurationManager.ConnectionStrings["conStringCloud"].ConnectionString;

        //private String conn_string = "server=192.168.254.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";

        int rowsaffected;
        MainWindow mw;
        public string getacode;

        public int insert_logs1(classes.binding _sql)
        {
            GetIPv4Address();
            string sql;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sql = "INSERT INTO users_login_logs (name, username, password, ip_address) " +
                "VALUES ( '" + _sql._name + "', '" + _sql._username + "', '" + _sql._password + "',  '" + "PC Name: " + strHostname + " | IP Address: " + strIPAddress + "') ";
            mysqlComm = new MySqlCommand(sql, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
        }
        public string getNameIP;
        public string strHostname;
        public string strIPAddress;
        public string getNamenIP;
        private void GetIPv4Address()
        {
            string getIPname;
            getIPname = String.Empty;
            strHostname = System.Net.Dns.GetHostName();
            //Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName);
            //ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork
            System.Net.IPHostEntry iphe = System.Net.Dns.GetHostEntry(strHostname);
            foreach (System.Net.IPAddress ipheal in iphe.AddressList)
            {
                //if ( ipheal.AddressFamily = 
                if (ipheal.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    getIPname = ipheal.ToString();
                }
            }
            strIPAddress = getIPname;
        }
    }
}