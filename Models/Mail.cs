namespace GestorDeEmails2.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mail")]
    public class Mail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MailId { get; set; }

        public string? Asunto { get; set; }

        public string? Contenido { get; set; }

        public string? Destinatario { get; set; }

        [DisplayName("De")]
        public string? Remitente { get; set; }

        public bool BandejaEntrada { get; set; }    
        public bool BandejaSalida { get; set; }

    
    }
}
