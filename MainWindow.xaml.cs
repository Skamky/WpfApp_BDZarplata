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
using BDZarplata.Pages;
using BDZarplata.Classes;

namespace BDZarplata
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            MainFrame.Navigate(new Page_Login());
            Manager.MainFrame = MainFrame;
            Manager.LabelStatus = LabelStatus1;
            Manager.MainProgressBar = ProgressBarStatus;

        }
        /// <summary>
        /// Возврат на предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Manager.MainFrame.GoBack();
            }
            catch (Exception)
            {
               
                MessageBox.Show("Это первая страница!");
            }
           
        }
    }
}
