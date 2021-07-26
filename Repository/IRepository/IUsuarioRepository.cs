using APIPeliculas.Models;
using System;
using System.Collections.Generic;

namespace APIUsuarios.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int IdUsuario);
        bool ExisteUsuario(String usuario);
        Usuario Registro(Usuario Usuario, String password);
        Usuario Login(String Usuario, String password);
        bool Guardar();
    }
}
