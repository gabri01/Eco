using System;
using Models;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        IBusiness business;
        public UtenteController(IBusiness business)
        {
            this.business = business;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Serilog")["LOGIN"])
                .CreateLogger();
        }

        [HttpGet("Get")]
        [Authorize(Roles = "Amministratore")]
        public IActionResult Get()
        {
            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
                          StringComparison.InvariantCultureIgnoreCase));

            //if (business.AutenticazioneUtente(IdUtenteEmailClaim.Value)) //IdUtenteEmailClaim.Value
            //    return BadRequest(new { message = "Utente non registrato." });
            return Ok(business.GetUtenteByID(IdUtenteEmailClaim.Value));
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Utente")]
        public IActionResult Update(string Nome, string Cognome, string Email)
        {
            var userRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            if (!userRole.Contains("Utente"))
            {
                return Forbid();
            }

            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
                         StringComparison.InvariantCultureIgnoreCase));

            if (business.ModificaUtente(Nome, Cognome, Email))
                return Ok(new { message = "Aggiornamento effettuato con successo." });

            return Ok(business.ModificaUtente(Nome, Cognome, IdUtenteEmailClaim.Value));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(Login Utente)
        {
            if (business.EsisteUtente(Utente))
            {
                Log.Information(Utente.Email.ToString());
                return Ok(business.Login(Utente));
            }
            return BadRequest(new { message = "Utente non registrato" });
        }

        [HttpPost("Signup")]
        public IActionResult Insert(Registrazione Utente)
        {
            if (business.VerificaUtenteEsistente(Utente.Email))
                return BadRequest(new { message = "Utente già registrato." });

            return Ok(business.Registrazione(Utente));
        }
    }
}
