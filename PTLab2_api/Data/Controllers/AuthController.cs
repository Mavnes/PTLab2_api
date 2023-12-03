using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.Interfaces;
using System.Xml.Linq;

namespace PTLab2_api.Data.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost()]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(string name, string email, string password)
        {
            var _response = _authService.Register(name, email, password);
            
            if (_response.Success) 
            {
                return CreatedAtAction(nameof(Register), new { id = _response.Data.Id }, _response);
            }
            
            return BadRequest(_response);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(string email, string password)
        {
            var _response = _authService.Login(email, password);

            if (_response.Success)
            {
                return Ok(_response);
            }

            return BadRequest(_response);
        }
    }
}
