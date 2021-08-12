using Finanzas.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;

namespace Finanzas.Tools.ProofsOfConcepts
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var configBuilder = new ConfigurationBuilder();
            var configuration = configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            using var db = new GastosContext(configuration["ConnectionString"]);
            db.Rubros.Add(new Rubros { Descripcion = "Verdulería" });
            db.Rubros.Add(new Rubros { Descripcion = "Carnicería de vaca" });
            db.SaveChanges();
        }
    }
}
