using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Repositories
{
    public static class MovimientosRepository
    {
        public static IList<Models.ViewModels.Movimiento> ObtenerMovimientosPorCuenta(int idCuenta)
        {
            using (var db = new GastosEntities())
            {
                var query = from c in db.Movimientos
                            join t in db.Transacciones on c.IdTransaccion equals t.Id
                            join r in db.Rubros on t.IdRubro equals r.Id
                            where c.IdCuenta == idCuenta
                            orderby c.FechaMovimiento descending
                            select new Models.ViewModels.Movimiento
                            {
                                Fecha = c.FechaMovimiento,
                                Rubro = r.Descripcion,
                                Transacción = t.Descripcion,
                                Importe = c.Importe,
                                Saldo = 0
                            };
                return query.ToList();
            }
        }
    }
}
