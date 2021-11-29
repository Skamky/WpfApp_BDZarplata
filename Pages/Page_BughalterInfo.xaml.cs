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
        bool Select0Items = true;
        public Page_BughalterInfo()
        {
            Manager.MainProgressBar.Visibility = Visibility.Visible;
            Manager.MainProgressBar.Maximum = 5;

            InitializeComponent();
            Manager.MainProgressBar.Value = 1;
            DB.loadDataGrid(DG_SotridnikOklad, "SELECT  [idDoljnost],    [title]     ,[Oklad], [Travmat] FROM [BD_Zarplata].[bd_zarplta].[doljnost]");

            TabI_LN_Initialized(null, null);
            DB.loadDataGrid(DG_Base, "SELECT *  FROM [BD_Zarplata].[bd_zarplta].[base]");


            Classes.DB.LoadDataListBox(LB_Sotrud_FIO2, "SELECT  [idSotrudnik],[full_name]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 1);
            Classes.DB.LoadDataListBox(LB_Sotrud_id2, "SELECT [idSotrudnik]  ,[full_name] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik]", 0);
            Manager.MainProgressBar.Visibility = Visibility.Hidden;

        }
        /// <summary>
        /// Подгруздка справочника льгот и налогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabI_LN_Initialized(object sender, EventArgs e)
        {
            Manager.MainProgressBar.Value = 2;
            TB_FCC.Text = Classes.DB.queryScalar("SELECT FCC FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_FOMC.Text = Classes.DB.queryScalar("SELECT FOMC FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Value = 3;
            TB_Kid1.Text = Classes.DB.queryScalar("SELECT kid1 FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_Kid3.Text = Classes.DB.queryScalar("SELECT Kid3 FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_KidInvalid.Text = Classes.DB.queryScalar("SELECT invalid FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_KidInvalid_opek.Text = Classes.DB.queryScalar("SELECT [invlid_o] FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Value = 5;
            TB_Mrot.Text = Classes.DB.queryScalar("SELECT MROT FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_NDFL.Text = Classes.DB.queryScalar("SELECT NDFL FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            TB_PFR.Text = Classes.DB.queryScalar("SELECT PFR FROM [BD_Zarplata].[bd_zarplta].[h]").ToString();
            Manager.MainProgressBar.Value = 5;
        }
        /// <summary>
        /// сохранение измененных полей таблицы БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            switch (TabC_Main.SelectedIndex)
            {
                case 0:

                    break;
                case 1:
                    if (MessageBoxResult.OK == MessageBox.Show("Сохранить Изменения? \n Ок -сохранить \n Отмена - Отменить все изменения", "Запрос на сохранение", MessageBoxButton.OKCancel))
                    {
                        List<string> columnName = new List<string>() { "[NDFL]", "[PFR]", "[FCC]", "[FOMC]", "[kid1]", "[kid3]", "[invalid]", "[invlid_o]", "[MROT]" };
                        List<string> MasData = new List<string>() { TB_NDFL.Text.Replace(',', '.'), TB_PFR.Text.Replace(',', '.'), TB_FCC.Text.Replace(',', '.'), TB_FOMC.Text.Replace(',', '.'), TB_Kid1.Text, TB_Kid3.Text, TB_KidInvalid.Text, TB_KidInvalid_opek.Text, TB_Mrot.Text };
                        Classes.Procedure.UpdateTable("[BD_Zarplata].[bd_zarplta].[h]", columnName, MasData, "");
                    }
                    TabI_LN_Initialized(null, null);
                    break;
                case 2:
                    MessageBox.Show("Изменения успешно сохранены!");
                    break;
                default:
                    break;

            }
        }
        /// <summary>
        /// Переход на страницу редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Redactir_Click(object sender, RoutedEventArgs e)
        {
            switch (TabC_Main.SelectedIndex)
            {
                case 0:
                    Manager.MainFrame.Navigate(new Pages.Page_BuhgaltAddEdit_Oklad(CellID));
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            }
        private void intOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //узнаем Кто вызвал событие 
            TextBox textBox = sender as TextBox;
            //проверка что введена цифра
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Только Целые числа", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //проверяем выходит ли за предел int
            else if (!int.TryParse(textBox.Text + e.Text, out _))
            {
                MessageBox.Show("Максимальный размер числа не может быть больше 2147483647", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;

            }
        }
        /// <summary>
        /// Ограничение для TextBox для ввода только Дробных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void floatOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //узнаем Кто вызвал событие 
            TextBox textBox = sender as TextBox;
            //проверка что в строке лишь 1 .
            string fullText = textBox.Text + e.Text;
            if (e.Text.Contains(",") && (textBox.Text.Contains(",") || textBox.Text.Length == 0))
            {
                e.Handled = true;
                MessageBox.Show("неверно поставлена точка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            //проверка что введена цифра или .
            else if (!Char.IsDigit(e.Text, 0) && e.Text != ",")
            {
                e.Handled = true;
                MessageBox.Show("Только дробные числа", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (textBox.Text.Length > 4)
            {
                e.Handled = true;
                MessageBox.Show("Длина строки не может превышать 5 символов", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        /// <summary>
        /// запись ID выделенной строки
        /// </summary>
        string CellID;
        private void DG_SotridnikOklad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Select0Items = false;
            Btn_Redactir.Visibility = Visibility.Visible;
            CellID = DG.GetSelectCell(DG_SotridnikOklad, 0);
        }
        /// <summary>
        /// Переход в режим редактирования оклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_SotridnikOklad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_BuhgaltAddEdit_Oklad(CellID));
        }
        /// <summary>
        /// Смена видимости кнопок , при выборе вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabI_OkladSotrud_GotFocus(object sender, RoutedEventArgs e)
        {
            Btn_Add.Visibility = Visibility.Hidden;
            Btn_delete.Visibility = Visibility.Hidden;
            if(Select0Items) Btn_Redactir.Visibility = Visibility.Hidden;
            Btn_Save.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Смена видимости кнопок , при выборе вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabI_Base_GotFocus(object sender, RoutedEventArgs e)
        {
            Btn_Add.Visibility = Visibility.Visible;
            Btn_delete.Visibility = Visibility.Visible;
            Btn_Redactir.Visibility = Visibility.Visible;
            Btn_Save.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Смена видимости кнопок , при выборе вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabI_LN_GotFocus(object sender, RoutedEventArgs e)
        {
            Btn_Add.Visibility = Visibility.Hidden;
            Btn_delete.Visibility = Visibility.Hidden;
            Btn_Redactir.Visibility = Visibility.Hidden;
            Btn_Save.Visibility = Visibility.Visible;
        }
        private void LB_Sotrud_FIO2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Sotrud_id2.SelectedIndex = LB_Sotrud_FIO2.SelectedIndex;
        }
        /// <summary>
        /// Подгрузка данных о надбавках и штрафах выбранного сотрудника в DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_Sotrud_id2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Classes.DB.loadDataGrid(DG_NadbavShtraf, $"SELECT  FORMAT([Data],'d') as 'Data',[Nadbav]      ,[Vichet]        FROM [BD_Zarplata].[bd_zarplta].[zp]  Where [Sotrudnik_idSotrudnik]= {LB_Sotrud_id2.SelectedItem}");
            CB_Data_Nadbav.Items.Clear();
            DB.LoadDataComboBox(CB_Data_Nadbav, $"SELECT [Data] FROM [BD_Zarplata].[bd_zarplta].[zp]  Where [Sotrudnik_idSotrudnik]= {LB_Sotrud_id2.SelectedItem}", 0);
        }
        /// <summary>
        /// Вывод в форму даты , надбавки и штрафов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_NadbavShtraf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Calendar2.DisplayDate = Convert.ToDateTime(DG.GetSelectCell(DG_NadbavShtraf, 0));
                Calendar2.SelectedDate = Convert.ToDateTime(DG.GetSelectCell(DG_NadbavShtraf, 0));
                TB_Nadbav.Text = DG.GetSelectCell(DG_NadbavShtraf, 1);
                TB_Vichet.Text = DG.GetSelectCell(DG_NadbavShtraf, 2);
                Btn_Red_Nadbav.IsEnabled = true;
            }
            catch
            {
                Calendar2.DisplayDate = DateTime.Now;
                Calendar2.SelectedDate = DateTime.Now;
                TB_Nadbav.Text = "";
                TB_Vichet.Text="";
                Btn_Red_Nadbav.IsEnabled = false;
            }
        }
        /// <summary>
        /// Переход к странице расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Raschet_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_raschet());
        }
        /// <summary>
        /// Внесение изменений в БД 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Red_Nadbav_Click(object sender, RoutedEventArgs e)
        {
            Procedure.UpdateTable
                (
                "[BD_Zarplata].[bd_zarplta].[zp]"
                , new List<string>() { "[Vichet]", "[Nadbav]" }
                , new List<string>() {  TB_Vichet.Text , TB_Nadbav.Text }
                , $"where[Sotrudnik_idSotrudnik] = {LB_Sotrud_id2.SelectedItem} AND [Data] = '{Calendar2.SelectedDate}'"
                );
            MessageBox.Show("Изменения сохранены!");
            Classes.DB.loadDataGrid(DG_NadbavShtraf, $"SELECT  FORMAT([Data],'d'),[Nadbav]      ,[Vichet]        FROM [BD_Zarplata].[bd_zarplta].[zp]  Where [Sotrudnik_idSotrudnik]= {LB_Sotrud_id2.SelectedItem}");
        }
    }
}
