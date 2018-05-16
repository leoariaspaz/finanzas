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
        public void Insertar(Rubro rubro)
        {
            using (var db = new GastosEntities())
            {
                if (db.Rubros.Any(r => r.Descripcion == rubro.Descripcion))
                {
                    throw new Exception("Ya existe el rubro ingresado.");
                }
                db.Rubros.Add(rubro);
                db.SaveChanges();
            }
        }

        public void Actualizar(int id, string descripción)
        {
            using (var db = new GastosEntities())
            {
                var r = db.Rubros.Find(id);
                r.Descripcion = descripción;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
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
