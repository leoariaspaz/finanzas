using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Repositories
{
    public class RubrosRepository
    {
        public static IList<Rubro> ObtenerRubros()
        {
            using (var db = new GastosEntities())
            {
                return db.Rubros.
                    ToList().
                    Select(r => new Rubro { Id = r.Id, Descripcion = r.Descripcion }).
                    OrderBy(r => r.Descripcion).
                    ToList();
            }
        }

        public static void Insertar(string descripción)
        {
            using (var db = new GastosEntities())
            {
                if (db.Rubros.Any(r => r.Descripcion == descripción))
                {
                    throw new Exception("Ya existe el rubro ingresado.");
                }
                db.Rubros.Add(new Rubro { Descripcion = descripción });
                db.SaveChanges();
            }
        }

        public static void Actualizar(int id, string descripción)
        {
            using (var db = new GastosEntities())
            {
                var r = db.Rubros.Find(id);
                r.Descripcion = descripción;
                db.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var db = new GastosEntities())
            {
                var rubro = db.Rubros.Find(id);
                if (rubro.Transacciones.Any())
                {
                    throw new Exception("No se puede eliminar este rubro: tiene transacciones relacionadas.");
                }
                db.Rubros.Remove(rubro);
                db.SaveChanges();
            }
        }
    }
}
