using System; 
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Controls;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp_КурсоваяРабота2021_BDZarplata.Classes
{
    class DB
    {
        /// <summary>
        /// загрузка данных из БД в DataGrid
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="sql">SQL команда для выполнения (типа SELECT)</param>
        public static void loadDataGrid(DataGrid dataGrid, string sql)
        {
            DB_Connect.OpenConnection(); 
            SqlDataAdapter mysqldataAdapter = new SqlDataAdapter(sql, DB_Connect.connectionString); 
            DataTable dataTable = new DataTable();
            mysqldataAdapter.Fill(dataTable); 
            dataGrid.ItemsSource = dataTable.DefaultView; 
            DB_Connect.CloseConnection();
        }

        /// <summary>
        /// Загрузка данных из БД в список
        /// </summary>
        /// <param name="comboBox">Список куда будет загружены данные из БД</param>
        /// <param name="sql">SQL команда по которой происходит выборка</param>
        /// <param name="numberCol">Номер столбца из выборки (начиная с 0) который присваивается списку  </param>
        public static void LoadDataComboBox(ComboBox comboBox, string sql, int numberCol)
        {
            DB_Connect.OpenConnection(); 
            SqlCommand command = new SqlCommand(sql, DB_Connect.myConnection); 
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            { comboBox.Items.Add(reader.GetValue(numberCol).ToString()); }
            DB_Connect.CloseConnection();
        }
        /// <summary>
        ///  Загрузка данных из БД в список
        /// </summary>
        /// <param name="listbox">Список куда будет загружены данные из БД</param>
        /// <param name="sql">SQL команда по которой происходит выборка</param>
        /// <param name="numberCol">Номер столбца из выборки (начиная с 0) который присваивается списку  </param>
        public static void LoadDataListBox(ListBox listbox, string sql, int numberCol)
        {
            DB_Connect.OpenConnection();
            SqlCommand command = new SqlCommand(sql, DB_Connect.myConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            { listbox.Items.Add(reader.GetValue(numberCol).ToString()); }
            DB_Connect.CloseConnection();
        }
        /// <summary>
        /// Выполнение SQL Запроса к бд
        /// </summary>
        /// <param name="sql">SQL команда для выполнения</param>
        /// <returns>Первый столбец первой строки набора результатов или пустая ссылка</returns>
        public static object queryScalar(string sql)
        {
            Manager.UpdateLabel("Выполняю запрос...");
            object sqlValue = null;
            DB_Connect.OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand(sql, DB_Connect.myConnection);
                sqlValue = command.ExecuteScalar();
                Manager.UpdateLabel("Запрос успешно выполнен!");
                
            }
            catch (Exception ex) { MessageBox.Show("Произошла Ошибка \n" + ex.Message + "\n SQL Запрос: \n" + sql, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { DB_Connect.CloseConnection(); }
            if (sqlValue!=null) return sqlValue;
            else 
            {
                sqlValue = "null";
                return sqlValue;
            }
        }
        /// <summary>
        /// Выполнение запроса к БД C возвратом указанного столбца
        /// </summary>
        /// <param name="SQL_Comand">SQL команда для выполнения</param>
        /// <param name="Column"> Номер возвращаемого столбца</param>
        /// <returns>Указанный столбец первой строки набора результатов или пустая ссылка</returns>
        public static string queryScalar(string SQL_Comand, int Column)
        {
            try { 
            Manager.UpdateLabel("Выполняю запрос...");
            DB_Connect.OpenConnection();
            SqlCommand command = new SqlCommand(SQL_Comand, DB_Connect.myConnection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string temp = reader[Column].ToString();
            
            Manager.UpdateLabel("Запрос успешно выполнен!");    
                if (temp != null) return temp;
                else
                {
                    temp = "null";
                    return temp;
                }
            }
            catch (Exception ex) { MessageBox.Show("Произошла Ошибка \n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
            finally { DB_Connect.CloseConnection(); }
        }
        /// <summary>
        /// Выполнение запроса к БД
        /// </summary>
        /// <param name="SQL_Comand">SQL команда для выполнения</param>
        /// <param name="Column"> Название возвращаемого столбца</param>
        /// <returns>Указанный столбец первой строки набора результатов или пустая ссылка</returns>
        public static string queryScalar(string SQL_Comand, string Column)
        {
            try { 
            Manager.UpdateLabel("Выполняю запрос...");
            DB_Connect.OpenConnection();
            SqlCommand command = new SqlCommand(SQL_Comand, DB_Connect.myConnection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string temp = reader[Column].ToString();
          
            Manager.UpdateLabel("Запрос успешно выполнен!");
                if (temp != null) return temp;
                else
                {
                    temp = "null";
                    return temp;
                }
            }
            catch (Exception ex) { MessageBox.Show("Произошла Ошибка \n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);return ""; }
            finally { DB_Connect.CloseConnection(); }
        }
        /// <summary>
        /// Выполнение запроса к БД 
        /// </summary>
        /// <param name="SQL_Comand">SQL команда для выполнения</param>
        /// <param name="Columns">Массив индексов возвращаемых столбцов (начиная с 0) </param>
        /// <returns> массив значений Указанных столбцов первой строки набора результатов или пустая ссылка</returns>
        public static string[] queryScalar(string SQL_Comand, int[] Columns)
        {
            string[] results = new string[Columns.Length];
            try
            {
                Manager.UpdateLabel("Выполняю запрос...");
                DB_Connect.OpenConnection();
                SqlCommand command = new SqlCommand(SQL_Comand, DB_Connect.myConnection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                
                for (int i = 0; i < Columns.Length; i++)
                {
                    results[i] = reader[Columns[i]].ToString();
                }


                Manager.UpdateLabel("Запрос успешно выполнен!");
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла Ошибка при чтении данных \n" + ex.Message, "Ошибка\n", MessageBoxButton.OK, MessageBoxImage.Error);
                results = new[] { "" };
                return results;
            }
            finally { DB_Connect.CloseConnection(); }
        }
        /// <summary>
        /// Выполнение запроса к БД 
        /// </summary>
        /// <param name="SQL_Comand">SQL команда для выполнения(Не рекомендуется использовать эту перегрузку если в запросе содержаться столбцы с одинаковым именем)</param>
        /// <param name="Columns">Массив названий возвращаемых столбцов </param>
        /// <returns> массив значений Указанных столбцов первой строки набора результатов или пустая ссылка</returns>
        public static string[] queryScalar(string SQL_Comand, string[] Columns)
        {
            string[] results= new string[Columns.Length];
            try
            {
                Manager.UpdateLabel("Выполняю запрос...");
                DB_Connect.OpenConnection();
                SqlCommand command = new SqlCommand(SQL_Comand, DB_Connect.myConnection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                for (int i = 0; i < Columns.Length; i++)
                {
                    results[i] = reader[Columns[i]].ToString();
                }
                

                Manager.UpdateLabel("Запрос успешно выполнен!");
                return results;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Произошла Ошибка при чтении данных \n" + ex.Message, "Ошибка\n", MessageBoxButton.OK, MessageBoxImage.Error); 
                results =new[]{""};
                return results;
            }
            finally { DB_Connect.CloseConnection(); }
        }

        /// <summary>
        /// Запрос к бд с получением данных из таблицы (таблиц)
        /// </summary>
        /// <param name="SQL_Comand">SQL команда для выполнения(типа Select)</param>
        /// <param name="CountColumn">Количество столбцов для возврата</param>
        ///  <param name="CountString">Количество строк для возврата(возврат начинается с 1 строки возвращаемым запросом</param>
        /// <param name="Results">[c,s]Массив результатов где перебор с - столбца , s- строки</param>
        public static void ReturnTable(string SQL_Comand, out string[,] Results,int CountString=1,int CountColumn=1)
        {
             Results = new string[CountColumn,CountString];

            try { 
                Manager.UpdateLabel("Выполняю запрос...");
                DB_Connect.OpenConnection();
                SqlCommand command = new SqlCommand(SQL_Comand, DB_Connect.myConnection);
                SqlDataReader reader = command.ExecuteReader();
                int s = 0;
                    while (reader.Read() && s< CountString)
                    {
                        for (int с = 0; с < CountColumn; с++)
                        {
                            Results[с,s] = reader[с].ToString();
                        }
                        s++;
                    }


            Manager.UpdateLabel("Запрос успешно выполнен!");
           
        }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла Ошибка при чтении таблицы \n" + ex.Message, "Ошибка\n", MessageBoxButton.OK, MessageBoxImage.Error);
              
            }
            finally { DB_Connect.CloseConnection(); }

        }

        //Выполнение внешнего SQl Файла
        //Выполняет для подключения инструкцию Transact-SQL и возвращает количество задействованных в инструкции строк.
        public static bool queryData(string sql) 
        {
            Manager.UpdateLabel("Выполняю запрос...");
            DB_Connect.OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand(sql, DB_Connect.myConnection);
                command.ExecuteNonQuery();
                Manager.UpdateLabel("Запрос успешно выполнен!");
                
            }
            catch(Exception ex) { MessageBox.Show("Произошла Ошибка \n"+ ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
            finally { DB_Connect.CloseConnection(); }
            return true;

        }
        


    }
}
