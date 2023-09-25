using System;
using System.IdentityModel.Tokens.Jwt; // For JwtSecurityToken
using System.Security.Claims; // For Claim and ClaimTypes
using System.Text; // For Encoding
using Microsoft.Extensions.Configuration; // You need to have IConfiguration injected or available for Configuration["Jwt:Key"]
using Microsoft.IdentityModel.Tokens; // For SymmetricSecurityKey and SigningCredentials
using POS.Models; // Your user model namespace

namespace POS.secure
{
    public class Token
    {
        private readonly IConfiguration Configuration;

        public Token(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateJwtToken(TUserDetail user, string RoleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); // Use HmacSha256Signature

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Issuer"],
                claims: new[]
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, RoleName)
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
