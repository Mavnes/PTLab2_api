using Microsoft.AspNetCore.Mvc;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.implimentations;
using PTLab2_api.Data.Services.Interfaces;

namespace PTLab2_api.Data.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var _response = _userService.GetById(id);

            if (_response.Success)
            {
                return Ok(_response);
            }

            return NotFound(_response);
        }

        [HttpPut]
        [Route("update_sale/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSale(int id)
        {
            var _response = _userService.UpdateSale(id);

            if (_response.Success)
            {
                return Ok(_response);
            }

            return BadRequest(_response);
        }
    }
}
