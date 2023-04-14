using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Models;

namespace ECommerceAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdineController : ControllerBase
    {
        IBusiness business;

        public OrdineController(IBusiness business)
        {
            this.business = business;
        }

        [HttpGet("Get")]
        [Authorize(Roles = "Utente")]
        public IActionResult Get()
        {
            var userRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            if (!userRole.Contains("Utente"))
            {
                return Forbid();
            }

            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
                StringComparison.InvariantCultureIgnoreCase));
            if (IdUtenteEmailClaim == null)
            {
                return BadRequest("L'email non appartiene all'utente");
            }
            return Ok(business.GetOrdini(IdUtenteEmailClaim.Value));
        }

        //Per Amministratore
        [HttpDelete("Delete")]
        [Authorize(Roles = "Amministratore")]
        public IActionResult Delete(int idOrdine, string email)
        {
            try
            {
                var result = business.DeleteOrderByUser(idOrdine, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Per utente
        [HttpDelete("DeleteOrderByUser")]
        [Authorize(Roles = "Utente")]
        public IActionResult Delete(int idOrdine)
        {
            var userRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            if (!userRole.Contains("Utente"))
            {
                return Forbid();
            }

            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
             StringComparison.InvariantCultureIgnoreCase));

            try
            {
                var result = business.DeleteOrder(idOrdine, IdUtenteEmailClaim.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Amministratore")]
        public IActionResult Update(int idOrdine, int idStato, string Email)
        {
            try
            {
                var result = business.UpdateOrder(idOrdine, idStato, Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "Utente")]
        public IActionResult Insert(NuovoOrdine nuovoOrdine)
        {
            var userRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            if (!userRole.Contains("Utente"))
            {
                return Forbid();
            }

            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
             StringComparison.InvariantCultureIgnoreCase));

            if (business.InsertOrdine(nuovoOrdine, IdUtenteEmailClaim.Value))
            {
                return Ok("Ordine effettuato.");
            }
            else
            {
                return BadRequest(new { message = "Ordine non effettuato!" });
            }
        }
    }
}
