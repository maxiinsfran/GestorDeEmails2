using Microsoft.EntityFrameworkCore;

namespace GestorDeEmails2.Data
{
    public class GestorDeEmails2Context : DbContext
    {
        public GestorDeEmails2Context(DbContextOptions<GestorDeEmails2Context> options)
                : base(options)
        {
        }

        public DbSet<GestorDeEmails2.Models.Mail> Mail { get; set; }
        public DbSet<GestorDeEmails2.Models.Usuario> Usuarios { get; set; }
    }
}
