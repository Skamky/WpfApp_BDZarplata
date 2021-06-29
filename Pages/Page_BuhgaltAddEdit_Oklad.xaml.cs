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
    /// Логика взаимодействия для Page_BuhgaltAddEdit_Oklad.xaml
    /// </summary>
    public partial class Page_BuhgaltAddEdit_Oklad : Page
    {

        public Page_BuhgaltAddEdit_Oklad(string IdDoljnost = "NOT")
        {
            InitializeComponent();
            if (IdDoljnost != "NOT")
            {
                TB_title.Text = Classes.DB.queryScalar($"Select * FROM [BD_Zarplata].[bd_zarplta].[doljnost] where [idDoljnost]={IdDoljnost}", 1);
                TB_Oklad.Text = Classes.DB.queryScalar($"Select * FROM [BD_Zarplata].[bd_zarplta].[doljnost] where [idDoljnost]={IdDoljnost}", 2);
                TB_Travmat.Text = Classes.DB.queryScalar($"Select * FROM [BD_Zarplata].[bd_zarplta].[doljnost] where [idDoljnost]={IdDoljnost}", 3);
                id = IdDoljnost;
            }
        }
        string id;
        int mrot = Convert.ToInt32(Classes.DB.queryScalar($"Select [MROT] FROM[BD_Zarplata].[bd_zarplta].[h]"));
        /// <summary>
        /// Проверка и сохранение при их корректности 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(TB_Oklad.Text) < mrot)
            {
                MessageBox.Show($"Оклад не может быть меньше МРОТ! ({mrot} руб) ");
                return;
            }
            if (Convert.ToDouble(TB_Travmat.Text)<0.2 || Convert.ToDouble(TB_Travmat.Text)>8.5)
            {
                MessageBox.Show("Процент травматизма должен быть в пределах от 0.2 до 8.5");
                return;
            }

            string[] columnname = { "[Oklad]", "[Travmat]" };
            string[] datacolumn = { TB_Oklad.Text, TB_Travmat.Text.Replace(',', '.') };
            MessageBoxResult boxResult = MessageBox.Show("Сохранить изменения?", "Запрос на сохранение", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                Classes.Procedure.UpdateTable("[bd_zarplta].[doljnost]", columnname, datacolumn, $"WHERE [idDoljnost]={id}");
                Classes.Manager.MainFrame.Navigate(new Pages.Page_BughalterInfo());
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
            else if (textBox.Text.Length > 2)
            {
                e.Handled = true;
                MessageBox.Show("Длина строки не может превышать 3 символов", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
