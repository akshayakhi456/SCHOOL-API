using School.API.Core.OtherObjects;
using System.Text;

namespace School.API.Core.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var secret = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var ipAddress = JwtUtils.ValidateJwtToken(token, secret);
            var remoteIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (ipAddress != null)
            {
                context.Items["IPAddress"] = ipAddress;
                context.Items["RemoteIPAddress"] = remoteIpAddress;
            }

            await _next(context);
        }
    }
}
