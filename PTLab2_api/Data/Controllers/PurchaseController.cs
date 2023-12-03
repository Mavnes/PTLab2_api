using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.implimentations;
using PTLab2_api.Data.Services.Interfaces;
using System.Xml.Linq;

namespace PTLab2_api.Data.Controllers
{
    [ApiController]
    [Route("purchase")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Route("get_by_user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByUser(int id)
        {
            var _response = _purchaseService.GetByUser(id);

            if (_response.Success)
            {
                return Ok(_response);
            }

            return NotFound(_response);
        }

        [HttpPost]
        [Route("make_purchase")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MakePurchase(int userId, int productId, string address)
        {
            var _response = _purchaseService.MakePurchase(userId, productId, address);

            if (_response.Success)
            {
                return CreatedAtAction(nameof(MakePurchase), new { id = _response.Data.Id }, _response);
            }

            return BadRequest(_response);
        } 
    }
}
