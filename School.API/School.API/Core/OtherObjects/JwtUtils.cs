using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace School.API.Core.OtherObjects
{
    public static class JwtUtils
    {
        static readonly IConfiguration? _configuration;
        public static string? ValidateJwtToken(string? token, byte[] secret)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = secret;//Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var ipAddress = jwtToken.Claims.First(x => x.Type == "IPAddress").Value;

                // return ipAddress from JWT token if validation successful
                return ipAddress;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
