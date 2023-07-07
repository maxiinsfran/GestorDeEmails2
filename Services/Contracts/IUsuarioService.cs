using GestorDeEmails2.Models;

namespace GestorDeEmails2.Services.Contracts
{
    public interface IUsuarioService
    {

        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
