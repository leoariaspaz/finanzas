using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finanzas.Lib.Extensions
{
    public static class DataGridViewExtensions
    {
        [Obsolete("Se reemplaza por CustomLibrary.Extensions.Controls.DataGridViewExtensions.SetRow")]
        public static void Posicionar(this DataGridView grid, Func<DataGridViewRow, bool> condición)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (condición(row))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            if (rowIndex >= 0)
            {
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    if (grid.Columns[i].Visible)
                    {
                        grid.CurrentCell = grid.Rows[rowIndex].Cells[i];
                        return;
                    }
                }
            }
        }
    }
}
