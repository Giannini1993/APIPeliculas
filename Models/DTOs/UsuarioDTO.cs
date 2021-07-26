using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Models.DTOs
{
    public class UsuarioDTO
    {
        public String UsuarioA { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
