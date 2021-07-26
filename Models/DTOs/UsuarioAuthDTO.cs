using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Models.DTOs
{
    public class UsuarioAuthDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre usuario es obligatorio.")]
        public String Usuario { get; set; }
        [Required(ErrorMessage = "El password es obligatorio.")]
        [StringLength(10,MinimumLength =4,ErrorMessage ="El password debe contener de 4 a 10 caracteres.")]
        public String Password { get; set; }
    }
}
