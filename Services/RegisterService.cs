using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IRegisterService
    {
        string RegisterUser(RegisterModel register);
    }
    public class RegisterService: IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        public RegisterService(IRegisterRepository registerRepository) 
        { 
            _registerRepository = registerRepository;
        }

        public string RegisterUser(RegisterModel register)
        {
           try
           {
               return _registerRepository.Register_User(register);
           }
           catch (Exception ex)
           {
                return ex.Message;
           }
        }
    }

}
