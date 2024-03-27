using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Base.Autorization
{
    public interface IAuthorizationService
    {
        AuthorizationResponse GetAuthorizationToken(AuthorizationRequest authorization);
    }
}
