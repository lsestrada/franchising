using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EWHC_FRANCHISING
{
    /// <summary>
    /// Interaction logic for win_addref.xaml
    /// </summary>
    public partial class win_addref : Window
    {
        public win_addref()
        {
            InitializeComponent();
        }
        String a, b, c;
        public int saveCtr = 0;
         #region ACTUARIAL TAB
         private void textBox_LostFocus(object sender, RoutedEventArgs e)
         {
             try
             {
                 a = tb_fname.Text.ToString().Substring(0, 1).ToUpper();
                 getName();
             }
             catch (Exception err) { };
           
         }

         private void textBox_Copy_LostFocus(object sender, RoutedEventArgs e)
         {
             try
             {
                 b = tb_mi.Text.ToString().Substring(0, 1).ToUpper();
                 getName();
             }
             catch (Exception err) { };
         }


         private void textBox_Copy1_LostFocus(object sender, RoutedEventArgs e)
         {
             try
             {
                 c = tb_lname.Text.ToString().Substring(0, 1).ToUpper();
                 getName();
             }
             catch (Exception err) { };
         }

         public void getName()
         {
             fullname.Content = tb_fname.Text.ToString().ToUpper() + " " + tb_mi.Text.ToString().ToUpper() + " " + tb_lname.Text.ToString().ToUpper();
             initials.Content = a + b + c;
         }


         BackgroundWorker bgref;
         loading l;
         string sql_actname;
         BlurEffect eff;
         private List<classes.binding> actname = new List<classes.binding>();
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
         {
            gridLoad();
            //actuarial.Focus();
         }


         public void gridLoad()
         {
                eff = new BlurEffect();
                dataGrid.Effect = eff;
                l = new loading();
                bgref = new BackgroundWorker();
                bgref.WorkerReportsProgress = true;
                bgref.DoWork += bg_DoWork;
                bgref.ProgressChanged += bgworker_ProgressChanged;
                bgref.RunWorkerCompleted += bg_RunWorkerCompleted;
                bgref.RunWorkerAsync();
                l.ShowDialog();                  
         }

         private ObservableCollection<classes.binding> qq = new ObservableCollection<classes.binding>();
         public void bg_DoWork(object sender, DoWorkEventArgs e)
         {
             actname = new List<classes.binding>();
             classes.getData gdata = new classes.getData();
             DataTable act_Data;
             sql_actname = "Select * from ref_actuarial order by initial";
             act_Data = new DataTable();
             act_Data = gdata.getdatasource(sql_actname);
             qq = new ObservableCollection<classes.binding>();
             int i = 1;
             foreach (DataRow ctr in act_Data.Rows)
             {
                 Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                            new Action(() =>
                            {
                                qq.Add(new classes.binding()
                                {
                                    _aname = ctr["fname"].ToString() + " " + ctr["mi"].ToString() + " " + ctr["lname"].ToString(),
                                    _initials = ctr["initial"].ToString(),
                                    _aemail = ctr["email"].ToString(),
                                    _afname = ctr["fname"].ToString(),
                                    _alname = ctr["lname"].ToString(),
                                    _mi = ctr["mi"].ToString(),
                                    _autocode = Convert.ToInt32(ctr["autocode"].ToString())                         
                            });
                                bgref.ReportProgress(i, qq);
                                i++;
                                System.Threading.Thread.Sleep(2);
                            }));
             }


         }


         public void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
         {
             dataGrid.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
             //CType(dataGrid.ItemsSource, ObservableCollection(Of binding)).Add(CType(e.UserState, binding))
            // lsss.pct.Content = Math.Round((Convert.ToDouble(e.ProgressPercentage.ToString()) / alldata.Rows.Count) * 100) + "%";
             //count.Content = Convert.ToInt32(e.ProgressPercentage.ToString());
         }

         private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
         {
             //eff = new BlurEffect();
             //dataGrid.Effect = eff;
             //l = new loading();
             //bgref = new BackgroundWorker();
             //bgref.WorkerReportsProgress = true;
             //bgref.DoWork += bg_DoWork;
             //bgref.ProgressChanged += bgworker_ProgressChanged;
             //bgref.RunWorkerCompleted += bg_RunWorkerCompleted;
             //bgref.RunWorkerAsync();
             //l.ShowDialog();
         }

         private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
         {                        
             DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
             if (row == null) return;
             classes.binding reff = (classes.binding)dataGrid.SelectedItem;
             tb_fname.Text = reff._afname.ToString();
             tb_lname.Text = reff._alname.ToString();
             tb_mi.Text = reff._mi.ToString();
             tb_email.Text = reff._aemail.ToString();
             fullname.Content = reff._aname.ToString();
             initials.Content = reff._initials.ToString();
             acode.Content = reff._autocode.ToString();
         }

         private void saveBtn_Click(object sender, RoutedEventArgs e)
         {
             if(tb_email.Text.ToString() == "" || tb_fname.Text.ToString() == "" || tb_lname.Text.ToString() == "" || tb_mi.Text.ToString() == "")
             {
                 MessageBox.Show("Unable to save! Incomplete details!", "Saving Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
             }
             else
             {
                 getName();
                 classes.binding newRef = new classes.binding();
                 newRef._afname = tb_fname.Text.ToString();
                 newRef._alname = tb_lname.Text.ToString();
                 newRef._mi = tb_mi.Text.ToString();
                 newRef._aemail = tb_email.Text.ToString();
                 newRef._initials = tb_fname.Text.ToString().Substring(0, 1).ToUpper() + tb_mi.Text.ToString().Substring(0, 1).ToUpper() + tb_lname.Text.ToString().Substring(0, 1).ToUpper(); ;
                 newRef._aname = fullname.Content.ToString();
                 newRef._autocode = Convert.ToInt32(acode.Content.ToString());
                 classes.getData gd = new classes.getData();
                 gd.executeCommand("Call `@insert_ref_act`('" + newRef._afname + "', '" + newRef._alname + "', " +
                     " '" + newRef._mi + "', '" + newRef._aemail + "', '" + newRef._initials + "', '" + newRef._aname + "', " +
                     " '" + newRef._autocode + "')");
                 gridLoad();
                 MessageBox.Show("Actuarial personnel added!");

                 tb_email.Text = "";
                 tb_fname.Text = "";
                 tb_lname.Text = "";
                 tb_mi.Text = "";
                 fullname.Content = "";
                 initials.Content = "";
                 acode.Content = "0";
                 saveCtr += 1;
                 //this.DialogResult = true;
             }
         }

         private void cancelBtn_Click(object sender, RoutedEventArgs e)
         {
             tb_email.Text = "";
             tb_fname.Text = "";
             tb_lname.Text = "";
             tb_mi.Text = "";
             fullname.Content = "";
             initials.Content = "";
             acode.Content = "0";
         }


         public void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {         
             dataGrid.Effect = null;
             Dispatcher.Invoke(DispatcherPriority.Normal, new Action(cls));
             
         }    

        public void cls()
         {
             l.Close();
         }

        #endregion


        #region Approving Officer
        BackgroundWorker bgref1;
        loading l1;
        public void gridLoad_app()
        {
            eff = new BlurEffect();
            dataGrid1.Effect = eff;
            l1 = new loading();
            bgref1 = new BackgroundWorker();
            bgref1.WorkerReportsProgress = true;
            bgref1.DoWork += bg_DoWork1;
            bgref1.ProgressChanged += bgworker_ProgressChanged1;
            bgref1.RunWorkerCompleted += bg_RunWorkerCompleted1;
            bgref1.RunWorkerAsync();
            l1.ShowDialog();
        }

        private ObservableCollection<classes.binding> qq1 = new ObservableCollection<classes.binding>();
        private List<classes.binding> appname = new List<classes.binding>();
       
        string sql_appname;
        public void bg_DoWork1(object sender, DoWorkEventArgs e)
        {
            appname = new List<classes.binding>();
            classes.getData gdata = new classes.getData();
            DataTable app_Data;
            sql_appname = "Select * from ref_approving_officer order by initial";
            app_Data = new DataTable();
            app_Data = gdata.getdatasource(sql_appname);
            qq1 = new ObservableCollection<classes.binding>();
            int j = 1;
            foreach (DataRow ctr in app_Data.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               qq1.Add(new classes.binding()
                               {
                                   _aname = ctr["fname"].ToString() + " " + ctr["mi"].ToString() + " " + ctr["lname"].ToString(),
                                   _initials = ctr["initial"].ToString(),
                                   _afname = ctr["fname"].ToString(),
                                   _alname = ctr["lname"].ToString(),
                                   _mi = ctr["mi"].ToString(),
                                   _autocode = Convert.ToInt32(ctr["autokey"].ToString())
                               });
                               bgref1.ReportProgress(j, qq1);
                               j++;
                               System.Threading.Thread.Sleep(2);
                           }));
            }
        }
        public void bgworker_ProgressChanged1(object sender, ProgressChangedEventArgs e)
        {
            dataGrid1.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
        }
        public void bg_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid1.Effect = null;
            l1.Close();
        }


        private void appOfficer_Initialized(object sender, EventArgs e)
        {
            gridLoad_app();
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;
            classes.binding reff = (classes.binding)dataGrid1.SelectedItem;
            app_fname.Text = reff._afname.ToString();
            app_lname.Text = reff._alname.ToString();
            app_mi.Text = reff._mi.ToString();
            appFname.Content = reff._aname.ToString();
            initials1.Content = reff._initials.ToString();
            akey.Content = reff._autocode.ToString();
        }
        

        private void app_save_Click(object sender, RoutedEventArgs e)
        {
            if (app_fname.Text.ToString() == "" || app_lname.Text.ToString() == "" || app_mi.Text.ToString() == "")
            {
                MessageBox.Show("Unable to save! Incomplete details!", "Saving Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                classes.binding newRef = new classes.binding();
                newRef._afname = app_fname.Text.ToString();
                newRef._alname = app_lname.Text.ToString();
                newRef._mi = app_mi.Text.ToString();
                newRef._initials = app_fname.Text.ToString().Substring(0, 1).ToUpper() + app_mi.Text.ToString().Substring(0, 1).ToUpper() + app_lname.Text.ToString().Substring(0, 1).ToUpper(); ;
                newRef._aname = appFname.Content.ToString();
                newRef._autocode = Convert.ToInt32(akey.Content.ToString());
                classes.getData gd = new classes.getData();
                gd.executeCommand("Call `@insert_ref_app`('" + newRef._afname + "', '" + newRef._alname + "', " +
                    " '" + newRef._mi + "', '" + newRef._initials + "', '" + newRef._aname + "', " +
                    " '" + newRef._autocode + "')");
                gridLoad_app();
                MessageBox.Show("Approving Officer successfully added!", "Added Successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                app_fname.Text = "";
                app_lname.Text = "";
                app_mi.Text = "";
                fullname.Content = "";
                initials.Content = "";
                akey.Content = "0";
                saveCtr += 1;
                //this.DialogResult = true;
            }
        }

        private void app_cancel_Click(object sender, RoutedEventArgs e)
        {
            app_fname.Text = "";
            app_lname.Text = "";
            app_mi.Text ="";
            appFname.Content = "";
            initials1.Content = "";
            akey.Content = "0";
        }

        #endregion

        private void comp_Initialized(object sender, EventArgs e)
        {
            gridLoad_ind();
        }
        BackgroundWorker bgref2;
        loading l2;
        public void gridLoad_ind()
        {
            eff = new BlurEffect();
            dataGrid2.Effect = eff;
            l2 = new loading();
            bgref2 = new BackgroundWorker();
            bgref2.WorkerReportsProgress = true;
            bgref2.DoWork += bg_DoWork2;
            bgref2.ProgressChanged += bgworker_ProgressChanged2;
            bgref2.RunWorkerCompleted += bg_RunWorkerCompleted2;
            bgref2.RunWorkerAsync();
            l2.ShowDialog();
        }

        private ObservableCollection<classes.binding> qq2 = new ObservableCollection<classes.binding>();
        private List<classes.binding> inddustryy = new List<classes.binding>();

        string sql_industry;
        public void bg_DoWork2(object sender, DoWorkEventArgs e)
        {
            inddustryy = new List<classes.binding>();
            classes.getData gdata = new classes.getData();
            DataTable indd;
            sql_industry = "Select * from ref_industry order by industry";
            indd = new DataTable();
            indd = gdata.getdatasource(sql_industry);
            qq2 = new ObservableCollection<classes.binding>();
            int h = 1;
            foreach (DataRow ctr in indd.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               qq2.Add(new classes.binding()
                               {
                                 _industry = ctr["industry"].ToString(),
                                 _autocode = Convert.ToInt32(ctr["autokey"].ToString())
                               });
                               bgref2.ReportProgress(h, qq2);
                               h++;
                               System.Threading.Thread.Sleep(2);
                           }));
            }
        }
        public void bgworker_ProgressChanged2(object sender, ProgressChangedEventArgs e)
        {
            dataGrid2.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
        }
        public void bg_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid2.Effect = null;
            l2.Close();
        }

        private void dataGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;
            classes.binding w = (classes.binding)dataGrid2.SelectedItem;
            industry.Text = w._industry.ToString();
            ind.Content = Convert.ToUInt32(w._autocode.ToString());
        }
       

        private void indSave_Click(object sender, RoutedEventArgs e)
        {
            if (industry.Text.ToString() == "")
            {
                MessageBox.Show("Unable to save! Incomplete details!", "Saving Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                classes.binding newRef = new classes.binding();
                newRef._industry = industry.Text.ToString();              
                newRef._autocode = Convert.ToInt32(ind.Content.ToString());
                classes.getData gd = new classes.getData();
                gd.executeCommand("Call `@insert_ref_ind`('" + newRef._industry + "', '" + newRef._autocode + "')");
                gridLoad_ind();
                MessageBox.Show("Company / Subgroup successfully added!", "Added Successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                industry.Text = "";                
                ind.Content = "0";
                saveCtr += 1;
                //this.DialogResult = true;

            }
        }
     

        private void indCancel_Click(object sender, RoutedEventArgs e)
        {
            industry.Text = "";
            ind.Content = "0";
        }

        #region REF BTNS EFFECTS
        private void saveBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            saveBtn.Foreground = Brushes.Blue;
        }

        private void saveBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            saveBtn.Foreground = Brushes.White;
        }

        private void cancelBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            cancelBtn.Foreground = Brushes.Blue;
        }

        private void cancelBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            cancelBtn.Foreground = Brushes.White;
        }

        private void app_save_MouseEnter(object sender, MouseEventArgs e)
        {
            app_save.Foreground = Brushes.Blue;
        }

        private void app_save_MouseLeave(object sender, MouseEventArgs e)
        {
            app_save.Foreground = Brushes.White;
        }

        private void inSave_MouseEnter(object sender, MouseEventArgs e)
        {
            inSave.Foreground = Brushes.Blue;
        }

        private void inSave_MouseLeave(object sender, MouseEventArgs e)
        {
            inSave.Foreground = Brushes.White;
        }

        private void indCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            indCancel.Foreground = Brushes.Blue;
        }

        private void indCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            indCancel.Foreground = Brushes.White;
        }

        private void app_cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            app_cancel.Foreground = Brushes.Blue;
        }

        private void app_cancel_MouseLeave(object sender, MouseEventArgs e)
        {
            app_cancel.Foreground = Brushes.White;
        }


        private void hmo_save_MouseEnter(object sender, MouseEventArgs e)
        {
            hmo_save.Foreground = Brushes.Blue;
        }

        private void hmo_save_MouseLeave(object sender, MouseEventArgs e)
        {
            hmo_save.Foreground = Brushes.White;
        }

        private void hmo_cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            hmo_cancel.Foreground = Brushes.Blue;
        }

        private void hmo_cancel_MouseLeave(object sender, MouseEventArgs e)
        {
            hmo_cancel.Foreground = Brushes.White;
        }

        #endregion



        private void hmo_Initialized(object sender, EventArgs e)
        {
            gridLoad_hmo();
        }

        BackgroundWorker bgref3;
        loading l3;
        public void gridLoad_hmo()
        {
            eff = new BlurEffect();
            dataGrid3.Effect = eff;
            l3 = new loading();
            bgref3 = new BackgroundWorker();
            bgref3.WorkerReportsProgress = true;
            bgref3.DoWork += bg_DoWork3;
            bgref3.ProgressChanged += bgworker_ProgressChanged3;
            bgref3.RunWorkerCompleted += bg_RunWorkerCompleted3;
            bgref3.RunWorkerAsync();
            l3.ShowDialog();
        }

        private ObservableCollection<classes.binding> qq3 = new ObservableCollection<classes.binding>();
        private List<classes.binding> hmo1 = new List<classes.binding>();
        string sql_hmo;

        public void bg_DoWork3(object sender, DoWorkEventArgs e)
        {
            hmo1 = new List<classes.binding>();
            classes.getData gdata = new classes.getData();
            DataTable hmoo;
            sql_hmo = "Select * from ref_providers order by hmo";
            hmoo = new DataTable();
            hmoo = gdata.getdatasource(sql_hmo);
            qq3 = new ObservableCollection<classes.binding>();
            int h = 1;
            foreach (DataRow ctr in hmoo.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               qq3.Add(new classes.binding()
                               {
                                   _existing_provider = ctr["hmo"].ToString(),
                                   _autocode = Convert.ToInt32(ctr["autokey"].ToString())
                               });
                               bgref3.ReportProgress(h, qq3);
                               h++;
                               System.Threading.Thread.Sleep(2);
                           }));
            }
        }
        public void bgworker_ProgressChanged3(object sender, ProgressChangedEventArgs e)
        {
            dataGrid3.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
        }
        public void bg_RunWorkerCompleted3(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid3.Effect = null;
            l3.Close();
        }

        private void dataGrid3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;
            classes.binding w = (classes.binding)dataGrid3.SelectedItem;
            tb_hmo.Text = w._existing_provider.ToString();
            hmo_key.Content = Convert.ToUInt32(w._autocode.ToString());
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(saveCtr >= 1)
            {
                //MessageBox.Show(saveCtr.ToString());
                this.DialogResult = true;
            }

        }

        private void dg_plan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

       

        private void hmo_save_Click(object sender, RoutedEventArgs e)
        {
            if (tb_hmo.Text.ToString() == "")
            {
                MessageBox.Show("Unable to save! Incomplete details!", "Saving Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                classes.binding newRef = new classes.binding();
                newRef._existing_provider = tb_hmo.Text.ToString();
                newRef._autocode = Convert.ToInt32(hmo_key.Content.ToString());
                classes.getData gd = new classes.getData();
                gd.executeCommand("Call `@insert_ref_hmo`('" + newRef._existing_provider + "', '" + newRef._autocode + "')");
                gridLoad_hmo();
                MessageBox.Show("Provider successfully added!", "Added Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_hmo.Text = "";
                hmo_key.Content = "0";
                saveCtr += 1;
                //this.DialogResult = true;
            }
        }

        private void hmo_cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_hmo.Text = "";
            hmo_key.Content = "0";
        }


        #region ROOM AND BOARD

        private void rnb_Initialized(object sender, EventArgs e)
        {
            gridload_rbn();
        }


        BackgroundWorker bgref4;
        loading l4;
        public void gridload_rbn()
        {
            eff = new BlurEffect();
            dg_rnb.Effect = eff;
            l4 = new loading();
            bgref4 = new BackgroundWorker();
            bgref4.WorkerReportsProgress = true;
            bgref4.DoWork += bg_DoWork4;
            bgref4.ProgressChanged += bgworker_ProgressChanged4;
            bgref4.RunWorkerCompleted += bg_RunWorkerCompleted4;
            bgref4.RunWorkerAsync();
            l4.ShowDialog();
        }

        List<classes.binding> cname10 = new List<classes.binding>();
        ObservableCollection<classes.binding> rnbb;
        public string str_rnb;
        private void bg_DoWork4(object sender, DoWorkEventArgs e)
        {
            cname10 = new List<classes.binding>();
            classes.getData gdata = new classes.getData();
            DataTable hmoo;
            str_rnb = "Select DISTINCT * from ref_rnb";
            hmoo = new DataTable();
            hmoo = gdata.getdatasource(str_rnb);
            rnbb = new ObservableCollection<classes.binding>();
            int h = 1;
            foreach (DataRow ctr in hmoo.Rows)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               rnbb.Add(new classes.binding()
                               {
                                   _autocode = Convert.ToInt32(ctr["autocode"]),                                
                                   _rnb = (ctr["rnb"].ToString())
                               });
                               bgref4.ReportProgress(h, rnbb);
                               h++;
                               System.Threading.Thread.Sleep(1);
                           }));
            }
        }

        private void dg_rnb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;
            classes.binding w = (classes.binding)dg_rnb.SelectedItem;
            tb_rnb.Text = w._rnb.ToString();
            rnb_id.Content = Convert.ToUInt32(w._autocode.ToString());
        }

        private void hmo_cancel_Copy_Click(object sender, RoutedEventArgs e)
        {
            tb_rnb.Text = "";
            rnb_id.Content = "0";
        }

        private void hmo_save_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (tb_rnb.Text.ToString() == "")
            {
                MessageBox.Show("Unable to save! Incomplete details!", "Saving Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                classes.binding newRef = new classes.binding();
                newRef._rnb = tb_rnb.Text.ToString();
                newRef._autocode = Convert.ToInt32(rnb_id.Content.ToString());
                classes.getData gd = new classes.getData();
                gd.executeCommand("Call `@insert_ref_rnb`('" + newRef._autocode + "', '" + newRef._rnb + "')");
                gridload_rbn();
                MessageBox.Show("Room and Board successfully added!", "Added Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_rnb.Text = "";
                rnb_id.Content = "0";
                saveCtr += 1;
                //this.DialogResult = true;
            }
        }

        private void bgworker_ProgressChanged4(object sender, ProgressChangedEventArgs e)
        {
            dg_rnb.ItemsSource = ((ObservableCollection<classes.binding>)e.UserState);
        }

        private void bg_RunWorkerCompleted4(object sender, RunWorkerCompletedEventArgs e)
        {
            dg_rnb.Effect = null;
            l4.Close();
        }


        #endregion


    }
}