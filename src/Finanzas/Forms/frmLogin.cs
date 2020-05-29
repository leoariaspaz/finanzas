using Finanzas.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finanzas.Lib.Extensions;
using Finanzas.Lib;
using CustomLibrary.Lib.Extensions;

namespace Finanzas.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var repo = new UsuariosRepository();
            if (repo.VerificarLoginUsuario(txtUsuario.Text, txtContraseña.Text))
            {
                errorProvider1.SetError(txtUsuario, "");
                Session.CurrentUser = repo.ObtenerUsuario(txtUsuario.Text);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                errorProvider1.SetError(txtUsuario, "El usuario o la contraseña son incorrectos.");
                new ToolTip().ShowError(this, txtUsuario, "El usuario o la contraseña son incorrectos.");
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
