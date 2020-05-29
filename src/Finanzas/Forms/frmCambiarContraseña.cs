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
using Finanzas.Repositories;
using Finanzas.Lib;
using CustomLibrary.Lib.Extensions;

namespace Finanzas.Forms
{
    public partial class frmCambiarContraseña : Form
    {
        public frmCambiarContraseña()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            txtAnterior.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                try
                {
                    UsuariosRepository.ReiniciarContraseña(Session.CurrentUser.Id, txtNueva.Text);
                    MessageBox.Show("Se cambió correctamente su contraseña.", "Cambiar Contraseña",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cambiar contraseña", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            bool result = ValidarTextBoxVacío(txtAnterior, this, errorProvider1) &&
                        ValidarTextBoxVacío(txtNueva, this, errorProvider1) &&
                        ValidarTextBoxVacío(txtRepetir, this, errorProvider1) &&
                        ValidarContraseñaRepetida();
            return result;
        }

        private bool ValidarContraseñaRepetida()
        {
            bool result = true;
            if (txtNueva.Text != txtRepetir.Text)
            {
                errorProvider1.SetError(txtRepetir, "Debe reingresar la nueva contraseña.");
                new ToolTip().ShowError(this, txtRepetir, "Debe reingresa la nueva contraseña.");
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtRepetir, "");
            }
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
    }
}
