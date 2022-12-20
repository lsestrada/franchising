using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
namespace EWHC_FRANCHISING.classes
{
    class regions
    {
        private string region, city;

        public string _region {
            get { return region; }
            set { region = value; }
        }
        public string _city
        {
            get { return city; }
            set { city = value; }
        }

        public List<regions> getlist() {
            classes.getData gd;
            DataTable dt;
            List<regions> lst;
            gd = new classes.getData();
            dt = gd.getdatasource("select DISTINCT region_key  from ref_province order by region_key, province ");

            lst = new List<classes.regions>();
            foreach (DataRow dr in dt.Rows)
            {
              //  MessageBox.Show(dr["province"].ToString());
                lst.Add(new classes.regions {
                    _region = dr[0].ToString().ToUpper()
                   
              }  );
            }


            return lst;
        }
        public override string ToString() {
            return _region;
        } 

    }
    class cities
    {
        private string region, city;

        public string _region
        {
            get { return region; }
            set { region = value; }
        }
        public string _city
        {
            get { return city; }
            set { city = value; }
        }

        public List<cities> getlist(string region = "")
        {
            classes.getData gd;
            DataTable dt;
            List<cities> lst;
            string q_region;
            q_region = "";
            if (!region.Equals("")) {
                q_region = " where region_key = '" + region + "' ";
            }


            gd = new classes.getData();
            dt = gd.getdatasource("select region_key, province " +
                                  "from ref_province " +
                                  q_region + 
                                  "order by region_key, province ");

            lst = new List<classes.cities>();
            foreach (DataRow dr in dt.Rows)
            {
                //  MessageBox.Show(dr["province"].ToString());
                lst.Add(new classes.cities
                {
                    _city = dr["province"].ToString().ToUpper(),
                    _region = dr["region_key"].ToString().ToUpper()

                });
            }


            return lst;
        }
        public override string ToString()
        {
            return _city;
        }

    }
}
