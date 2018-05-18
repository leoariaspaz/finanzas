using Finanzas.Lib;
using Finanzas.Repositories;
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
using Finanzas.Models;

namespace Finanzas.Forms.Movimientos
{
    public partial class frmEdición : Form
    {
        public frmEdición()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            dtFecha.MaxDate = Configuration.CurrentDate;
            cbCuentas.Select();
        }

        public frmEdición(int idCuenta)
            : this()
        {
            this.Text = "Nuevo movimiento";
            cbCuentas.DataSource = CuentasRepository.ObtenerCuentas();
            cbCuentas.DisplayMember = "Descripcion";
            cbCuentas.ValueMember = "Id";
            cbCuentas.SelectedValue = idCuenta;

            cbRubros.DataSource = RubrosRepository.ObtenerRubros();
            cbRubros.DisplayMember = "Descripcion";
            cbRubros.ValueMember = "Id";

            CargarTransacciones();

            txtImporte.Text = String.Format("{0:C2}", 0);
        }

        private void cbRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTransacciones();
        }

        private void CargarTransacciones()
        {
            cbTransacciones.DataSource = TransaccionesRepository.ObtenerTransaccionesPorRubro(IdRubro);
            cbTransacciones.DisplayMember = "Descripcion";
            cbTransacciones.ValueMember = "Id";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool result = true;
            if (Importe < 0)
            {
                errorProvider1.SetError(txtImporte, "No puede ser menor que cero");
                new ToolTip().ShowError(this, txtImporte, "No puede ser menor que cero");
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtImporte, "");
            }
            return result;
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            decimal i;
            if (!Decimal.TryParse(txtImporte.Text, out i))
            {
                i = 0;
            }
            txtImporte.Text = String.Format("{0:C2}", i);
        }


        public int IdCuenta
        {
            get
            {
                return ((Cuenta)cbCuentas.SelectedValue).Id;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return dtFecha.Value.Date;
            }
        }

        public int IdRubro
        {
            get
            {
                if (cbRubros.SelectedValue is Rubro)
                {
                    return ((Rubro)cbRubros.SelectedValue).Id;
                }
                else
                {
                    return (int)cbRubros.SelectedValue;
                }
            }
        }

        public int IdTransaccion
        {
            get
            {
                return ((Transaccion)cbTransacciones.SelectedValue).Id;
            }
        }

        public decimal Importe
        {
            get
            {
                decimal i;
                if (!Decimal.TryParse(txtImporte.Text, out i))
                {
                    i = 0;
                }
                return i;
            }
        }
    }
}
