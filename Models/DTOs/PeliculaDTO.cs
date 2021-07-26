using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static APIPeliculas.Models.Pelicula;

namespace APIPeliculas.Models.DTOs
{
    public class PeliculaDTO
    {
        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public String Nombre { get; set; }
        
        public String RutaImagen { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio.")]
        public String Descripcion { get; set; }
        
        public String Duracion { get; set; }
       
        public TipoClasificaion Clasificacion { get; set; }


        public int CategoriaId { get; set; }
       
        public Categoria Categoria { get; set; }
    }
}
