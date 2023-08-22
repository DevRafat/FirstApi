using FirstApi.Service.User;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using FirstApi.Service.loggedIn;

namespace FirstApi.MiddleWare
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;


        public JwtMiddleWare(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            //if (context.User.Identity.IsAuthenticated && context.User.FindFirst("exp") != null)
            //{
            //    var expirationUnixTime = long.Parse(context.User.FindFirst("exp").Value);
            //    var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;

            //    if (expirationDateTime <= DateTime.UtcNow)
            //    {
            //        await context.SignOutAsync();
            //        context.Response.StatusCode = 401; // Unauthorized
            //        await context.Response.WriteAsync("Token has expired.");
            //        return;
            //    }

            //}
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var token2 = handler.ReadJwtToken(token);

                var claims = token2.Claims.FirstOrDefault(c=>c.Type=="name");
               

            }
     


                  await _next(context);
        }
    }
}
