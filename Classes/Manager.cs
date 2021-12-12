using System.Windows.Controls;
namespace BDZarplata.Classes
{
    class Manager
    {
        public static Frame MainFrame { get; set; }
        public static Label LabelStatus { get; set; }
        public static ProgressBar MainProgressBar { get; set; }
        /// <summary>
        /// отладочный вывод сообщений в отдельной Label
        /// </summary>
        /// <param name="newContent">новое сообщение для вывода</param>
        public static void UpdateLabel(string newContent = "")
        {
            LabelStatus.Content = newContent;
        }
    }
}
