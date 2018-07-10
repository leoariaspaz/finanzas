using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Repositories
{
    public class UsuariosRepository
    {
        public bool VerificarLoginUsuario(string nombre, string contraseña)
        {
            Usuario usr = ObtenerUsuario(nombre);
            if (usr == null)
            {
                return false;
            }
            string hash = "";
            using (var alg = SHA512.Create())
            {
                alg.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                hash = BitConverter.ToString(alg.Hash);
            }
            return usr.Contraseña == hash;
        }

        internal Usuario ObtenerUsuario(string nombre)
        {
            using (var db = new GastosEntities())
            {
                return (from u in db.Usuarios where u.Nombre.ToLower() == nombre.ToLower() select u).FirstOrDefault();
            }
        }

        public static IEnumerable<Usuario> ObtenerUsuarios()
        {
            using (var db = new GastosEntities())
            {
                //var query = (from u in db.Usuarios select u)
                //                .ToList()
                //                .Select(
                //                    u => new Usuario
                //                    {
                //                        Id = u.Id,
                //                        FechaAlta = u.FechaAlta,
                //                        FechaBaja = u.FechaBaja,
                //                        Nombre = u.Nombre,
                //                        NombreCompleto = u.NombreCompleto,
                //                        Estado = u.Estado
                //                    });
                //return query.ToList();

                return (from u in db.Usuarios orderby u.Nombre select u).ToList();
            }
        }
    }

}
