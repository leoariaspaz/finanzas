using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finanzas.Models;
using Finanzas.Lib.Extensions;
using Finanzas.Repositories;
using CustomLibrary.Extensions.Collections;
using CustomLibrary.Extensions.Controls;

namespace Finanzas.Forms.Movimientos
{
    public partial class frmListado : Form
    {
        public frmListado()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmConsultaMovimientos_Load(object sender, EventArgs e)
        {
            cbCuentas.DataSource = CuentasRepository.ObtenerCuentas().OrderBy(r => r.Descripcion).ToList();
            cbCuentas.DisplayMember = "Descripcion";
            cbCuentas.ValueMember = "Id";
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            //dgvDatos.DataSource = MovimientosRepository.ObtenerMovimientosPorCuenta(IdCuenta)
            //                        .ToSortableBindingList();
            dgvDatos.SetDataSource(MovimientosRepository.ObtenerMovimientosPorCuenta(IdCuenta));
            dgvDatos.Columns[0].Visible = false;
        }

        public int IdCuenta
        {
            get
            {
                if (cbCuentas.SelectedValue is Int32)
                {
                    return Convert.ToInt32(cbCuentas.SelectedValue);
                }
                else
                {
                    return 0;
                }
            }
        }

        private void cbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarDatos();
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                switch (c.Index)
                {
                    case 1: 
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        break;
                    case 4:
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        break;
                    default:
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        break;
                }
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDatos.Columns[5].DefaultCellStyle.Format = "C2";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[6].DefaultCellStyle.Format = "C2";
            dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDatos.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new frmEdición(IdCuenta))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var mov = MovimientosRepository.Insertar(f.IdCuenta, f.Fecha, f.IdTransaccion, f.Importe);
                        cbCuentas.SelectedValue = mov.IdCuenta;
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == mov.Id);
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (decimal)dgvDatos.Rows[rowindex].Cells[0].Value;
            var m = MovimientosRepository.ObtenerMovimientoPorId(id);
            using (var f = new frmEdición(m))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        MovimientosRepository.Actualizar(m.Id, f.IdCuenta, f.Fecha, f.IdTransaccion, f.Importe);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError(ex.Message);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (decimal)dgvDatos.Rows[rowindex].Cells[0].Value;
            var m = MovimientosRepository.ObtenerMovimientoPorId(id);
            if (MessageBox.Show("¿Está seguro de que desea contrasentar el movimiento seleccionado?",
                "Contrasentar Movimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    MovimientosRepository.Contrasentar(m.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
            }
        }

        private void frmListado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }
    }
}
