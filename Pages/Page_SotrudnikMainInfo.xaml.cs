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
    /// Логика взаимодействия для Page_SotrudnikMainInfo.xaml
    /// </summary>
    public partial class Page_SotrudnikMainInfo : Page
    {
        public Page_SotrudnikMainInfo()
        {
            InitializeComponent();
            Classes.DB.loadDataGrid(DG_Sotrud_Anketa, "SELECT [idSotrudnik], full_name , t2.title, family_status, num_zd_kids,num_invalid_kids, opeka, SpecStatus, Staj FROM[bd_zarplta].[sotrudnik] t1 LEFT JOIN bd_zarplta.doljnost t2 ON t1.idDoljnost = t2.idDoljnost");
            Classes.DB.LoadDataListBox(LB_Sotrud_FIO, "SELECT  [idSotrudnik],[full_name]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 1);
            Classes.DB.LoadDataListBox(LB_Sotrud_id, "SELECT [idSotrudnik]  ,[full_name] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 0);
            Classes.DB.LoadDataListBox(LB_Sotrud_FIO2, "SELECT  [idSotrudnik],[full_name]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 1);
            Classes.DB.LoadDataListBox(LB_Sotrud_id2, "SELECT [idSotrudnik]  ,[full_name] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 0);
        }

        private void Btn_Redactir_Click(object sender, RoutedEventArgs e)
        {
            string sotrudid = Classes.DG.GetSelectCell(DG_Sotrud_Anketa, 0);
            switch (TabC_Main.SelectedIndex)
            {
                case 0:
                    Manager.MainFrame.Navigate(new Page_AddRedAnketa(sotrudid));
                    break;
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LB_Sotrud_FIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Sotrud_id.SelectedIndex = LB_Sotrud_FIO.SelectedIndex;
        }

        private void LB_Sotrud_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Classes.DB.loadDataGrid(DG_Raspisnie, $"SELECT[DATE] ,[StatusSotrud] ,[StatusDay] FROM [BD_Zarplata].[bd_zarplta].[graphik_rabot] where [Sotrudnik_idSotrudnik] = {LB_Sotrud_id.SelectedItem}");
        }

        private void LB_Sotrud_FIO2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Sotrud_id2.SelectedIndex = LB_Sotrud_FIO2.SelectedIndex;
        }

        private void LB_Sotrud_id2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Classes.DB.loadDataGrid(DG_NadbavShtraf, $"SELECT [Data],[Nadbav]      ,[Vichet]        FROM [BD_Zarplata].[bd_zarplta].[zp]  Where [Sotrudnik_idSotrudnik]= {LB_Sotrud_id2.SelectedItem}");
        }

        private void DG_Sotrud_Anketa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Btn_Redactir_Click(sender, e);
        }
    }
}
