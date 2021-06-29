using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_КурсоваяРабота2021_BDZarplata.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_SelectPage.xaml
    /// </summary>
    public partial class Page_SelectPage : Page
    {
        public Page_SelectPage()
        {
            InitializeComponent();
        }

        private void BTN_BughalterInfo_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Page_BughalterInfo());
        }

        private void BTN_Page_SotrudnikMainInfo_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.Page_SotrudnikMainInfo());
        }
    }
}
