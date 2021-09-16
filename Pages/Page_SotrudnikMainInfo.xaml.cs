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
            DB.LoadDataComboBox(CB_StatusDay, "SELECT DISTINCT [StatusDay] FROM[BD_Zarplata].[bd_zarplta].[graphik_rabot]", 0);
            DB.LoadDataComboBox(CB_StatusSotrud, "SELECT DISTINCT [StatusSotrud] FROM[BD_Zarplata].[bd_zarplta].[graphik_rabot]", 0);
        }
        /// <summary>
        /// Переход в режим редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// сохранение измененных полей таблицы БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
          
        }
        /// <summary>
        /// Сопоставление ФИО и ID сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_Sotrud_FIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Sotrud_id.SelectedIndex = LB_Sotrud_FIO.SelectedIndex;
        }
        /// <summary>
        /// Подгрузка данных о Расписании выбранного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_Sotrud_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Classes.DB.loadDataGrid(DG_Raspisnie, $"SELECT FORMAT([DATE],'d') as 'Date' ,[StatusSotrud] ,[StatusDay] FROM [BD_Zarplata].[bd_zarplta].[graphik_rabot] where [Sotrudnik_idSotrudnik] = {LB_Sotrud_id.SelectedItem}");
        }


        /// <summary>
        /// Переход в режим редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_Sotrud_Anketa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Btn_Redactir_Click(sender, e);
        }
        /// <summary>
        /// Переход в режим добавления нового сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_AddRedAnketa());
        }
        /// <summary>
        /// Подгрузка данных о выбранном дне
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_Raspisnie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                calendar_raspisan.DisplayDate = Convert.ToDateTime(DG.GetSelectCell(DG_Raspisnie, 0));
                calendar_raspisan.SelectedDate = Convert.ToDateTime(DG.GetSelectCell(DG_Raspisnie, 0));
                CB_StatusDay.SelectedItem = DG.GetSelectCell(DG_Raspisnie, 2);
                CB_StatusSotrud.SelectedItem = DG.GetSelectCell(DG_Raspisnie, 1);
                BTN_RedRaspisan.IsEnabled = true;
            }
            catch
            {
                calendar_raspisan.DisplayDate = DateTime.Now;
                calendar_raspisan.SelectedDate = DateTime.Now;
                CB_StatusDay.SelectedIndex = 1;
                CB_StatusSotrud.SelectedIndex = 1;
                BTN_RedRaspisan.IsEnabled = false;
            }
        }

        
        /// <summary>
        /// сохранение измененных полей таблицы БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_RedRaspisan_Click(object sender, RoutedEventArgs e)
        {
            Procedure.UpdateTable
                (
                "[BD_Zarplata].[bd_zarplta].[graphik_rabot]"
                , new List<string>() { "[StatusSotrud]", "[StatusDay]" }
                , new List<string>() { "'"+CB_StatusSotrud.SelectedItem+"'", "'"+CB_StatusDay.SelectedItem+"'" }
                , $"where[Sotrudnik_idSotrudnik] = {LB_Sotrud_id.SelectedItem} AND[DATE] = '{calendar_raspisan.SelectedDate}'"
                );
            Classes.DB.loadDataGrid(DG_Raspisnie, $"SELECT FORMAT([DATE],'d') ,[StatusSotrud] ,[StatusDay] FROM [BD_Zarplata].[bd_zarplta].[graphik_rabot] where [Sotrudnik_idSotrudnik] = {LB_Sotrud_id.SelectedItem}");
            MessageBox.Show("Успешно Обновленно!");
        }
    }
}
