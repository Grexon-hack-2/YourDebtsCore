using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Base.Validations
{
    public class ValidationToken
    {
        public static (DataUserModel, bool) Handler(HttpContext context)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = context.Request.Headers["Authorization"].ToString().Split(" ")[1];

            var expiredToken = tokenHandler.ReadJwtToken(token);

            if(expiredToken.ValidTo < DateTime.UtcNow)
            {
                return (new DataUserModel(), true);
            }

            string dataUserString = expiredToken.Claims.First(x =>
                x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

            DataUserModel DataUserObj = JsonConvert.DeserializeObject<DataUserModel>(dataUserString);

            return (DataUserObj, false);
        }
    }
}
