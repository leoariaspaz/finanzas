using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Repositories
{
    public static class CuentasRepository
    {
        public static IList<Cuenta> ObtenerCuentas()
        {
            using (var db = new GastosEntities())
            {
                var query = (from c in db.Cuentas select c)
                                .ToList()
                                .Select(
                                    c => new Cuenta
                                    {
                                        Id = c.Id,
                                        Descripcion = c.Descripcion,
                                        Estado = c.Estado,
                                        SaldoInicial = c.SaldoInicial
                                    });
                return query.ToList();
            }
        }

        internal static void Insertar(string descripción, byte estado, decimal saldoInicial)
        {
            using (var db = new GastosEntities())
            {
                if (db.Cuentas.Any(c => c.Descripcion == descripción))
                {
                    throw new Exception("Ya existe una cuenta con este nombre.");
                }
                var id = db.Cuentas.Max(c => c.Id) + 1;
                var cta = new Cuenta
                {
                    Id = id,
                    Descripcion = descripción.Trim(),
                    Estado = estado,
                    SaldoInicial = saldoInicial
                };
                db.Cuentas.Add(cta);
                db.SaveChanges();
            }
        }

        internal static Cuenta ObtenerCuentaPorId(int id)
        {
            using (var db = new GastosEntities())
            {
                return db.Cuentas.Find(id);
            }
        }

        internal static void Actualizar(int id, string descripción, byte estado, decimal saldoInicial)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Cuentas.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la cuenta con Id " + id);
                }
                var cta = db.Cuentas.Find(id);
                cta.Descripcion = descripción.Trim();
                cta.Estado = estado;
                cta.SaldoInicial = saldoInicial;
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int id)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Cuentas.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la cuenta con Id " + id);
                }
                var cta = db.Cuentas.Find(id);
                if (cta.Movimientos.Any())
                {
                    throw new Exception("No se puede eliminar esta cuenta: tiene transacciones relacionadas.");
                }
                db.Cuentas.Remove(cta);
                db.SaveChanges();
            }
        }
    }
}
