using System;
using Models;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        }
        [HttpGet("GetByID")]
        [Authorize]
        public IActionResult Get()
        {
            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
                          StringComparison.InvariantCultureIgnoreCase));

            //if (business.AutenticazioneUtente(IdUtenteEmailClaim.Value)) //IdUtenteEmailClaim.Value
            //    return BadRequest(new { message = "Utente non registrato." });
            return Ok(business.GetUtenteByID(IdUtenteEmailClaim.Value));
        }

        [HttpPut("Modifica")]
        [Authorize]
        public IActionResult Update(string Nome, string Cognome, string Email)
        {
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
                return Ok(business.Login(Utente));
            return BadRequest(new { message = "Utente non registrato" });
        }

        [HttpPost("Registrazione")]
        public IActionResult Insert(Registrazione Utente)
        {
            if (business.VerificaUtenteEsistente(Utente.Email))
                return BadRequest(new { message = "Utente già registrato." });

            return Ok(business.Registrazione(Utente));
        }
    }
}

