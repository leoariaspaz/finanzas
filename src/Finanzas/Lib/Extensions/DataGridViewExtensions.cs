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

        public static ComponentModel.SortableBindingList<T> ToSortableBindingList<T, T2>(this IEnumerable<T2> query, Func<T2, T> convert)
        {
            ComponentModel.SortableBindingList<T> list = new ComponentModel.SortableBindingList<T>();
            foreach (var item in query)
            {
                list.Add(convert(item));
            }
            return list;
        }

        public static ComponentModel.SortableBindingList<T> ToSortableBindingList<T>(this IEnumerable<T> query)
        {
            ComponentModel.SortableBindingList<T> rubros = new ComponentModel.SortableBindingList<T>();
            foreach (var item in query)
            {
                rubros.Add(item);
            }
            return rubros;
        }
    }
}
