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
    public partial class frmInputBox : Form
    {
        public frmInputBox()
        {
            InitializeComponent();
        }

        public frmInputBox(string formTitle, string caption) : this()
        {
            this.Text = formTitle;
            lblCaption.Text = caption;
        }

        public string Value { get; private set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Value = txtInput.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
