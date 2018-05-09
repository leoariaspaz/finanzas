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
    public partial class frmEditarCuentas : Form
    {
        public frmEditarCuentas()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ConsultarDatos();

            tableLayoutPanel1.RowStyles[2].Height = 0;
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute; 
        }

        private void ConsultarDatos()
        {
            using (var db = new GastosEntities())
            {
                var query = from r in db.Cuentas
                            orderby r.Id
                            select new
                            {
                                r.Id,
                                r.Descripcion,
                                r.SaldoInicial,
                                DescripciónEstado = (r.Estado == 1 ? "Activa" : "Baja"),
                                r.Estado
                            };
                dgvDatos.DataSource = query.ToList();
            }
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                //dgvDatos.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.AliceBlue;
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

            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            dgvDatos.Columns[1].HeaderText = "Descripción";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Saldo Inicial";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dgvDatos.Columns[2].DefaultCellStyle.Format = "C2";

            dgvDatos.Columns[3].HeaderText = "Estado";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            dgvDatos.Columns[4].Visible = false;
        }
    }
}
