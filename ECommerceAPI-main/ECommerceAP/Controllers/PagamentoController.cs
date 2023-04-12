using System;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAP.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PagamentoController : ControllerBase
   {
       IBusiness business;
       public PagamentoController(IBusiness business)
       {
           this.business = business;
       }
       [HttpGet("GetAll")]
       public IActionResult Get()
       {
           return Ok(business.GetAllPagamenti());
       }
   }

}