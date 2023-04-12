using System;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAP.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CorriereController : ControllerBase
        {
            IBusiness business;
            public CorriereController(IBusiness business)
            {
                this.business = business;
            }
            [HttpGet("GetAll")]
            public IActionResult Get()
            {
                return Ok(business.GetAllCorrieri());
            }
        }
    
}

