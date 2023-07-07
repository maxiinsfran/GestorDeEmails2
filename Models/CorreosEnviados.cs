using System.Collections.Generic;

namespace GestorDeEmails2.Models
{
    public class CorreosEnviados
    {
        public ICollection<Mail> Mails { get; set; }
    }
}
