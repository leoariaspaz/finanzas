﻿using Finanzas.Models;
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
        public frmEdición(Transaccion transacción = null)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = transacción == null ? "Nueva transacción" : "Edición de transacción";
            if (transacción != null)
            {
                txtRubro.Text = transacción.Rubro.Descripcion;
                txtDescripción.Text = transacción.Descripcion;
                ckEsDébito.Checked = transacción.EsDebito;
                ckHabilitada.Checked = transacción.Estado == 1;
            }
        }
    }
}
