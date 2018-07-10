using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finanzas.Lib.Extensions;
using CustomLibrary.Extensions.Collections;
using CustomLibrary.Extensions.Controls;
using Finanzas.Repositories;

namespace Finanzas.Forms.Usuarios
{
    public partial class frmListado : Form
    {
        public frmListado()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            dgvDatos.SetDataSource(from c in UsuariosRepository.ObtenerUsuarios()
                                   select new
                                   {
                                       c.Id,
                                       c.Nombre,
                                       c.NombreCompleto,
                                       Activo = c.Estado == 1,
                                       c.FechaAlta,
                                       c.FechaBaja
                                   });
            dgvDatos.Columns[0].Visible = false;
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                switch (c.Index)
                {
                    case 4: 
                    case 5:
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        break;
                    case 3:
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        break;
                    default:
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        break;
                }
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDatos.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
            }
        }

        private void frmListado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //using (var f = new frmEdición(IdCuenta))
            //{
            //    if (f.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            var mov = MovimientosRepository.Insertar(f.IdCuenta, f.Fecha, f.IdTransaccion, f.Importe);
            //            cbCuentas.SelectedValue = mov.IdCuenta;
            //            ConsultarDatos();
            //            dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == mov.Id);
            //        }
            //        catch (Exception ex)
            //        {
            //            CustomMessageBox.ShowError("Error al intentar grabar los datos: \n" + ex.Message);
            //        }
            //    }
            //}
        }
    }
}
