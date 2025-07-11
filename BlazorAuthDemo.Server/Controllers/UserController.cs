using BlazorAuthDemo.Server.Data;
using BlazorAuthDemo.Server.Helpers;
using BlazorAuthDemo.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAuthDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbcontext _context;
        public UserController(AppDbcontext context)
        {
            _context = context;
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API está rodando.");
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("User with this email already exists.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password =  PasswordHasherHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                // need to implements classe pra retorno das solicitações // return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                return Ok(user); 
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving user to the database.");
            }
        }
    }
}
