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

namespace Finanzas.Forms
{
    public partial class frmEditarRubros : Form
    {
        public frmEditarRubros()
        {
            InitializeComponent();
            using (var db = new GastosEntities())
            {
                var query = from r in db.Rubros
                            orderby r.Descripcion
                            select new { r.Id, r.Descripcion };
                //cbCuentas.Items.AddRange(query.ToArray());
                dgvDatos.DataSource = query.ToList();
            }
        }
    }
}
