using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWHC_FRANCHISING.classes
{
    public class accounts
    {
        public accounts() { }
        public string accountName { get; set; } 
        public DateTime? franchiseExpiry { get; set; } 
        public string franchiseStatus { get; set;}
        public string franchiseSubgroup { get; set;}


        public override string ToString()
        {
            return accountName;
        }

    }
}
