using System.Windows;
using System.Windows.Controls;

namespace BDZarplata.Pages
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
        /// <summary>
        /// Попытка подключения к БД 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.UpdateLabel("Попытка подключения К БД");
            if (Classes.DB_Connect.OpenClouseConnection(TB_IpPc.Text, TB_DBName.Text))
            {
                Classes.Manager.UpdateLabel("Подключение прошло успешно");

                object DostupLvl =
                 Classes.DB.queryScalar("SELECT  t2.AccessLvl " +
                     "FROM[bd_zarplta].[sotrudnik] " +
                     "t1 LEFT JOIN bd_zarplta.doljnost t2 ON t1.idDoljnost = t2.idDoljnost " +
                     "where t1.idSotrudnik=" + TB_idSotrudnik.Text).ToString();
                switch (DostupLvl)
                {
                    case "0":
                        MessageBox.Show("В Доступе Отказано! Обратитесь в Бухгалтерию для получения выписки");
                        break;
                    case "1":
                        Classes.Manager.MainFrame.Navigate(new Page_BughalterInfo());
                        break;
                    case "2":
                        Classes.Manager.MainFrame.Navigate(new Pages.Page_SotrudnikMainInfo());
                        break;
                    case "3":
                        Classes.Manager.MainFrame.Navigate(new Page_SelectPage());
                        break;
                    default:
                        MessageBox.Show("Неизвестный уровень доступа " + DostupLvl);
                        break;
                }

            }
            else
            {
                Classes.Manager.UpdateLabel("Ошибка");
            }
        }
        /// <summary>
        /// Переключение поля для ввода адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Переключение поля для ввода названия БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_BD_NAmeDef_Click(object sender, RoutedEventArgs e)
        {
            if (CB_BD_NAmeDef.IsChecked == true)
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
