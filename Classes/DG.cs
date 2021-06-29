﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp_КурсоваяРабота2021_BDZarplata.Classes
{
    class DG
    {
        /// <summary>
        /// Получение значения выбранной ячейки
        /// </summary>
        /// <param name="dataGrid1">DataGrid в котором находиться нужная ячейка</param>
        /// <param name="selectedColumn">Номер столбца (начиная с 0) в котором находиться ячейка. 
        /// Присвоить -1 для автоматического определения столбца </param>
        /// <returns>значение ячейки или ""</returns>
        public static string GetSelectCell(DataGrid dataGrid1, int selectedColumn = -1)
        {
            try
            {
                string result = "";

                if (selectedColumn == -1) { selectedColumn = dataGrid1.CurrentCell.Column.DisplayIndex; }
                var selectedCell = dataGrid1.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                if (cellContent is TextBlock)
                {
                    result = (cellContent as TextBlock).Text;
                }
                return result;
            }catch(Exception ex) { Manager.UpdateLabel("Ошибка в returnCell, возможно не выбрана ячейка для возврата \n" + ex.Message); return ""; }
        }
      

    }
}
