using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWHC_FRANCHISING.classes
{
    public class binding
    {
        private string company_name, industry, address,contact_person, contact_person_number, designation, existing_provider;
        private string  type_of_plan, franchisee, fcontact_person_no, fstatus, subgroup;
        private string remarks, actuarial, approving_officer;
        private string code;
        private int prins_count, deps_count, employee_count;
        private DateTime request_date, approval_date, expiry_date, years_with_provider;
        private string sales, agent_code, producer;
        private string aname;
        private DateTime contract_expiry;
        private string rnb;
        private string add_city, add_region, add_brgy, add_street;
        private string add_bldg;
        //private MySqlConnection mysqlConn;
        //private MySqlCommand mysqlComm;
        //private String conn_string = "Source=192.168.2.3;user=root;password=3astw3st;database=franchising";
        //int rowsaffected;

        public string _add_bldg {
            get { return add_bldg; }
            set { add_bldg = value; }
        }
        public string _add_city {
            get { return add_city;}
            set { add_city = value; }

        }
        public string _add_region {
            get { return add_region; }
            set { add_region = value; }
        }
        public string _add_brgy
        {
            get { return add_brgy; }
            set { add_brgy = value; }
        }
        public string _add_street
        {
            get { return add_street; }
            set { add_street = value; }
        }

        public string _code
        {
            get { return code; }
            set { code = value; }
        }
        public string _company_name
        {
            get { return company_name; }
            set { company_name = value;}
        }
        public string _industry
        {
            get { return industry; }
            set { industry = value; }
        }
        public string _address
        {
            get { return address; }
            set { address = value; }
    
        }
        public string _contact_person
        {
            get { return contact_person; }
            set { contact_person = value; }
        }
        public string _contact_person_number
        {
            get { return contact_person_number; }
            set { contact_person_number = value; }
        }
        public string _designation
        {
            get { return designation; }
            set { designation = value; }
        }
        public string _existing_provider
        {
            get { return existing_provider; }
            set { existing_provider = value; }
        }
        public DateTime _years_with_provider
        {
            get { return years_with_provider; }
            set { years_with_provider = value; }
        }
        public string _type_of_plan
        {
            get { return type_of_plan; }
            set { type_of_plan = value; }
        }
        public DateTime _request_date
        {
            get { return request_date; }
            set { request_date = value; }
        }

        public DateTime _contract_expiry
        { 
            get { return contract_expiry; }
            set { contract_expiry = value; }
        }

        public string _franchisee
        {
            get { return franchisee; }
            set { franchisee = value; }
        }
        public string _fcontact_person_no
        {
            get { return fcontact_person_no; }
            set { fcontact_person_no = value; }
        }
        public string _fstatus
        {
            get { return fstatus; }
            set { fstatus = value; }
        }
        public string _subgroup
        {
            get { return subgroup; }
            set { subgroup= value; }
        }
        public string _remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        public string _actuarial
        {
            get { return actuarial; }
            set { actuarial = value; }
        }
        public string _approving_officer
        {
            get { return approving_officer; }
            set { approving_officer = value; }
        } 
        public int _prins_count
        {
            get { return prins_count; }
            set { prins_count = value; }
        }
        public int _deps_count
        {
            get { return deps_count; }
            set { deps_count = value;}
        }
        public int _employee_count
        {
            get { return employee_count; }
            set { employee_count = value; }
        }
        public DateTime _approval_date
        {
            get { return approval_date; }
            set { approval_date = value; }
        }
        public DateTime _expiry_date
        {
            get { return expiry_date; }
            set { expiry_date = value; }
        }
        public string _sales
        {
            get { return sales; }
            set { sales = value; }
        }
        public string _producer
        {
            get { return producer; }
            set { producer = value; }
        }
        public string _agent_code
        {
            get { return agent_code; }
            set { agent_code = value; }
        }



        private string username, password, name, dept;

        public string _username
        {
            get { return username; }
            set { username = value; }
        }

        public string _password
        {
            get { return password; }
            set { password = value; }
        }

        public string _name
        {
            get { return name; }
            set { name = value; }
        }

        public string _dept
        {
            get { return dept; }
            set { dept = value; }
        }

        private string initials;

        public string _initials
        {
            get { return initials; }
            set { initials = value; }
        }
        public string _aname
        {
            get { return aname; }
            set { aname = value; }
        }


        private string aemail, afname, alname, mi;
        private int autocode;

        public int _autocode
        {
            get { return autocode; }
            set { autocode = value; }
        }

        public string _aemail
        {
            get { return aemail; }
            set { aemail = value; }
        }
        public string _afname
        {
            get { return afname; }
            set { afname = value; }
        }
        public string _alname
        {
            get { return alname; }
            set { alname = value; }
        }

        public string _mi
        {
            get { return mi; }
            set { mi = value; }
        }

        public string _rnb
        {
            get { return rnb; }
            set { rnb = value; }
        }

        public override string ToString()
        {
            return _sales + _actuarial + _fstatus + type_of_plan + _existing_provider + _company_name + _remarks + _industry + _rnb; 
        }

  
        }
    }

