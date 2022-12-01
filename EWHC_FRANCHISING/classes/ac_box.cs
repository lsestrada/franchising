using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
namespace EWHC_FRANCHISING.classes
{
    class ac_box : AutoCompleteBox
    {
        private int _maxresult;
        int counter = 0;
        public ac_box() {
           _maxresult = 300;
            this.Loaded += new RoutedEventHandler(Load);
        }

        public new void Focus() {
            TextBox tb;
            tb = (TextBox)Template.FindName("Text", this);

            if (tb != null) {
                tb.Focus();
            }
        }
        private void Load(object sender, RoutedEventArgs e) {
            this.TextFilter = new AutoCompleteFilterPredicate<string>(NewTextFilter);
            this.TextChanged += new RoutedEventHandler(this_TextChanged);

        }
        public void Clear() {
            TextBox tb;
            tb = (TextBox)Template.FindName("Text", this);
            if (tb != null) {
                tb.Clear();
            }
        }
         public int MaxResult {
            get { return _maxresult; }
           set { _maxresult = value; }
        }
        private bool NewTextFilter(string str, object value) {
            if (value.ToString().ToUpper().Contains(str.ToUpper().Trim()) &&  counter < MaxResult)
            {
                counter += 1;
                return true;
            }
            else { return false; }
        }
        public void this_TextChanged(object sender, RoutedEventArgs e) {
            counter = 0;
        }
       
    }
}
