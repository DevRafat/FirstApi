using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirstApi.Models;
using FirstApi.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FirstApi.Service.User
{
    public class UserService:IUserService
    {
        private readonly AplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AppUser> AddUser(UserRequestModel m)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(m.Password);

            var user = new AppUser()
            {
                FullName = m.FullName,
                Email = m.UserName,
                Password = passwordHash
            };
            var result = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<AppUser?> GetUser(AuthModel m)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(c => c.Email.ToUpper() == m.UserName.ToUpper());
        }

        public string CreateToken(string userName)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("name", userName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

       
    }
}
