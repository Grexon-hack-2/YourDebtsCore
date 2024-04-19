using YourDebtsCore.Base.Autorization;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IRegisterService
    {
        string RegisterUser(RegisterModel register);
        Task<AuthorizationResponse> UpdateRegister(RegisterModel updateData, DataUserModel admin);
    }
    public class RegisterService: IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly IAuthorizationService _authorizationService;
        public RegisterService(IRegisterRepository registerRepository, IAuthorizationService authorizationService) 
        { 
            _registerRepository = registerRepository;
            _authorizationService = authorizationService;
        }

        public string RegisterUser(RegisterModel register)
        {
            return _registerRepository.Register_User(register);
        }

        public async Task<AuthorizationResponse> UpdateRegister(RegisterModel updateData, DataUserModel admin)
        {
            var result = await _registerRepository.UpdateRegister(updateData, admin.UserAdminID);

            var auth = new AuthorizationRequest()
            {
                User = admin.NameUser,
                Password = admin.Password
            };

            return _authorizationService.GetAuthorizationToken(auth, true);
        }
    }

}
