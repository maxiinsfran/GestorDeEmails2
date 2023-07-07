using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeEmails2.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Usuarios",
                newName: "UsuarioNombre");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "BandejaDeSalida",
                table: "Mail",
                newName: "BandejaSalida");

            migrationBuilder.RenameColumn(
                name: "BandejaDeEntrada",
                table: "Mail",
                newName: "BandejaEntrada");

            migrationBuilder.RenameColumn(
                name: "IdMail",
                table: "Mail",
                newName: "MailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioNombre",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "BandejaSalida",
                table: "Mail",
                newName: "BandejaDeSalida");

            migrationBuilder.RenameColumn(
                name: "BandejaEntrada",
                table: "Mail",
                newName: "BandejaDeEntrada");

            migrationBuilder.RenameColumn(
                name: "MailId",
                table: "Mail",
                newName: "IdMail");
        }
    }
}
