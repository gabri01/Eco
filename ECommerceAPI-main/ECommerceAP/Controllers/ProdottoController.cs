using System;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ECommerceAP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        IBusiness business;

        public ProdottoController(IBusiness business)
        {
            this.business = business;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(business.GetAllProdotti());
        }

        [HttpPost("InsertProdotto")]
        [Authorize(Roles = "Amministratore")]
        public long InsertProdotto(Prodotto prodotto)
        {
            return (business.InsertProdotto(prodotto));
        }


        [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "Amministratore")]
        public IActionResult Delete(int idProdotto)
        {
            try
            {
                var result = business.DeleteProduct(idProdotto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProdotto")]
        [Authorize(Roles = "Amministratore")]
        public IActionResult Update(Prodotto prodotto)
        {
            if (prodotto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(business.UpdateProduct(prodotto));
            }
        }

        [HttpGet("SearchByName")]
        public IActionResult SearchProducts(string nomeProdotto)
        {
            try
            {
                var result = business.SearchProductsByName(nomeProdotto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Prodotto cercato non trovato" + ex.Message);
            }
        }

        [HttpGet("SearchByCategory")]
        public IActionResult Search(string nomeCategoria)
        {
            try
            {
                var result = business.SearchProductByCategory(nomeCategoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Categoria cercata non trovata" + ex.Message);
            }
        }
    }
}

