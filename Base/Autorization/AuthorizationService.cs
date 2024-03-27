using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;
using Newtonsoft.Json;

namespace YourDebtsCore.Base.Autorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly string _keyValue;


        public AuthorizationService(ILoginRepository loginRepository, string value)
        {
            _keyValue = value;
            _loginRepository = loginRepository;
        }

        private string GenerarToken(string dataUser)
        {

            var keyBytes = Encoding.ASCII.GetBytes(_keyValue);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, dataUser));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        public AuthorizationResponse GetAuthorizationToken(AuthorizationRequest authorization)
        {
            try
            {
                var usuario_encontrado = _loginRepository.Login(authorization);

                string tokenActive = GenerarToken(JsonConvert.SerializeObject(usuario_encontrado));

                return new AuthorizationResponse() { Token= tokenActive, Authorized = true, Msg = "usuario autorizado" };
            }
            catch (Exception ex) 
            {
                return new AuthorizationResponse() { Token = null, Authorized = false, Msg = ex.Message };
            }
        }
    }
}
