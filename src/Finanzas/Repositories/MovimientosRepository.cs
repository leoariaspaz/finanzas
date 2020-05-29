using Finanzas.Lib;
using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

namespace Finanzas.Repositories
{
    public static class MovimientosRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Models.ViewModels.Movimiento> ObtenerMovimientosPorCuenta(int idCuenta,
            DateTime desde, DateTime hasta)
        {
            List<Models.ViewModels.Movimiento> movimientos = null;
            using (var db = new GastosEntities())
            {
                var query = from m in db.Movimientos
                            join t in db.Transacciones on m.IdTransaccion equals t.Id
                            join r in db.Rubros on t.IdRubro equals r.Id
                            where m.IdCuenta == idCuenta &&
                                    m.FechaMovimiento >= desde &&
                                    m.FechaMovimiento <= hasta
                            orderby m.FechaMovimiento ascending
                            select new Models.ViewModels.Movimiento
                            {
                                Fecha = m.FechaMovimiento,
                                Rubro = r.Descripcion,
                                Transacción = t.Descripcion,
                                Contrasiento = m.EsContrasiento,
                                Importe = t.EsDebito ? -m.Importe : m.Importe,
                                Saldo = 0,
                                Id = m.Id
                            };
                movimientos = query.ToList();
            }
            //actualizo el saldo a mostrar
            var cta = CuentasRepository.ObtenerCuentaPorId(idCuenta);
            if (cta != null)
            {
                var saldoAnterior = cta.SaldoInicial;
                foreach (var m in movimientos)
                {
                    m.Saldo = saldoAnterior + m.Importe;
                    saldoAnterior = m.Saldo;
                }
            }

            Log.Debug("Movimientos:");
            using (var w = new System.IO.StringWriter())
            {
                new System.Xml.Serialization.XmlSerializer(movimientos.GetType()).Serialize(w, movimientos);
                Log.Debug(w.ToString());
            }
            return movimientos;
        }

        //private void Loggear<T>(T obj)
        //{
        //    using (var w = new System.IO.StringWriter())
        //    {
        //        new System.Xml.Serialization.XmlSerializer(typeof(T)).Serialize(w, obj);
        //        Log.Debug(w.ToString());
        //    }
        //}

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
                    Importe = importe,
                    FechaGrabacion = Configuration.CurrentDate,
                    IdUsuario = Session.CurrentUser.Id
                };
                db.Movimientos.Add(m);
                db.SaveChanges();
                return m;
            }
        }

        public static Movimiento ObtenerMovimientoPorId(decimal idMovimiento)
        {
            using (var db = new GastosEntities())
            {
                return db.Movimientos.Find(idMovimiento);
            }
        }

        public static void Actualizar(decimal id, int idCuenta, DateTime fecha, int idTransaccion, decimal importe)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Movimientos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el movimiento con Id " + id);
                }
                var m = db.Movimientos.Find(id);
                m.IdCuenta = idCuenta;
                m.FechaMovimiento = fecha;
                m.IdTransaccion = idTransaccion;
                m.Importe = importe;
                db.SaveChanges();
            }
        }

        internal static void Contrasentar(decimal id)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Movimientos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el movimiento con Id " + id);
                }
                var m = db.Movimientos.Find(id);
                if (m.EsContrasiento)
                {
                    throw new Exception("No existe el movimiento con Id " + id);
                }
                m.EsContrasiento = true;
                db.SaveChanges();
            }
        }
    }
}
