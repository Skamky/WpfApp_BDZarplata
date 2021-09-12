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
using WpfApp_КурсоваяРабота2021_BDZarplata.Pages;
using WpfApp_КурсоваяРабота2021_BDZarplata.Classes;

namespace WpfApp_КурсоваяРабота2021_BDZarplata
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MainFrame.Navigate(new Page_Login());
            MainFrame.Navigate(new Page_Login());
            Manager.MainFrame = MainFrame;
            Manager.LabelStatus = LabelStatus1;
            Manager.MainProgressBar = ProgressBarStatus;

        }

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
