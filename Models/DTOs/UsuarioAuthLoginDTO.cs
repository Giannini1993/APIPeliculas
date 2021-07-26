using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Models.DTOs
{
    public class UsuarioAuthLoginDTO
    {
        [Required(ErrorMessage = "El nombre usuario es obligatorio.")]
        public String Usuario { get; set; }
        [Required(ErrorMessage = "El password es obligatorio.")]
        public String Password { get; set; }
    }
}
