using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp_КурсоваяРабота2021_BDZarplata.Classes
{
    class Procedure
    {
        /// <summary>
        /// Обновление указанной таблицы в бд
        /// </summary>
        /// <param name="NameTable">Имя таблицы</param>
        /// <param name="ColumnsName">Массив имен столбцов</param>
        /// <param name="ColumnsData">Массив значений этих столбцов</param>
        /// <param name="Where">Условие отбора</param>
        public static void UpdateTable(string NameTable, string[] ColumnsName,string[] ColumnsData,string Where)
        {
            string sqlComand = $"UPDATE {NameTable} SET {ColumnsName[0]} = {ColumnsData[0]}";
            if (ColumnsData.Length == ColumnsName.Length)
            {
                for (int i = 1; i < ColumnsData.Length; i++)
                {
                    sqlComand += $", {ColumnsName[i]} = {ColumnsData[i]}";
                }
                sqlComand += Where;
                Classes.DB.queryScalar(sqlComand);
            }
            else MessageBox.Show("err UpdateTable \n Количество столбцов не совпадает с количеством значений");
        }
    }
}
