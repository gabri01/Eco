using System;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        IBusiness business;
        public CategoriaController(IBusiness business)
        {
            this.business = business;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(business.GetAllCategorie());
        }
    }
}

