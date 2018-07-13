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
using Finanzas.Lib.Extensions;

namespace Finanzas.Forms.Usuarios
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
            txtNombre.Focus();
        }

        public frmEdición(Usuario usuario)
            : this()
        {
            if (usuario == null)
            {
                this.Text = "Nuevo usuario";
                ckEstado.Checked = true;
            }
            else
            {
                this.Text = "Edición de usuario";
                txtNombre.Text = usuario.Nombre;
                txtNombreCompleto.Text = usuario.NombreCompleto;
                ckEstado.Checked = usuario.Estado == 1;
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
            //if (String.IsNullOrWhiteSpace(txtNombre.Text))
            //{
            //    errorProvider1.SetError(txtNombre, "No puede estar vacío");
            //    new ToolTip().ShowError(this, txtNombre, "No puede estar vacío");
            //    result = false;
            //}
            //else
            //{
            //    errorProvider1.SetError(txtNombre, "");
            //}
            //if (String.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            //{
            //    errorProvider1.SetError(txtNombreCompleto, "No puede estar vacío");
            //    new ToolTip().ShowError(this, txtNombreCompleto, "No puede estar vacío");
            //    result = false;
            //}
            //else
            //{
            //    errorProvider1.SetError(txtNombreCompleto, "");
            //}
            result = ValidarTextBoxVacío(txtNombre, this, errorProvider1) &&
                        ValidarTextBoxVacío(txtNombreCompleto, this, errorProvider1);
            return result;
        }

        private bool ValidarTextBoxVacío(TextBox txt, IWin32Window window,
            ErrorProvider error)
        {
            bool result = true;
            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                error.SetError(txt, "No puede estar vacío");
                new ToolTip().ShowError(window, txt, "No puede estar vacío");
                result = false;
            }
            else
            {
                error.SetError(txt, "");
            }
            return result;
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text.Trim();
            }
        }

        public string NombreCompleto
        {
            get
            {
                return txtNombreCompleto.Text.Trim();
            }
        }

        public byte Estado
        {
            get
            {
                return (byte)(ckEstado.Checked ? 1 : 0);
            }
        }
    }
}
