using GestorDeEmails2.Data;
using GestorDeEmails2.Models;
using GestorDeEmails2.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GestorDeEmails2.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {

        private readonly GestorDeEmails2Context _connection;

        public UsuarioService(GestorDeEmails2Context connection)
        {
            _connection = connection;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _connection.Usuarios.Where(usuario => usuario.Correo == correo && usuario.Contrasenia == clave)
                .FirstOrDefaultAsync();

            return usuarioEncontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _connection.Usuarios.Add(modelo);
            await _connection.SaveChangesAsync();
            return modelo;
        }
    }
}
