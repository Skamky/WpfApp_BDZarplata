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
        public static string connectionString = "Data Source=localhost; Initial Catalog=BD_Zarplata; Integrated Security=true;";
        public static SqlConnection myConnection = new SqlConnection(connectionString);
        /// <summary>
        /// создание соединения с БД
        /// </summary>
        public static void OpenConnection()
        {
            
            if (myConnection.State == System.Data.ConnectionState.Closed)
            {
                myConnection.Open();
             
            }
                
        }
        public static void CloseConnection()
        {
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
        /// <summary>
        /// проверка на возможность установки соедениния с бд 
        /// </summary>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public  bool OpenClouseConnection(string connectStr)
        {
            try
            {
                SqlConnection userConnection = new SqlConnection(connectStr);

                if (userConnection.State == System.Data.ConnectionState.Closed)
                {
                    userConnection.Open();

                }
                if (userConnection.State == System.Data.ConnectionState.Open)
                {
                    userConnection.Close();
                    myConnection = userConnection;
                }
                return true;
            }catch(Exception ex) { MessageBox.Show("Произошла ошибка:\n" + ex.Message + "\n Проверьте корректность введенных данных");return false }

        }
    }
}
