using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWHC_FRANCHISING.classes;
using System.Data;
namespace EWHC_FRANCHISING.classes
{

    


    public class users
    {
        public users() { }
        
        public int id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string userLevel { get; set; }
        public string email { get; set; }

        public string getFullName()
        {
            return firstName + " " + lastName;
        }
        
        
        
        private DataRow getDataRow(DataTable dt)
        {
            return dt.Rows[0];

        }

        public List<users> getUsers(string query) 
        { 
            List<users> users = new List<users>();

            DataTable dt;
            dt = (new getData()).getdatasource(query);
            foreach (DataRow row in dt.Rows)
            {
                users.Add(new classes.users()
                { 
                    userName = row["username"].ToString(),
                    lastName = row["lastname"].ToString(),
                    firstName = row["firstname"].ToString(),
                    userLevel = row["user_level"].ToString(),
                    email = row["email"].ToString()
                });
            }
            return users;

        }
        
    }


    static class userInfo
    {
        public static int user_level;
    }


}
