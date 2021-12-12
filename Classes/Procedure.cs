using System.Collections.Generic;
using System.Windows;

namespace BDZarplata.Classes
{
    class Procedure
    {
        /// <summary>
        /// Обновление значений указанной таблицы в БД
        /// </summary>
        /// <param name="NameTable">Имя таблицы</param>
        /// <param name="ColumnsName">Массив имен столбцов</param>
        /// <param name="ColumnsData">Массив значений этих столбцов</param>
        /// <param name="Where">Условие отбора</param>
        public static void UpdateTable(string NameTable, List<string> ColumnsName, List<string> ColumnsData, string Where)
        {
            string sqlComand = $"UPDATE {NameTable} SET {ColumnsName[0]} = {ColumnsData[0]}";
            if (ColumnsData.Count == ColumnsName.Count)
            {
                for (int i = 1; i < ColumnsData.Count; i++)
                {
                    sqlComand += $", {ColumnsName[i]} = {ColumnsData[i]}";
                }
                sqlComand += Where;
                Classes.DB.queryScalar(sqlComand);
            }
            else
            {
                MessageBox.Show("err UpdateTable \n Количество столбцов не совпадает с количеством значений");
            }
        }
        /// <summary>
        /// Добавление значений указанной таблицы в БД
        /// </summary>
        /// <param name="NameTable">Имя таблицы</param>
        /// <param name="ColumnsName">Массив имен столбцов</param>
        /// <param name="ColumnsData">Массив значений этих столбцов</param>
        public static void InsertTable(string NameTable, List<string> ColumnsName, List<string> ColumnsData)
        {

            string sqlComand = $"INSERT INTO {NameTable} ( {ColumnsName[0]}";
            if (ColumnsData.Count == ColumnsName.Count)
            {
                for (int i = 1; i < ColumnsName.Count; i++)
                {
                    sqlComand += $", {ColumnsName[i]}";
                }
                sqlComand += $") VALUES( {ColumnsData[0]} ";
                for (int i = 1; i < ColumnsData.Count; i++)
                {
                    sqlComand += $", {ColumnsData[i]}";
                }
                sqlComand += ")";
                DB.queryScalar(sqlComand);

            }
            else
            {
                MessageBox.Show("err InsertTable \n Количество столбцов не совпадает с количеством значений");
            }
        }

    }
}