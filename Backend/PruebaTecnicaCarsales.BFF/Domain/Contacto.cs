using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCarsales.BFF.Domain
{
    public class Contacto
    {

        public int Id {get;set;}
        public string Nombre {get;set;}= string.Empty;
        public string Telefono {get;set;}= string.Empty;
        
    }
}