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

        [HttpGet("GetOrdine")]
        //[Authorize(Roles = "Utente")]
        [Authorize]
        public IActionResult Get()
        {
            //var userRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            //if (!userRole.Contains("Utente"))
            //{
            //    return Forbid();
            //}
            var IdUtenteEmailClaim = User.Claims.FirstOrDefault(e => e.Type.Equals("Email",
                StringComparison.InvariantCultureIgnoreCase));
            if (IdUtenteEmailClaim == null)
            {
                return BadRequest("L'email non appartiene all'utente");
            }
            return Ok(business.GetOrdini(IdUtenteEmailClaim.Value));
        }

        //Per Amministratore
        [HttpDelete("DeleteOrderByUser")]
        //[Authorize(Roles = "Amministratore")]
        [Authorize]
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
        [HttpDelete("DeleteOrder")]
        //[Authorize(Roles = "Utente")]
        [Authorize]
        public IActionResult Delete(int idOrdine)
        {
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

        [HttpPut("UpdateOrder")]
        //[Authorize(Roles = "Amministratore")]
        [Authorize]
        public IActionResult Update(int idOrdine, int idStato)
        {
            try
            {
                var result = business.UpdateOrder(idOrdine, idStato);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NewOrdine")]
        //[Authorize(Roles = "Utente")]
        [Authorize]
        public IActionResult Insert(NuovoOrdine nuovoOrdine)
        {
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
