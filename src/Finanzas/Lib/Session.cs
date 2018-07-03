using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Lib
{
    public static class Session
    {
        public static Usuario CurrentUser { get; set; }
    }
}
