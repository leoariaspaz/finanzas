using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Repositories
{
    public class TransaccionesRepository
    {
        public static Transaccion Insertar(string descripción, bool esDébito, byte estado, int idRubro)
        {
            using (var db = new GastosEntities())
            {
                if (db.Transacciones.Any(t => t.IdRubro == idRubro && t.Descripcion == descripción))
                {
                    throw new Exception("Ya existe esta transacción para este rubro.");
                }
                var trx = new Transaccion
                {
                    Descripcion = descripción.Trim(),
                    EsDebito = esDébito,
                    Estado = estado,
                    IdRubro = idRubro
                };
                db.Transacciones.Add(trx);
                db.SaveChanges();
                return trx;
            }
        }

        public static void Actualizar(int id, string descripción, bool esDébito, byte estado, int idRubro)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Transacciones.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la transacción con Id " + id);
                }
                var trx = db.Transacciones.Find(id);
                trx.Descripcion = descripción.Trim();
                trx.EsDebito = esDébito;
                trx.Estado = estado;
                trx.IdRubro = idRubro;
                db.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var db = new GastosEntities())
            {
                if (!db.Transacciones.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la transacción con Id " + id);
                }
                var trx = db.Transacciones.Find(id);
                if (trx.Movimientos.Any())
                {
                    throw new Exception("No se puede eliminar esta transacción: tiene transacciones relacionadas.");
                }
                db.Transacciones.Remove(trx);
                db.SaveChanges();
            }
        }
    }
}
