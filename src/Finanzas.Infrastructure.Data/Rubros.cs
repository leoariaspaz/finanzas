﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Finanzas.Infrastructure.Data
{
    public partial class Rubros
    {
        public Rubros()
        {
            Transacciones = new HashSet<Transacciones>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Transacciones> Transacciones { get; set; }
    }
}