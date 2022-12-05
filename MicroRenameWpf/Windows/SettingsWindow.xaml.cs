using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MicroRenameWpf.Windows
{
    /// <summary>
    /// Interaction logic for BRMBrowserSettings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        //Component Init

        bool gKillSwitch;
        //Data Init

        void jInitData()
        {
            gKillSwitch = false;
        }
        void jDestructor() { }
        //Events----------------------------------------------------------------------------------

        //Form
        void BRMBrowserSettings_WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => e.Cancel = kCheckKill();
        void xCancel_Click(object sender, RoutedEventArgs e) => Hide();

        //Filter
        void xAdd_Click(object sender, RoutedEventArgs e) => jAdd();
        void xDel_Click(object sender, RoutedEventArgs e) => jDel();
        void xClear_Click(object sender, RoutedEventArgs e) => jDel();

        //Jobs--------------------------------------------------------------------------------------------

        //Table
        void jAdd() { kAdd(this.xNew.Text); xNew.Clear(); }
        void jDel() { }
        void jClear() { }

        //Filters
        List<string> jGetFilters() { return new List<string>(0); }
        void jSetFilters(List<string> i) { }
        bool jGetMode() => false;
        void jSetMode(bool i) { }




        //Kernel------------------------------------------------------------------------------------------

        //Form
        bool kCheckKill()
        {
            this.Hide();
            return gKillSwitch;
        }
        //List
        void kAdd(string i)
        {
            this.xList.Items.Add(i);
        }

		//Public------------------------------------------------------------------------------------------

	    //Properties
        public List<string> pBlacklistFilters
        {
            get { return jGetFilters(); }
            set { jSetFilters(value); }
        }
        public bool pUseWhitelistMode
        {
            get { return jGetMode(); }
            set { jSetMode(value); }
        }
        //Methods
        public void rAuthorizeKill(bool iAuth) => gKillSwitch = iAuth;
        public bool rAuthorizeFile(string i) => true;
    }
}
