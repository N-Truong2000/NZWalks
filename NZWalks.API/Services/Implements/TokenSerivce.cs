using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Services.Implements
{
    public class TokenSerivce : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenSerivce(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CraeteJwtToken(IdentityUser user, List<string> roles)
        {
            //create claims
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var keyString = _configuration["Jwt:Key"];
            var keyBytes = Encoding.ASCII.GetBytes(keyString);
            var issuer = _configuration["Jwt:Issuer"];
            var Audience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(key, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(issuer, Audience, claim, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
