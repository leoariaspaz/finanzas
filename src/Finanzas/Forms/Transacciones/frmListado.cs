using Finanzas.Models;
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
using Finanzas.Repositories;
using CustomLibrary.Extensions.Controls;

namespace Finanzas.Forms.Transacciones
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
        }

        private void frmEditarTransacciones_Load(object sender, EventArgs e)
        {
            cbRubros.DataSource = RubrosRepository.ObtenerRubros().OrderBy(r => r.Descripcion).ToList();
            cbRubros.DisplayMember = "Descripcion";
            cbRubros.ValueMember = "Id";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditarTransacciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRubros_SelectedValueChanged(object sender, EventArgs e)
        {
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            int idRubro = ((Rubro)cbRubros.SelectedItem).Id;
            var qry = TransaccionesRepository.ObtenerTransaccionesPorRubro(idRubro)
                                  .OrderBy(t => t.Descripcion)
                                  .Select(t => new
                                  {
                                      t.Id,
                                      t.Descripcion,
                                      t.EsDebito,
                                      Estado = t.Estado == 1 ? "Activo" : "Baja"
                                  });
            dgvDatos.DataSource = qry.ToList();
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
            }
            dgvDatos.Rows[e.RowIndex].Cells[0].Style.BackColor = SystemColors.ButtonFace;
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            //Id, Descripcion, EsDebito, Estado
            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Descripción";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Es débito";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[3].HeaderText = "Estado";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new frmEdición((int)cbRubros.SelectedValue))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var trx = TransaccionesRepository.Insertar(f.Descripción, f.EsDébito, f.Estado, f.IdRubro);
                        cbRubros.SelectedValue = trx.IdRubro;
                        ConsultarDatos();
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError(ex.Message);
                    }
                    dgvDatos.SetRow(r => r.Cells[1].Value.ToString().ToLower() == f.Descripción.Trim().ToLower());
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
            var trx = TransaccionesRepository.ObtenerTransaccionPorId(id);
            using (var f = new frmEdición(trx))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        TransaccionesRepository.Actualizar(trx.Id, f.Descripción, f.EsDébito, f.Estado, f.IdRubro);
                        cbRubros.SelectedValue = f.IdRubro;
                        ConsultarDatos();
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError(ex.Message);
                    }
                    dgvDatos.SetRow(r => Int32.Parse(r.Cells[0].Value.ToString()) == trx.Id);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
            var trx = TransaccionesRepository.ObtenerTransaccionPorId(id);
            if (MessageBox.Show(String.Format("¿Está seguro de que desea eliminar {0}?", trx.Descripcion),
                "Eliminar transacción", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    TransaccionesRepository.Eliminar(trx.Id);
                    ConsultarDatos();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
            }
        }
    }
}
