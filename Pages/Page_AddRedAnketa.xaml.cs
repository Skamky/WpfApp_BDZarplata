﻿using System;
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
    /// Логика взаимодействия для Page_AddRedAnketa.xaml
    /// </summary>
    public partial class Page_AddRedAnketa : Page
    {
        string IDSotrud;
        public Page_AddRedAnketa(string IdSotrud = "")
        {
            IDSotrud = IdSotrud;
            InitializeComponent();
            DB.LoadDataComboBox(CB_Doljnost, "SELECT [idDoljnost], [title] FROM [BD_Zarplata].[bd_zarplta].[doljnost]", 1);
            DB.LoadDataComboBox(CB_DoljnostID, "SELECT [idDoljnost], [title] FROM [BD_Zarplata].[bd_zarplta].[doljnost]", 0);
            if (IdSotrud != "")
            {
                TB_FIO.Text = DB.queryScalar("SELECT [full_name] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_FamilyStatus.Text = DB.queryScalar("SELECT [family_status] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_KidInvalid.Text = DB.queryScalar("SELECT [num_invalid_kids] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_KidInvalid_opek.Text = DB.queryScalar("SELECT [opeka] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_kidsZd.Text = DB.queryScalar("SELECT [num_zd_kids]  FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_SchetZachisl.Text = DB.queryScalar("SELECT [SchetZachisl] [SchetZachisl] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                TB_Staj.Text = DB.queryScalar("SELECT [Staj] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                CB_DoljnostID.SelectedItem = DB.queryScalar("SELECT [idDoljnost] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud).ToString();
                CB_Doljnost.SelectedIndex = CB_DoljnostID.SelectedIndex;
                CB_SpecStatus.SelectedIndex = Convert.ToInt32(DB.queryScalar("SELECT [SpecStatus] FROM [BD_Zarplata].[bd_zarplta].[sotrudnik] WHERE [idSotrudnik]= " + IdSotrud));
            }
        }
        /// <summary>
        /// соединение 2 списков для синхронного выбора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Doljnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB_DoljnostID.SelectedIndex = CB_Doljnost.SelectedIndex;
        }

        /// <summary>
        /// Фильтр int значений для TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        ////Проверка и внесение данных в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string[] columnNames = { "[full_name]", "[idDoljnost]", "[SpecStatus]" };
            string[] RowData = { TB_FIO.Text, CB_DoljnostID.SelectedItem.ToString(), CB_SpecStatus.SelectedIndex.ToString() };
            MessageBoxResult Result_SaveOrRed;
            if (TB_FIO.Text.Length < 2)
            {
                MessageBox.Show("Ошибка! Поле ФИО обязательно для заполнения");
                return;
            }
            if (TB_FamilyStatus.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[family_status]");
                Procedure.AddStr_to_array(RowData, TB_FamilyStatus.Text);
            }
            if (TB_KidInvalid.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[num_invalid_kids]");
                Procedure.AddStr_to_array(RowData, TB_KidInvalid.Text);
            }
            if (TB_KidInvalid_opek.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[opeka]");
                Procedure.AddStr_to_array(RowData, TB_KidInvalid_opek.Text);
            }
            if (TB_kidsZd.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[num_zd_kids]");
                Procedure.AddStr_to_array(RowData, TB_kidsZd.Text);
            }
            if (TB_SchetZachisl.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[SchetZachisl]");
                Procedure.AddStr_to_array(RowData, TB_SchetZachisl.Text);
            }
            if (TB_Staj.Text != "")
            {
                Procedure.AddStr_to_array(columnNames, "[Staj]");
                Procedure.AddStr_to_array(RowData, TB_Staj.Text);
            }

            if (IDSotrud == "") { Result_SaveOrRed = MessageBoxResult.No; }
            else
            {
                Result_SaveOrRed = MessageBox.Show
                    (
                    "Сохранить изменения?  \n \n Да - Сохранить изменения  существующей записи \n Нет - Создать новую запись \n Отмена - Возврат в окно редактирования",
                    "Добавить или Изменить?",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                    );
            }
            switch (Result_SaveOrRed)
            {
                //обновление существующей записи 
                case MessageBoxResult.Yes:
                    Procedure.UpdateTable("[BD_Zarplata].[bd_zarplta].[sotrudnik]", columnNames, RowData, "WHERE [idSotrudnik]= " + IDSotrud);
                    break;
                case MessageBoxResult.No:
                    // вставка новой записи 
                    Procedure.InsertTable("[BD_Zarplata].[bd_zarplta].[sotrudnik]", columnNames, RowData);
                    break;
                default:
                    Manager.UpdateLabel("Отменено пользователем");
                    return;   
            }
            Manager.MainFrame.Navigate(new Page_SotrudnikMainInfo());
        }
    }
}