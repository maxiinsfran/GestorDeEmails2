using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestorDeEmails2.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string? UsuarioNombre { get; set; }

        public string? Contrasenia { get; set; }

        public string? Correo { get; set; }
    }
}
