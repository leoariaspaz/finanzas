using Finanzas.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Finanzas.Lib.Extensions;
using Finanzas.Repositories;

namespace Finanzas.Forms
{
    public partial class frmEditarRubros : Form
    {
        public frmEditarRubros()
        {
            InitializeComponent();
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            using (var db = new GastosEntities())
            {
                var query = from r in db.Rubros
                            orderby r.Descripcion
                            select new { r.Id, r.Descripcion };
                dgvDatos.DataSource = query.ToSortableBindingList(t => new Tuple<int, string>(t.Id, t.Descripcion));
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var f = new frmInputBox("Nuevo rubro", "Descripción:");
            f.AllowEmptyValue = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                string v = f.Value.Trim();
                try
                {
                    new RubrosRepository().Insertar(new Rubro { Descripcion = v });
                    ConsultarDatos();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
                dgvDatos.Posicionar(r => r.Cells[1].Value.ToString().Equals(v));
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            int id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
            string descripción = dgvDatos.Rows[rowindex].Cells[1].Value.ToString();
            var f = new frmInputBox("Edición de rubro", "Descripción:", descripción);
            f.AllowEmptyValue = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                string v = f.Value.Trim();
                try
                {
                    new RubrosRepository().Actualizar(id, v);
                    ConsultarDatos();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
                dgvDatos.Posicionar(r => r.Cells[1].Value.ToString().Equals(v));
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            int id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
            string descripción = dgvDatos.Rows[rowindex].Cells[1].Value.ToString();
            if (MessageBox.Show(String.Format("¿Está seguro de que desea eliminar {0}?", descripción),
                "Eliminar rubro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    new RubrosRepository().Eliminar(id);
                    ConsultarDatos();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
            }
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgvDatos.Rows[e.RowIndex].Cells[0].Style.BackColor = SystemColors.ButtonFace;
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.AliceBlue;
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[1].HeaderText = "Descripción";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[1].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        }

        private void frmEditarRubros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }
    }
}
