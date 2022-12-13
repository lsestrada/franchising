using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace EWHC_FRANCHISING.classes
{
    public class deleterecord
    {
        private MySqlConnection mysqlConn;
        private MySqlCommand mysqlComm;
        private String conn_string = ConfigurationManager.ConnectionStrings["conStringCloud"].ConnectionString;

        //private String conn_string = "server=localhost;user=root;password=password;database=franchising";
        //Public connection_string As String = "server=db-offsite;user id=root;password=3astw3st;database=pnb"
        int rowsaffected;

        public int deleterecord_plan(classes.plan_count _sql)
        {
            string sqlplan;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sqlplan = " DELETE FROM ref_plan_count " +
                   " where autokey = '" + _sql._id + "'";
            mysqlComm = new MySqlCommand(sqlplan, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
        }

    }
}
