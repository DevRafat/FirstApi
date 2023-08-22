using Microsoft.AspNetCore.Identity;

namespace FirstApi.Tables
{
    public class AppUser
    {
        public long Id { get; set; }
        public required string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
