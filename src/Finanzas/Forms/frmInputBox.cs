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
            if (this.ValidateChildren())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public bool AllowEmptyValue { get; set; }

        private void txtInput_Validating(object sender, CancelEventArgs e)
        {
            if (!AllowEmptyValue && String.IsNullOrEmpty(txtInput.Text.Trim()))
            {
                e.Cancel = true;
                new ToolTip().Show(this, txtInput, "");
                //PosBalloon(txtInput, "No puede estar vacío.");
            }
        }

        private void PosBalloon(TextBox textBox, string message)
        {
            errorProvider1.SetError(textBox, message);
            Point p = new Point(textBox.Left + textBox.Size.Width / 2, textBox.Top + textBox.Size.Height / 2 - 20);
            if (!textBox.Multiline) p.Y -= 25;
            p = textBox.FindForm().PointToClient(textBox.Parent.PointToScreen(p));
            var tt = new ToolTip();
            tt.IsBalloon = true;
            tt.ToolTipIcon = ToolTipIcon.Error;
            tt.ToolTipTitle = "Error";
            tt.Show(message, this, p, 3000);
        }

        private void txtInput_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtInput, "");
            //toolTip1.Hide(txtInput);
        }
    }
}
