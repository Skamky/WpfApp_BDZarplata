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
    /// Логика взаимодействия для Page_BughalterInfo.xaml
    /// </summary>
    public partial class Page_BughalterInfo : Page
    {
        public Page_BughalterInfo()
        {
            InitializeComponent();
            Classes.DB.loadDataGrid(DG_SotridnikOklad,"SELECT      [title]     ,[Oklad], [Travmat] FROM [BD_Zarplata].[bd_zarplta].[doljnost]");
            TB_FCC.Text = Classes.DB.queryScalar("SELECT FCC FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
        
        }

        
    }
}
