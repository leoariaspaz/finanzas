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
        }

        public frmEdición(int idCuenta) : this()
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
        }

        private void cbRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTransacciones();
        }

        private void CargarTransacciones()
        {
            cbTransacciones.DataSource = TransaccionesRepository.ObtenerTransaccionesPorRubro((int)cbRubros.SelectedValue);
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
            return true;
        }
    }
}
