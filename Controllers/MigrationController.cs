using EcommerceTrail.Data;
using EcommerceTrail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/[controller]")]
[ApiController]
public class MigrationController : ControllerBase
{
    private readonly EcomContext _context;

    public MigrationController(EcomContext context)
    {
        _context = context;
    }

    [HttpPost("migrate-passwords")]
    public async Task<IActionResult> MigratePasswords()
    {
        var users = await _context.Users.ToListAsync();
        var passwordHasher = new PasswordHasher<User>();
        int updatedCount = 0;

        foreach (var user in users)
        {
            // Check if password is already hashed (basic check, adjust as needed)
            if (!user.PasswordHash.StartsWith("A"))
            {
                user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
                updatedCount++;
            }
        }

        await _context.SaveChangesAsync();
        return Ok($"{updatedCount} passwords hashed successfully.");
    }
}
