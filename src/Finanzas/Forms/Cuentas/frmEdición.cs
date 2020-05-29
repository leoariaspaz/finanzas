using CustomLibrary.Lib.Extensions;
using Finanzas.Lib.Extensions;
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

namespace Finanzas.Forms.Cuentas
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
            txtDescripción.Select();
            this.Text = "Nueva cuenta";
            txtSaldoInicial.Text = String.Format("{0:C2}", 0);
        }

        public frmEdición(Cuenta cuenta)
            : this()
        {
            this.Text = "Edición de cuenta";
            txtDescripción.Text = cuenta.Descripcion;
            ckHabilitada.Checked = cuenta.Estado == 1;
            txtSaldoInicial.Text = String.Format("{0:C2}", cuenta.SaldoInicial);
        }

        public string Descripción
        {
            get
            {
                return txtDescripción.Text;
            }
        }

        public byte Estado
        {
            get
            {
                return (byte)(ckHabilitada.Checked ? 1 : 0);
            }
        }

        public decimal SaldoInicial
        {
            get
            {
                return Decimal.Parse(txtSaldoInicial.Text);
            }
        }

        private void txtSaldoInicial_Leave(object sender, EventArgs e)
        {
            decimal saldo;
            if (!Decimal.TryParse(txtSaldoInicial.Text, out saldo))
            {
                saldo = 0;
            }
            txtSaldoInicial.Text = String.Format("{0:C2}", saldo);
        }

        private void frmEdición_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
            if (String.IsNullOrEmpty(txtDescripción.Text.Trim()))
            {
                errorProvider1.SetError(txtDescripción, "No puede estar vacío.");
                new ToolTip().ShowError(this, txtDescripción, "No puede estar vacío.", 3000);
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtDescripción, "");
            }
            return result;
        }
    }
}
