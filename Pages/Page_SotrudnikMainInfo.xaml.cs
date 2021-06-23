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
    /// Логика взаимодействия для Page_SotrudnikMainInfo.xaml
    /// </summary>
    public partial class Page_SotrudnikMainInfo : Page
    {
        public Page_SotrudnikMainInfo()
        {
            InitializeComponent();
            Classes.DB.queryScalar("SELECT  full_name , t2.title, family_status, num_zd_kids,num_invalid_kids, opeka, SpecStatus, Staj FROM[bd_zarplta].[sotrudnik] t1 LEFT JOIN bd_zarplta.doljnost t2 ON t1.idDoljnost = t2.idDoljnost");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
