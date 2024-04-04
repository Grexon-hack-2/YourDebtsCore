using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IDebtService
    {
        Task<string> InsertDebt(DebtRegisterModel debtRegister);
        Task<string> InsertPay(PayDebtsModel debtRegister);
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
    }
}
