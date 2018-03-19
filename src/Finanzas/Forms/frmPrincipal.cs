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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rubrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmEditarRubros().ShowDialog();
        }

        private void consultarMovimientosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmConsultaMovimientos().ShowDialog();
        }
    }
}
