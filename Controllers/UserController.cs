using EcommerceTrail.Data;
using EcommerceTrail.DTO;
using EcommerceTrail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTrail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {
        private readonly EcomContext _context;
        private readonly AuthService _authService;


        public UserController(EcomContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        


        [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == register.Email);
        if (user != null)
        {
            return BadRequest("User already present");
        }

        var passwordHasher = new PasswordHasher<User>();
        var newUser = new User
        {
            Username = register.Username,
            Email = register.Email
        };

        newUser.PasswordHash = passwordHasher.HashPassword(newUser, register.PasswordHash);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.PasswordHash);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateJwtToken(user.Email);
            return Ok(new { token });
        }

        [Authorize]

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUser()
        {

            var users = await _context.Users
                   .Select(u => new UserData
                   {
                       Email = u.Email,
                       Username = u.Username
                   })
                    .ToListAsync();

            return Ok(users);


        }

        [Authorize]
    [HttpPatch("ResetPassword")]
    public async Task<ActionResult> ResetPassword(ResetPassword res)
    {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Email == res.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, res.PasswordHash);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Old password is incorrect");
            }

            user.PasswordHash = passwordHasher.HashPassword(user, res.NewPassword);
            await _context.SaveChangesAsync();

            _authService.ExpireToken(user.Email); // Invalidate old token

            var newToken = _authService.GenerateJwtToken(user.Email); // Issue new token

            return Ok(new { token = newToken });

        }






    }
}
