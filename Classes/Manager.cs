using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace WpfApp_КурсоваяРабота2021_BDZarplata.Classes
{

    class Manager
    {
        public static Frame MainFrame { get; set; }
        //метод по изменению контента в объектах
        /// <summary>
        /// отладочный вывод сообщений в отдельной Label
        /// </summary>
        /// <param name="newContent">новое сообщение для вывода</param>
        public static void UpdateLabel(string newContent="") 
        {
            MainWindow MW = Application.Current.MainWindow as MainWindow;
           //W.LabelStatus1.Content = newContent;
        }
        //public static void UpdateLabel(string newContent, Label label)
        //{
        //    MainWindow MW = Application.Current.MainWindow as MainWindow;
        //    MW.label.Content = newContent;
        //}
    }       
}
