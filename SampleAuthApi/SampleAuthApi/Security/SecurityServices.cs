using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleAuthApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleAuthApi.Security
{
    public interface ISecurityServices
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
    public class SecurityServices : ISecurityServices
    {

        private readonly AppSettings _appSettings;
        private readonly IUserServices _userServices;
        private readonly ICryptoServices _cryptoServices;

        public SecurityServices(IOptions<AppSettings> appSettings, IUserServices userServices, ICryptoServices cryptoServices)
        {
            _appSettings = appSettings.Value;
            _userServices = userServices;
            _cryptoServices = cryptoServices;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userServices.GetByUserName(model.Username);

            if(!_cryptoServices.VerifyPassword(model.Password, user.PasswordHash))
            {
                return null;
            }

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }


        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }), /*you can even ecrypt this before adding it to the token*/
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
