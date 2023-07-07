using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace GestorDeEmails2.Models
{
    public class CorreosEntrantes
    {

        public ICollection<Mail> Mails { get; set; }
    }
}
