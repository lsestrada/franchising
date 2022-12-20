using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWHC_FRANCHISING.classes
{
    public class subgroups : accounts
    {
        public subgroups() { }
        public string subgroupName { get; set; }

        public override string ToString()
        {
            return subgroupName;
        }
    }
}
