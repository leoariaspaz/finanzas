﻿using Finanzas.Lib;
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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        public bool Inicializar()
        {
            if (new frmLogin().ShowDialog() == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rubrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmEditarRubros()) f.ShowDialog();
        }

        private void consultarMovimientosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var f = new Movimientos.frmListado()) f.ShowDialog();
        }

        private void transaccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Transacciones.frmListado()) f.ShowDialog();
        }

        private void cuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Cuentas.frmListado()) f.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Usuarios.frmListado()) f.ShowDialog();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmCambiarContraseña()) f.ShowDialog();
        }
    }
}
