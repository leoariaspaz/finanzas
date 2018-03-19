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

namespace Finanzas.Forms
{
    public partial class frmConsultaMovimientos : Form
    {
        public frmConsultaMovimientos()
        {
            InitializeComponent();
        }

        private void frmConsultaMovimientos_Load(object sender, EventArgs e)
        {
            using (var db = new GastosEntities())
            {
                var query = from c in db.Cuentas
                            orderby c.Descripcion
                            where c.Estado == 1
                            select c;
                //cbCuentas.Items.AddRange(query.ToArray());
                cbCuentas.DataSource = query.ToList();
                cbCuentas.DisplayMember = "Descripcion";
                cbCuentas.ValueMember = "Id";
            }
            ConsultarMovimientos();
        }

        private void ConsultarMovimientos()
        {
            dgvMovimientos.DataSource = null;
            using (var db = new GastosEntities())
            {
                var query = from c in db.Movimientos
                            join t in db.Transacciones on c.IdTransaccion equals t.Id
                            join r in db.Rubros on t.IdRubro equals r.Id
                            where c.IdCuenta == IdCuenta
                            orderby c.FechaMovimiento descending
                            select new Models.ViewModels.Movimiento
                            {
                                Fecha = c.FechaMovimiento,
                                Rubro = r.Descripcion,
                                Transacción = t.Descripcion,
                                Importe = c.Importe,
                                Saldo = 0
                            };
                //query.ToList();
                dgvMovimientos.DataSource = query.ToList();
            }
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
                //return ((Cuenta)cbCuentas.SelectedValue).Id;

                //if (cbCuentas.SelectedValue != null)
                //{
                //    return Convert.ToInt32(cbCuentas.SelectedValue);
                //}
                //else
                //{
                //    return 0;
                //}
            }
        }

        private void cbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarMovimientos();
        }

        private void dgvMovimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvMovimientos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgvMovimientos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMovimientos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMovimientos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMovimientos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMovimientos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvMovimientos.Columns[3].DefaultCellStyle.Format = "C2";
            dgvMovimientos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMovimientos.Columns[4].DefaultCellStyle.Format = "C2";
            dgvMovimientos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
