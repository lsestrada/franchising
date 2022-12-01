using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWHC_FRANCHISING.classes
{
    public class plan_count
    {
        private string  fcode,description, level, rnb, count;
        private Double mbl, amount;
        private int id;


        public int _id
        {
            get { return id;  }
            set { id = value; }
        }


        public string _fcode
        {
            get { return fcode; }
            set { fcode = value; }
        }

        public string _description
        {
            get { return description; }
            set { description = value; }
        }

        public string _level
        {
            get { return level; }
            set { level = value; }
        }

        public string _rnb
        {
            get { return rnb; }
            set { rnb = value; }
        }

        public string _count
        {
            get { return count; }
            set { count = value; }
        }

        public Double _mbl
        {
            get { return mbl; }
            set { mbl = value; }
        }

    }
}
