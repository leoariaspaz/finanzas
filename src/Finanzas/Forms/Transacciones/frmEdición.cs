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

namespace Finanzas.Forms.Transacciones
{
    public partial class frmEdición : Form
    {
        public frmEdición(Transaccion transacción)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = transacción == null ? "Nueva transacción" : "Edición de transacción";
            txtDescripción.Select();
            //txtRubro.Text = transacción.Rubro.Descripcion;
            using (var db = new GastosEntities())
            {
                cbRubros.DataSource = db.Rubros.ToList();
                cbRubros.ValueMember = "Id";
                cbRubros.DisplayMember = "Descripcion";
                cbRubros.SelectedValue = transacción.Rubro.Id;
            }
            if (transacción.Id > 0)
            {
                txtDescripción.Text = transacción.Descripcion;
                ckEsDébito.Checked = transacción.EsDebito;
                ckHabilitada.Checked = transacción.Estado == 1;
            }
        }

        public string Descripción
        {
            get
            {
                return txtDescripción.Text;
            }
        }

        public bool EsDébito
        {
            get
            {
                return ckEsDébito.Checked;
            }
        }

        public byte Estado
        {
            get
            {
                return (byte) (ckHabilitada.Checked ? 1 : 0);
            }
        }

        public int IdRubro
        {
            get
            {
                return ((Rubro)cbRubros.SelectedItem).Id;
            }
        }
    }
}
