﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        public String Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
