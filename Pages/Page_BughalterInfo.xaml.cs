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
using WpfApp_КурсоваяРабота2021_BDZarplata.Classes;

namespace WpfApp_КурсоваяРабота2021_BDZarplata.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_BughalterInfo.xaml
    /// </summary>
    public partial class Page_BughalterInfo : Page
    {
        public Page_BughalterInfo()
        {
            Manager.MainProgressBar.Visibility = Visibility.Visible;
            Manager.MainProgressBar.Maximum = 5;

            InitializeComponent();
            Manager.MainProgressBar.Value = 1;
            DB.loadDataGrid(DG_SotridnikOklad,"SELECT      [title]     ,[Oklad], [Travmat] FROM [BD_Zarplata].[bd_zarplta].[doljnost]");
            Manager.MainProgressBar.Value = 2;
            TB_FCC.Text = Classes.DB.queryScalar("SELECT FCC FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_FOMC.Text = Classes.DB.queryScalar("SELECT FOMC FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Value = 3;
            TB_Kid1.Text = Classes.DB.queryScalar("SELECT kid1 FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_Kid3.Text = Classes.DB.queryScalar("SELECT Kid3 FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_KidInvalid.Text = Classes.DB.queryScalar("SELECT invalid FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_KidInvalid_opek.Text = Classes.DB.queryScalar("SELECT [invlid_o] FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Value = 4;
            TB_Mrot.Text = Classes.DB.queryScalar("SELECT MROT FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_NDFL.Text = Classes.DB.queryScalar("SELECT NDFL FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_PFR.Text = Classes.DB.queryScalar("SELECT PFR FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Visibility = Visibility.Hidden;
        }

        
    }
}
