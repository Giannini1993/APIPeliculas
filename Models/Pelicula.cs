using System;
using
    System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        public String Nombre { get; set; }
        public String RutaImagen { get; set; }
        public String Descripcion { get; set; }
        public String Duracion { get; set; }
        public enum TipoClasificaion { Siete,Trece,Dieciseis,Dieciocho }
        public TipoClasificaion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
