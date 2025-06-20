using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaFAC.Context;
using PruebaFAC.Dto;
using PruebaFAC.Services;
using BCrypt.Net;

namespace PruebaFAC.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == dto.Password);

            if (customer == null)
                return Unauthorized("Usuario no encontrado.");

            var passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash);

            if (!passwordValid)
                return Unauthorized("Contraseña incorrecta.");

            var token = _jwtService.GenerateToken(customer.Email, customer.Id);

            return Ok(new
            {
                Token = token,
                Email = customer.Email,
                CustomerId = customer.Id
            });
        }
    }
}
