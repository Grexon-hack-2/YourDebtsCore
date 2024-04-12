using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IDebtService
    {
        Task<string> InsertDebt(DebtRegisterModel debtRegister);
        Task<string> InsertPay(PayDebtsModel debtRegister);
        Task<string> InsertOtherDebt(OtherDebtsRequestModel debt, Guid adminUser);
        Task<List<OtherDebtsResponseModel>> GetAllOtherDebts(Guid userAdmin);
        Task<OtherDebtsResponseModel> GetOtherDebtById(Guid userAdmin, Guid debtorId);
    }

    public class DebtService: IDebtService
    {
        private readonly IDebtRepository _debtRepository;

        public DebtService(IDebtRepository debtRepository)
        {
            _debtRepository = debtRepository;
        }

        public async Task<string> InsertDebt(DebtRegisterModel debtRegister)
        {
            return await _debtRepository.InsertDebt(debtRegister);
        }

        public async Task<string> InsertPay(PayDebtsModel debtRegister)
        {
            return await _debtRepository.InsertPay(debtRegister);
        }

        public async Task<string> InsertOtherDebt(OtherDebtsRequestModel debt,Guid adminUser)
        {
            return await _debtRepository.InsertOtherDebt(debt,adminUser);
        }

        public async Task<List<OtherDebtsResponseModel>> GetAllOtherDebts(Guid userAdmin)
        {
            return await _debtRepository.GetDataOtherDebts(userAdmin);
        }

        public async Task<OtherDebtsResponseModel> GetOtherDebtById(Guid userAdmin, Guid debtorId)
        {
            return await _debtRepository.GetDataOtherDebtById(userAdmin,debtorId);
        }
    }
}
