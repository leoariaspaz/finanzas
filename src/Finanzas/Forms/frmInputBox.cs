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

namespace Finanzas.Forms
{
    public partial class frmInputBox : Form
    {
        public frmInputBox()
        {
            InitializeComponent();
            AllowEmptyValue = true;
        }

        public frmInputBox(string formTitle, string caption, string value = "") : this()
        {
            this.Text = formTitle;
            lblCaption.Text = caption;
            this.Value = value;
        }

        public string Value
        {
            get
            {
                return txtInput.Text;
            }
            private set
            {
                txtInput.Text = value;
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
            if (!AllowEmptyValue && String.IsNullOrEmpty(txtInput.Text.Trim()))
            {
                errorProvider1.SetError(txtInput, "No puede estar vacío.");
                new ToolTip().ShowError(this, txtInput, "No puede estar vacío.", 3000);
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtInput, "");
            }
            return result;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public bool AllowEmptyValue { get; set; }
    }
}
