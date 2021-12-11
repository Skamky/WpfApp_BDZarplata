using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace BDZarplata.Classes
{

    class Manager
    {
        public static Frame MainFrame { get; set; }
        public static Label LabelStatus { get; set; }
        public static ProgressBar MainProgressBar { get; set; }
        //метод по изменению контента в объектах
        /// <summary>
        /// отладочный вывод сообщений в отдельной Label
        /// </summary>
        /// <param name="newContent">новое сообщение для вывода</param>
        public static void UpdateLabel(string newContent="") 
        {
            LabelStatus.Content = newContent;
        }
        
    }       
}
