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
                var query = from m in db.Movimientos
                            join t in db.Transacciones on m.IdTransaccion equals t.Id
                            join r in db.Rubros on t.IdRubro equals r.Id
                            where m.IdCuenta == idCuenta
                            orderby m.FechaMovimiento descending
                            select new Models.ViewModels.Movimiento
                            {
                                Fecha = m.FechaMovimiento,
                                Rubro = r.Descripcion,
                                Transacción = t.Descripcion,
                                Importe = m.Importe,
                                Saldo = 0,
                                Id = m.Id
                            };
                return query.ToList();
            }
        }

        public static Movimiento Insertar(int idCuenta, DateTime fecha, int idTransaccion, decimal importe)
        {
            using (var db = new GastosEntities())
            {
                //if (db.Cuentas.Any(c => c.Descripcion == descripción))
                //{
                //    throw new Exception("Ya existe esta cuenta con este nombre.");
                //}
                var id = db.Movimientos.Max(c => c.Id) + 1;
                var m = new Movimiento
                {
                    Id = id,
                    IdCuenta = idCuenta,
                    FechaMovimiento = fecha,
                    IdTransaccion = idTransaccion,
                    Importe = importe
                };
                db.Movimientos.Add(m);
                db.SaveChanges();
                return m;
            }
        }

        internal static Movimiento ObtenerMovimientoPorId(decimal idMovimiento)
        {
            using (var db = new GastosEntities())
            {
                return db.Movimientos.Find(idMovimiento);
            }
        }
    }
}
