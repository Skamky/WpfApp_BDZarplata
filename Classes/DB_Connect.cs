using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.IO;
namespace WpfApp_КурсоваяРабота2021_BDZarplata.Classes
{
    class DB_Connect
    {
        public static string connectionString = "Data Source=SKAMKYPC; Initial Catalog=BD_Zarplata; Integrated Security=true;";
        public static SqlConnection myConnection = new SqlConnection(connectionString);
        /// <summary>
        /// создание соединения с БД
        /// </summary>
        public static void openConnection()
        {
            
            if (myConnection.State == System.Data.ConnectionState.Closed)
            {
                myConnection.Open();
             //   MessageBox.Show("Подключение прошло успешно!","Информация", MessageBoxButton.OK,MessageBoxImage.Information);
            }
                
        }
        public static void closeConnection()
        {
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
               // MessageBox.Show("Отключен От Базы данных", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
