using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static APIPeliculas.Models.Pelicula;

namespace APIPeliculas.Models.DTOs
{
    public class PeliculaCreateDTO
    {
        public String Nombre { get; set; }
        public String RutaImagen { get; set; }
        public IFormFile Foto { get; set; }
        [Required(ErrorMessage = "El nombreFoto es obligatorio.")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "El descripcion es obligatorio.")]
        public int Duracion { get; set; }
        public TipoClasificaion Clasificacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
