using GestorDeEmails2.Models;
using GestorDeEmails2.Recursos;
using GestorDeEmails2.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeEmails2.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ActionResult Registrarse() 
        {
        
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {

            modelo.Contrasenia = Utilidades.EncriptarContrasenia(modelo.Contrasenia);

            Usuario usuarioCreado = await _usuarioService.SaveUsuario(modelo);

            if (usuarioCreado.IdUsuario > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No fue posible crear el usuario";

            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _usuarioService.GetUsuario(correo, Utilidades.EncriptarContrasenia(clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no valido";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.UsuarioNombre),
                new Claim(ClaimTypes.Email, usuarioEncontrado.Correo)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index", "Mail");

        }

    }
}
