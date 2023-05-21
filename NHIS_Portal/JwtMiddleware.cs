namespace NHIS_Portal
{
    //public class JwtMiddleware
    
       // namespace WebApi.Helpers;

   // using ClubBooking.DataAccess.DTO;
    //using ClubBooking.Helper;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                //var authid1 = jwtToken.Claims.Where(x => x.Type == "Name").FirstOrDefault();

                //var authid = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value);

                //var roles = jwtToken.Claims.Where(x => x.Type == ClaimTypes.Role).
                //    Select(x => x.Value).
                //    ToList();


                context.Items["User"] = new { email = email };

            }
            catch
            {

            }
        }
    }

}

