using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAP.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("Email")]
        public IActionResult Get()
        {
             try
             {
                MailAddress To = new MailAddress("");
                MailAddress From = new MailAddress("");

                MailMessage email = new MailMessage(From, To);
                email.Subject = "Email inviata con C#";
                email.Body = "Ciao " + "Destinatario" + "!";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("", "");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Send(email);
                return Ok("Email Inviata");
                }
                    catch
                {
                return BadRequest("Email non inviata");
             }
        }
    }
}
