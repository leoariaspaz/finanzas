using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Models.ViewModels
{
    public class Movimiento
    {
        public Decimal Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Rubro { get; set; }

        public string Transacción { get; set; }
        
        [System.ComponentModel.DisplayName("Contr.")]
        public bool Contrasiento { get; set; }

        public decimal Importe { get; set; }

        public decimal Saldo { get; set; }
    }
}
