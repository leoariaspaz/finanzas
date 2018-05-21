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

        public decimal Importe { get; set; }

        public float Saldo { get; set; }
    }
}
