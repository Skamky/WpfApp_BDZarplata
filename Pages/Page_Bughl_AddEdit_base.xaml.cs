using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BDZarplata.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Bughl_AddEdit_base.xaml
    /// </summary>
    public partial class Page_Bughl_AddEdit_base : Page
    {
        /// <summary>
        /// Режим Добавления новых данных
        /// </summary>
        bool AddMode;
        string YEAR;
        public Page_Bughl_AddEdit_base(string year = "", string znach = "")
        {
            if (year == "")
            {
                AddMode = true;
            }
            else
            {
                AddMode = false;
            }

            InitializeComponent();
            TB_Year.Text = year;
            TB_Znach.Text = znach;
            YEAR = year;
        }
        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Year.Text == "")
            {
                MessageBox.Show("Поле год обязательно для заполнения!");
                return;
            }

            if (TB_Znach.Text == "")
            {
                MessageBox.Show("Поле значение обязательно для заполнения!");
                return;
            }
            List<string> columnNames = new List<string>() { "[Y]", "[Z]" };
            List<string> RowData = new List<string>() { TB_Year.Text, TB_Znach.Text };
            if (AddMode)
            {
                Classes.Procedure.InsertTable("[BD_Zarplata].[bd_zarplta].[base]", columnNames, RowData);
                MessageBox.Show("Вставка новой записи завершена!");
            }
            else
            {
                Classes.Procedure.UpdateTable("[BD_Zarplata].[bd_zarplta].[base]", columnNames, RowData, "WHERE [Y]= " + TB_Year.Text);
                MessageBox.Show("Обновление записи успешно!");

            }
            Classes.Manager.MainFrame.Navigate(new Pages.Page_BughalterInfo());
        }
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MR;
            if (YEAR == "")
            {
                MR = MessageBox.Show("Объект для удаления не выбран. \n  хотите выбрать его сейчас?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (MR == MessageBoxResult.Yes)
                {
                    Classes.Manager.MainFrame.Navigate(new Page_BughalterInfo());
                }
                else
                {
                    return;
                }
            }
            else if (MessageBoxResult.Yes == MessageBox.Show("Вы собираетесь безвозвратно удалить\n Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                //ТК в БД прописано каскадное удаление ,удаляем лишь из родительской таблицы 
                string comand = "DELETE FROM [bd_zarplta].[base] WHERE [Y] =" + YEAR;

                if (Classes.DB.queryData(comand) != -1)
                {
                    Classes.Manager.UpdateLabel("Готово.");
                    MessageBox.Show("Операция успешна выполнена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Classes.Manager.MainFrame.Navigate(new Page_BughalterInfo());
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при удалении");
                }
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

        private void shortOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            else if (!short.TryParse(textBox.Text + e.Text, out _))
            {
                MessageBox.Show("Максимальный размер числа не может быть больше 32000", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;

            }
        }
    }
}
