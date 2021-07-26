using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static APIPeliculas.Models.Pelicula;

namespace APIPeliculas.Models.DTOs
{
    public class PeliculaUpdateDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El id es obligatorio.")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "El descripcion es obligatorio.")]
        public String Duracion { get; set; }
        public TipoClasificaion Clasificacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
