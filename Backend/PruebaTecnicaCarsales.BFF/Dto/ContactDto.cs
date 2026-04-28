using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaCarsales.BFF.Dto
{
    public class ContactDto
    {
        [Required(ErrorMessage="El campo Nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage="El campo Teléfono es obligatorio")]
        [RegularExpression(@"^\d{9}$",ErrorMessage ="El telefono debe tener 9 digitos como minumo y máximo")]
        public string Telefono { get; set; }= string.Empty;
    }
}