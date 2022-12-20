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
    public class insertrecord
    {

        private MySqlConnection mysqlConn;
        private MySqlCommand mysqlComm;
        private String conn_string = ConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString;
        //private String conn_string = "server=localhost;user=root;password=password;database=franchising";
        //Public connection_string As String = "server=db-offsite;user id=root;password=3astw3st;database=pnb"
        public users currentUser;
        int rowsaffected;
        MainWindow mw;
        public string getacode;
        public int insertrecord_fclient(classes.binding _sql)
        {
            string sql;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sql = " INSERT INTO franchise_client (code, franchisee, agent_code, franchisee_contact, " +
                   " company_name, industry, contact_person, " +
                   " designation, complete_address, contact_numbers, subgroup, add_bldg, add_street, " +
                   " add_brgy, add_city, add_region, created_by_userlogin_autokey) " +
                   " VALUES( '" + _sql._code + "', " +
                   " '" + _sql._franchisee.Replace("'", "''") + "' , '" + getacode + "', '" 
                   + _sql._fcontact_person_no.Replace("'", "''") + "' " + 
                   ", '" + _sql._company_name.Replace("'", "''") + "', " +
                   " '" + _sql._industry.Replace("'", "''") + "', '" 
                   + _sql._contact_person.Replace("'", "''") + "', '" 
                   + _sql._designation.Replace("'", "''") + "'" +
                   " , '" + _sql._address.Replace("'", "''") + "', " +
                   " '" + _sql._contact_person_number.Replace("'", "''") + "' , '" + _sql._subgroup.Replace("'", "''") + "', " +
                   " '" + _sql._add_bldg + "','" + _sql._add_street + "','" + _sql._add_brgy + "','" + _sql._add_city + "','" + _sql._add_region + "', " +
                   " '" + currentUser.id + "')";
            
            mysqlComm = new MySqlCommand(sql, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
           // MessageBox.Show("Franchise Added!");
          

            //-***************-
        }
        //-***************-
        public int insertrecord_fhistory(classes.binding _sql)
            {
            string sqlhistory;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sqlhistory = " INSERT INTO franchise_history (franchise_key, existing_hmo, years_with_hmo, " +
                   " prins_count, deps_count, employee_count, contract_expiry)" +
                   " VALUES( '" + _sql._code + "', '" + _sql._existing_provider.Replace("'", "''") + "' , " +
                   " '" + _sql._contract_expiry.ToString("yyyy-MM-dd") + "' , '" + _sql._prins_count + "', " +
                   " '" + _sql._deps_count + "', '" + _sql._employee_count + "', '" + _sql._contract_expiry.ToString("yyyy-MM-dd") + "' ) ";
            mysqlComm = new MySqlCommand(sqlhistory, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
           // MessageBox.Show("Franchise Added!");
           
            }
        //-***************-
        public int insertrecord_fstatus(classes.binding _sql)
        {
            string sqlstatus;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
      
            sqlstatus = " INSERT INTO ref_franchise_status (code, franchise_status, expiry_date, remarks) " +
                        " VALUES('" + _sql._code + "', '" + _sql._fstatus.Replace("'", "\'") + "' , " +
                        " '" + _sql._expiry_date.ToString("yyyy-MM-dd") + "', " +
                        " '" + _sql._remarks.Replace("'", "\'") + "')";
            
            mysqlComm = new MySqlCommand(sqlstatus, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
            // MessageBox.Show("Franchise Added!");
            
        }
        //-***************-
        public int insertrecord_fpayment(classes.binding _sql)
        {
            string sqlpayment;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sqlpayment = " INSERT INTO ref_type_of_plan (code, program_type) " +
            " VALUES( '" + _sql._code + "', '" + _sql._type_of_plan + "')";
            mysqlComm = new MySqlCommand(sqlpayment, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();        
            return rowsaffected;
            //MessageBox.Show("Franchise Added!");
        }  
        //-***************-
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
        public int insertrecord_tracking(classes.binding _sql)
        {
            GetIPv4Address();
            string sqltracking;
            //string ipAddress = "";
            string x = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress[] address = hostEntry.AddressList;
            //ipAddress = address.GetValue(1).ToString(); 
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();

            sqltracking = "INSERT INTO transaction_tracking " +
                           " (code, company_name, date_request, industry, address, contact, contact_no, " +
                           " designation, existing_hmo, years_with_hmo, type_of_plan, principal_count, " +
                           " dependents_count, employee_count, franchisee, franchisee_contact, franchise_status, " +
                           " approval_date, expiry_date, remarks, actuarial, " +
                           " date_editted, pc_ip_address, subgroup) " +
                           " VALUES ( '" + _sql._code + "', '" + _sql._company_name.Replace("'", "''") + "' " +
                           " , '" + _sql._request_date.ToString("yyyy-MM-dd") + "', '" + _sql._industry.Replace("'", "''") + "' " +
                           " , '" + _sql._address.Replace("'", "''") + "', '" + _sql._contact_person.Replace("'", "''") + "', '"
                           + _sql._contact_person_number.Replace("'", "''") + "', " +
                           " '" + _sql._designation.Replace("'", "''") + "', '" + _sql._existing_provider.Replace("'", "''") + "', '"
                           + _sql._contract_expiry.ToString("yyyy-mm-dd")  + "', '" + _sql._type_of_plan + "', '"
                           + _sql._prins_count + "', '" + _sql._deps_count + "', '" + _sql._employee_count + "', " +
                           " '" + _sql._franchisee.Replace("'", "''") + "', '" + _sql._fcontact_person_no.Replace("'", "''") + "', " +
                           " '" + _sql._fstatus.Replace("'", "''") + "', '" + _sql._approval_date.ToString("yyyy-MM-dd") + "', '" + _sql._expiry_date.ToString("yyyy-MM-dd") + "', " +
                           " '" + _sql._remarks.Replace("'", "''") + "', '" + _sql._actuarial.Replace("'", "''") + "', " +
                           " '" + x + "', '" + "PC Name: " + strHostname + " | IP Address: " + strIPAddress + "', '" + _sql._subgroup.Replace("'", "''") + "' )";         
            mysqlComm = new MySqlCommand(sqltracking, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
        }
        //-***************-
        public int insertrecord_trackcode(classes.binding _sql)
        {
            string sqltrackcode;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sqltrackcode = "INSERT INTO track_code (code) VALUES( '" + _sql._code + "' )";        
            mysqlComm = new MySqlCommand(sqltrackcode, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();        
            return rowsaffected;
        }


        //-***************-
        public int insertrecord_plan(classes.plan_count _sql)
        {
            string sqlplan;
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();
            sqlplan = " INSERT INTO ref_plan_count (fcode, description, level, " +
                   " rnb, mbl, count)" +
                   " VALUES( '" + _sql._fcode + "', '" + _sql._description.Replace("'", "''") + "' , " +
                   " '" + _sql._level.ToString() + "' , '" + _sql._rnb + "', " +
                   " '" + _sql._mbl + "', '" + _sql._count + "' ) ";
            mysqlComm = new MySqlCommand(sqlplan, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
            // MessageBox.Show("Franchise Added!");

        }

        //-***************-



        public int insertrecord_request(classes.binding _sql)
        {
            GetIPv4Address();
            string sqlrequest;
            //string ipAddress = "";
            string x = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress[] address = hostEntry.AddressList;
            //ipAddress = address.GetValue(1).ToString(); 
            mysqlConn = new MySqlConnection(conn_string);
            mysqlConn.Open();

            sqlrequest = "INSERT INTO franchise_request " +
                           " (code, company_name, date_request, industry, address, add_bldg, add_brgy, add_city, add_region, add_street, contact, contact_no, " +
                           " designation, existing_hmo, years_with_hmo, type_of_plan, principal_count, " +
                           " dependents_count, employee_count, franchisee, franchisee_contact, franchise_status, " +
                           " approval_date, expiry_date, remarks, agent_code, approving_officer, " +
                           "  pc_ip_address, subgroup) " +
                           " VALUES ( '" + _sql._code + "', '" + _sql._company_name.Replace("'", "''") + "' " +
                           " , '" + _sql._request_date.ToString("yyyy-MM-dd") + "', '" + _sql._industry.Replace("'", "''") + "' " +
                           " , '" + _sql._address.Replace("'", "''") + "', " +
                           "'" + _sql._add_bldg + "','" + _sql._add_brgy + "','"  + _sql._add_city + "','" + _sql._add_region + "','" +_sql._add_street + "', " + 
                           "'" + _sql._contact_person.Replace("'", "''") + "', '"
                           + _sql._contact_person_number.Replace("'", "''") + "', " +
                           " '" + _sql._designation.Replace("'", "''") + "', '" + _sql._existing_provider.Replace("'", "''") + "', '"
                           + _sql._contract_expiry.ToString("yyyy-mm-dd")  + "', '" + _sql._type_of_plan + "', '"
                           + _sql._prins_count + "', '" + _sql._deps_count + "', '" + _sql._employee_count + "', " +
                           " '" + _sql._franchisee.Replace("'", "''") + "', '" + _sql._fcontact_person_no.Replace("'", "''") + "', " +
                           " '" + _sql._fstatus.Replace("'", "''") + "', '" + _sql._approval_date.ToString("yyyy-MM-dd") + "', '" + _sql._expiry_date.ToString("yyyy-MM-dd") + "', " +
                           " '" + _sql._remarks.Replace("'", "''") + "', '" + getacode + "', '" + _sql._approving_officer.Replace("'", "''") + "'," +
                           " '" + "PC Name: " + strHostname + " | IP Address: " + strIPAddress + "', '" + _sql._subgroup.Replace("'", "''") + "' )";
            mysqlComm = new MySqlCommand(sqlrequest, mysqlConn);
            mysqlComm.CommandType = CommandType.Text;
            rowsaffected = mysqlComm.ExecuteNonQuery();
            mysqlConn.Close();
            return rowsaffected;
        }
    }
}
