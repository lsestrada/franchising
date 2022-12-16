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


        public string getFullName()
        {
            return firstName + " " + lastName;
        }
        
        
        private DataRow getDataRow(DataTable dt)
        {
            return dt.Rows[0];

        }

        
        



    
        
    }
}
