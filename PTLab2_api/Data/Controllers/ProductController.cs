using Microsoft.AspNetCore.Mvc;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.implimentations;
using PTLab2_api.Data.Services.Interfaces;

namespace PTLab2_api.Data.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var _response = _productService.GetAll();

            if (_response.Success)
            {
                return Ok(_response);
            }

            return NotFound(_response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var _response = _productService.GetById(id);

            if (_response.Success)
            {
                return Ok(_response);
            }

            return NotFound(_response);
        }
    }
}
