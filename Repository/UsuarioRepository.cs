using APIPeliculas.Data;
using APIPeliculas.Models;
using APIUsuarios.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _bd;
        public UsuarioRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool ExisteUsuario(string usuario)
        {
            if(_bd.Usuario.Any(x => x.UsuarioA == usuario)){
                return true;
            }
            return false;
        }

        public Usuario GetUsuario(int IdUsuario)
        {
            return _bd.Usuario.FirstOrDefault(c => c.Id == IdUsuario);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(c => c.UsuarioA).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges()>=0 ?true : false;
        }

        public Usuario Login(string Usuario, string password)
        {
            var user = _bd.Usuario.FirstOrDefault(x => x.UsuarioA == Usuario);
            if(user == null)
            {
                return null;
            }
            if(!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public Usuario Registro(Usuario Usuario, string password)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordHash(password, out passwordHash, out passwordSalt);
            Usuario.PasswordHash = passwordHash;
            Usuario.PasswordSalt = passwordSalt;
            _bd.Usuario.Add(Usuario);
            Guardar();
            return Usuario;
        }


        private bool VerificarPasswordHash(String password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i< hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CrearPasswordHash(String password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
