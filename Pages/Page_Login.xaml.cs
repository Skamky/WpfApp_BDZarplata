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
    /// Логика взаимодействия для Page_Login.xaml
    /// </summary>
    public partial class Page_Login : Page
    {
        public Page_Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.UpdateLabel("Попытка подключения К БД");
            if (Classes.DB_Connect.OpenClouseConnection(TB_IpPc.Text, TB_DBName.Text))
            {
                Classes.Manager.UpdateLabel("Подключение прошло успешно");

                Classes.DB.queryScalar("SELECT  t2.AccessLvl " +
                    "FROM[bd_zarplta].[sotrudnik] " +
                    "t1 LEFT JOIN bd_zarplta.doljnost t2 ON t1.idDoljnost = t2.idDoljnost " +
                    "where t1.idSotrudnik="+TB_idSotrudnik.Text);
                //Classes.Manager.MainFrame.Navigate(new Page_Grid());
            }
            else Classes.Manager.UpdateLabel("Ошибка");
        }

        private void CB_IPPC_Localhost_Click(object sender, RoutedEventArgs e)
        {
            if (CB_IPPC_Localhost.IsChecked == true)
            {
                TB_IpPc.Text = "localhost";
                TB_IpPc.IsEnabled = false;
            }
            else
            {
                TB_IpPc.Text = "";
                TB_IpPc.IsEnabled = true;
            }
        }

        private void CB_BD_NAmeDef_Click(object sender, RoutedEventArgs e)
        {
            if (CB_BD_NAmeDef.IsChecked==true)
            {
                TB_DBName.Text = "BD_Zarplata";
                TB_DBName.IsEnabled = false;
            }
            else
            {
                TB_DBName.Text = "";
                TB_DBName.IsEnabled = true;
            }
        }
    }
}
