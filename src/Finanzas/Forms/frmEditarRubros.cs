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
                //cbCuentas.Items.AddRange(query.ToArray());
                dgvDatos.DataSource = query.ToList();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var f = new frmInputBox("Nuevo rubro", "Descripción:");
            bool salir = false;
            while (!salir)
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string v = f.Value.Trim();
                    if (!String.IsNullOrEmpty(v))
                    {
                        using (var db = new GastosEntities())
                        {
                            if (db.Rubros.Any(r => r.Descripcion == v))
                            {
                                MessageBox.Show("Ya existe el rubro ingresado.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                            db.Rubros.Add(new Rubro { Descripcion = v });
                            db.SaveChanges();
                        }
                        ConsultarDatos();
                        Posicionar(v);
                        salir = true;
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            string rubro = dgvDatos.Rows[rowindex].Cells[1].Value.ToString();
            var f = new frmInputBox("Nuevo rubro", "Descripción:", rubro);
            bool salir = false;
            while (!salir)
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string v = f.Value.Trim();
                    if (!String.IsNullOrEmpty(v))
                    {
                        using (var db = new GastosEntities())
                        {
                            var r = db.Rubros.FirstOrDefault(r1 => r1.Descripcion == rubro);
                            r.Descripcion = v;
                            db.SaveChanges();
                        }
                        ConsultarDatos();
                        Posicionar(v);
                        salir = true;
                    }
                }
            }
        }

        private void Posicionar(string descripción)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(descripción))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            //dgvDatos.Rows[rowIndex].Selected = true;
            dgvDatos.CurrentCell = dgvDatos.Rows[rowIndex].Cells[0];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (var db = new GastosEntities())
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                int id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
                var rubro = db.Rubros.FirstOrDefault(r => r.Id == id);
                if (MessageBox.Show(String.Format("¿Está seguro de que desea eliminar {0}?", rubro.Descripcion),
                    "Eliminar rubro", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (rubro.Transacciones.Any())
                    {
                        MessageBox.Show("No se puede eliminar este rubro: tiene transacciones relacionadas.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    db.Rubros.Remove(rubro);
                    db.SaveChanges();
                    ConsultarDatos();
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
        }
    }
}
