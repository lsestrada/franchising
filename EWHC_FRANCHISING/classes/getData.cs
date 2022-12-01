using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWHC_FRANCHISING.classes
{
    public class getData
    {
        private MySqlConnection mysqlConn;
        private MySqlCommand mysqlComm;
        //private String conn_string = "server=localhost;user=root;password=password;database=franchising";
        private String conn_string = "server=192.168.2.3;user=ewhealthcare;password=3@stw3sth3@lthc@r3;database=franchising";
        //Public connection_string As String = "server=db-offsite;user id=root;password=3astw3st;database=pnb"
        int rowsaffected;
        
        public DataTable getdatasource(string _sql)
        {
            mysqlConn = new MySqlConnection(conn_string);
            MySqlDataAdapter mAdapter;
            DataSet ds;
            DataTable dt;
            mysqlConn.Open();
            mAdapter = new MySqlDataAdapter(_sql, mysqlConn);
            mAdapter.SelectCommand.CommandTimeout = 6000;
            ds = new DataSet();
            mAdapter.Fill(ds);
            mysqlConn.Close();
            dt = ds.Tables[0];
            return dt;
        }

        
        public int executeCommand(string _sql)
        {
            MySqlCommand mysqlComm;           
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            mysqlComm = new MySqlCommand(_sql, mysqlConn);            
            return mysqlComm.ExecuteNonQuery();
        }
    }
}
 