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
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfApp_КурсоваяРабота2021_BDZarplata.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_raschet.xaml
    /// </summary>
    public partial class Page_raschet : Page
    {
        double NDFL_stavka, PFR_stavka, FCC_stavka, FOMC_stavka,TRavmat_stavka ,Mrot_stavka;
        int Kid1_stavka, Kid3_stavka, Invalid_stavka, Invalid_o_stavka;
        int Nadbav, Vichet;



        public Page_raschet()
        {
            InitializeComponent();
            DB.LoadDataComboBox(CB_FIO, "SELECT [idSotrudnik] ,[full_name] FROM[BD_Zarplata].[bd_zarplta].[sotrudnik]", 1);
            DB.LoadDataComboBox(CB_SotrudID, "SELECT [idSotrudnik] ,[full_name] FROM[BD_Zarplata].[bd_zarplta].[sotrudnik]", 0);
            int[] tempColumns = { 0,1,2,3,4,5,6,7,8 };
            string[] temp = DB.queryScalar("Select * FROM [BD_Zarplata].[bd_zarplta].[h]", tempColumns);
            NDFL_stavka = Convert.ToDouble( temp[0]);
            PFR_stavka = Convert.ToDouble(temp[1]);
            FCC_stavka = Convert.ToDouble(temp[2]);
            FOMC_stavka = Convert.ToDouble(temp[3]);
            Kid1_stavka = Convert.ToInt32(temp[4]);
            Kid3_stavka = Convert.ToInt32(temp[5]);
            Invalid_stavka = Convert.ToInt32(temp[6]);
            Invalid_o_stavka = Convert.ToInt32(temp[7]);
            Mrot_stavka = Convert.ToInt32(temp[8]);
            
        }
        public double itogZarplata=0;
        /// <summary>
        /// Расчет зарплаты 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Raschet_Click(object sender, RoutedEventArgs e)
        {

            DateTime StrartdateTime;
            //ищем ближайшуюю предыдущую выплату ЗП
            var StartVar = DB.queryScalar($"SELECT [Data]  " +
                $"FROM[BD_Zarplata].[bd_zarplta].[zp]  " +
                $"Where[Sotrudnik_idSotrudnik] = {CB_SotrudID.SelectedItem} AND[Data] < '{CB_Date.SelectedItem}' " +
                $"order by Data desc");
            //Если не находим начинаем считать зарплату сначала месяца
            if(StartVar=="null")
            {
               
                StrartdateTime = calendar.DisplayDate;
                StrartdateTime= StrartdateTime.AddDays(-StrartdateTime.Day+1);
            }
            //иначе считаем с него
            else { StrartdateTime = Convert.ToDateTime(StartVar); }
            //считаем количество рабочих дней
            int KolvoRabDney = Convert.ToInt32(DB.queryScalar($"SELECT COUNT(*) FROM[BD_Zarplata].[bd_zarplta].[graphik_rabot] " +
                $"where[Sotrudnik_idSotrudnik] = {CB_SotrudID.SelectedItem} " +
                $"AND DATE >= '{StrartdateTime}' " +
                $"AND DATE < '{CB_Date.SelectedItem}' " +
                $"AND[StatusDay] = 'рабочий'   "));
            double DnevDohod = Convert.ToInt32(L_Oklad.Content) / KolvoRabDney;
            double OkladMes = 0;
            for (DateTime i = StrartdateTime; i < calendar.DisplayDate; i=i.AddDays(1))
            {
                string statusDay = DB.queryScalar($"SELECT [StatusDay] FROM[BD_Zarplata].[bd_zarplta].[graphik_rabot]" +
                    $" where[Sotrudnik_idSotrudnik] =  {CB_SotrudID.SelectedItem} AND DATE = '{i}'").ToString();
                string statusSotrud = DB.queryScalar($"SELECT [StatusSotrud] FROM[BD_Zarplata].[bd_zarplta].[graphik_rabot]" +
                    $" where[Sotrudnik_idSotrudnik] =  {CB_SotrudID.SelectedItem} AND DATE = '{i}'").ToString();
                Manager.UpdateLabel(i.ToString());
                //начисление зп в обычный день
                if(statusDay.LastIndexOf("рабочий")!=-1 && statusSotrud.LastIndexOf("вышел")!=-1)
                {
                    OkladMes += DnevDohod;
                }
                //начисление зп за выход выходной
                else if (statusDay.LastIndexOf("выходной") != -1 && statusSotrud.LastIndexOf("вышел") != -1)
                {
                    OkladMes += DnevDohod*2;
                }
                else 
                {
                //расчет больничного
                }
            }
            
            Vichet = Convert.ToInt32( DB.queryScalar($"SELECT  [Vichet] FROM[BD_Zarplata].[bd_zarplta].[zp] where[Data] = '{CB_Date.SelectedItem}' AND [Sotrudnik_idSotrudnik] = {CB_SotrudID.SelectedItem}"));
             Nadbav = Convert.ToInt32(DB.queryScalar($"SELECT [Nadbav] FROM[BD_Zarplata].[bd_zarplta].[zp] where[Data] = '{CB_Date.SelectedItem}' AND [Sotrudnik_idSotrudnik] = {CB_SotrudID.SelectedItem}"));
            //сумма зарплаты работника до вычета налогов
            double FactDohod = OkladMes + Nadbav - Vichet;

            double nalogBase = FactDohod;

            int CountZdKids = Convert.ToInt32(DB.queryScalar($"SELECT [num_zd_kids]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHEre [idSotrudnik]={CB_SotrudID.SelectedItem}"));
            int CountInvalidKids = Convert.ToInt32(DB.queryScalar($"SELECT [num_invalid_kids]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHEre [idSotrudnik]={CB_SotrudID.SelectedItem}"));
            int CountInvalidOpekaKids = Convert.ToInt32(DB.queryScalar($"SELECT [opeka]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHEre [idSotrudnik]={CB_SotrudID.SelectedItem}"));
            int SpecStatus = Convert.ToInt32(DB.queryScalar($"SELECT [SpecStatus]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHEre [idSotrudnik]={CB_SotrudID.SelectedItem}"));

            if (SpecStatus == 1) nalogBase = nalogBase - 3000;
            else if (SpecStatus == 2) nalogBase = nalogBase - 500;

            if (CountZdKids <= 2) nalogBase = nalogBase - (Kid1_stavka * CountZdKids);
            else if (CountZdKids > 2) nalogBase = nalogBase - (Kid1_stavka * 2 + Kid3_stavka * (CountZdKids - 2));

            nalogBase = nalogBase - CountInvalidKids * Invalid_stavka;
            nalogBase = nalogBase - CountInvalidOpekaKids * Invalid_o_stavka;
            //Ндфл которое платит работник
            double NdFl = (nalogBase / 100 * NDFL_stavka);
            L_Ndfl.Content = NdFl;
            //Итоговая зарплата на руки работнику
             itogZarplata = FactDohod - NdFl;
            //налоги  уплачеваемые работадателем
            L_FCC.Content =  (FactDohod / 100 * FCC_stavka);
            L_FOMC.Content = (FactDohod / 100 * FOMC_stavka);
            L_pfr.Content = (FactDohod / 100 * PFR_stavka);
            L_Travmatizm.Content =  (FactDohod / 100 * TRavmat_stavka);
            BTN_Export.IsEnabled = true;

        }
        private void BTN_Export_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            ex.SheetsInNewWorkbook = 1;
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            ex.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);

            // sheet.Range["A1", "K1"].Merge(); - объединение ячеек вы заданном диапазоне
            // sheet.Cells.Range["A1", "A7"].Font.Bold = true; включение жирного текста
            //sheet.Cells[1, 1] = "String" присвоить значение ячейки
            //sheet.Cells.WrapText = true; перенос текста в ячейках
            // sheet.Range["B13", "B13"].BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic);
            //рамка для ячейки

            sheet.Range["A1", "Q2"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            sheet.Range["A1", "B2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            sheet.Cells.Font.Name = "Times New Roman"; //задание шрифта
            sheet.Cells.Font.Size = "12"; 
            //sheet.Rows[x].RowHeight = 40;
            sheet.Columns[1].ColumnWidth = 30;
            sheet.Columns[6].ColumnWidth = 30;
            sheet.Cells.Range["A1", "F4"].Font.Bold = true;
            
            sheet.Cells[1, 1] = "Расчетный листок за "+ Convert.ToDateTime(CB_Date.SelectedItem).ToShortDateString();
            sheet.Cells[2, 1] = "ООО";
            sheet.Cells[3, 1] = "Работник: "+CB_FIO.SelectedItem;
            sheet.Cells[4, 1] = "Табельный номер: "+CB_SotrudID.SelectedItem;
            sheet.Cells[4, 6] = "Должность: " + L_DoljnostTitle.Content;
            sheet.Cells.Range["A5", "H6"].Font.Italic = true;
            sheet.Cells.Range["A5", "H5"].BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic);

            sheet.Cells[5, 1] = "Вид";
            sheet.Cells[5, 2] = "Период";
            sheet.Cells[5, 3] = "Дни";
            sheet.Cells[5, 4] = "Часы";
            sheet.Cells[5, 5] = "Сумма";
            sheet.Cells[5, 6] = "Вид";
            sheet.Cells[5, 7] = "Период";
            sheet.Cells[5, 8] = "Сумма";
            
            sheet.Range["A5", "H15"].BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic);
            
            sheet.Range["A1", "H1"].Merge();
            sheet.Range["A2", "H2"].Merge();
            sheet.Range["A3", "E3"].Merge();
            sheet.Range["F4", "H4"].Merge();

            sheet.Cells[6, 1] = "1. Начислено";
            sheet.Cells[7, 1] = "Оклад";
            sheet.Cells[7, 5] = L_Oklad.Content;
            sheet.Cells[8, 1] = "Дополнительные выплаты";
            sheet.Cells[8, 5] = Nadbav;
            sheet.Cells[9, 1] = "Больничные пособия";
            sheet.Cells[11, 1] = "Всего начислено:";
            sheet.Cells[11, 5] = "=СУММ(E7:E10)";

            sheet.Cells[12, 1] = "3. Взносы в ПФР";
            sheet.Cells[13, 1] = "Страховые взносы в ПФР (страховая часть 22%)";
            sheet.Cells[13, 5] = L_pfr.Content;

            sheet.Cells[6, 6] = "2.Удержано";
            sheet.Cells[7, 6] = "НДФЛ по ставке 13%";
            sheet.Cells[7, 8] = L_Ndfl.Content;
            sheet.Cells[8, 6] = "Иные удержания";
            sheet.Cells[8, 8] = Vichet;
            sheet.Cells[11, 6] = "Всего удержано:";
            sheet.Cells[11, 8] = "=СУММ(H7:H10)";
            sheet.Cells[12, 6] = "Сумма к выплате";
            sheet.Cells[12, 8] = itogZarplata;
            sheet.Cells[13, 6] = "Зачислено на счёт№" + DB.queryScalar("SELECT [SchetZachisl] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE" + CB_SotrudID.SelectedItem);
            sheet.Cells[14, 6] = "Выдано наличными";
            sheet.Cells[14, 8] = itogZarplata;

            sheet.Cells.WrapText = true;
            ex.Visible = true;

        }
        private void CB_SotrudID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB_FIO.SelectedIndex = CB_SotrudID.SelectedIndex;
        }

        private void CB_FIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTN_Export.IsEnabled = false;
            BTN_Raschet.IsEnabled = false;
            CB_Date.Items.Clear();
            CB_SotrudID.SelectedIndex = CB_FIO.SelectedIndex;
            DB.LoadDataComboBox(CB_Date, "SELECT [Data] FROM [BD_Zarplata].[bd_zarplta].[zp] wHERE [Sotrudnik_idSotrudnik]= " + CB_SotrudID.SelectedItem,0);
            L_IdDoljnost.Content = DB.queryScalar("SELECT [idDoljnost],[full_name] FROM[BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]=" + CB_SotrudID.SelectedItem);
            L_DoljnostTitle.Content = DB.queryScalar("SELECT [title] FROM[BD_Zarplata].[bd_zarplta].[doljnost] WHERE [idDoljnost]=" + L_IdDoljnost.Content);
            L_Oklad.Content = DB.queryScalar("SELECT   [Oklad]  FROM [BD_Zarplata].[bd_zarplta].[doljnost] where [idDoljnost] =" + L_IdDoljnost.Content);
            TRavmat_stavka =Convert.ToDouble( DB.queryScalar("SELECT   [Travmat]  FROM [BD_Zarplata].[bd_zarplta].[doljnost] where [idDoljnost] =" + L_IdDoljnost.Content));
        }

        private void CB_Date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar.DisplayDate = Convert.ToDateTime(CB_Date.SelectedItem);
            calendar.SelectedDate = Convert.ToDateTime(CB_Date.SelectedItem);
            BTN_Raschet.IsEnabled = true;
        }
    }
}
