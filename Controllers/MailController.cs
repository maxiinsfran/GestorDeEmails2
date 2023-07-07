using GestorDeEmails2.Data;
using GestorDeEmails2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorDeEmails2.Controllers
{
    [Authorize]
    public class MailController : Controller
    {

        private readonly GestorDeEmails2Context _context;

        public MailController(GestorDeEmails2Context context)
        {
            _context = context;
        }

        // Get para los mails recibidos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string destinatario = User.FindFirst(ClaimTypes.Email)?.Value;
            var bandejaDeEntrada = await _context.Mail.Where(mail => mail.BandejaEntrada == true && mail.Destinatario.Contains(destinatario))
                .ToListAsync();

            return View(bandejaDeEntrada);
        
        }

        // Get para los mails enviados
        [HttpGet]
        public async Task<IActionResult> MailEnviados()
        {
            string remitente = User.FindFirst(ClaimTypes.Email)?.Value;
            var bandejaDeSalida = await _context.Mail.Where(mail => mail.BandejaSalida == true && mail.Remitente.Contains(remitente))
                .ToListAsync();

            return View(bandejaDeSalida);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var mail = await _context.Mail.FirstOrDefaultAsync(m => m.MailId == id);
            
            if(mail == null)
            {
                return NotFound();
            }

            return View(mail);
        
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MailId,Asunto,Contenido,Destinatario,Remitente")] Mail mail)
        {

            if (ModelState.IsValid)
            {
                _context.Add(mail);
                mail.BandejaSalida = true;
                mail.BandejaEntrada = true;
                mail.Remitente = User.FindFirst(ClaimTypes.Email)?.Value;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mail);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();  
            }

            var mail = await _context.Mail.FindAsync(id);
            if(mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MailId,Asunto,Contenido,Destinatario,Remitente")] Mail mail)
        {
            if (id != mail.MailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                { 
                    if (!MailExists(mail.MailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mail);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mail = await _context.Mail.FirstOrDefaultAsync(m => m.MailId == id);
            if(mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mail = await _context.Mail.FindAsync(id);
            _context.Mail.Remove(mail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailExists(int mailId)
        {
            return _context.Mail.Any(e => e.MailId == mailId);
        }
    }
}
