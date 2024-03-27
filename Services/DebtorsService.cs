using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IDebtorsService
    {
        List<DebtorModel> GetDebtors(Guid ID);
        string AddNewClient(DebtorModel debtor, Guid userId);
    }
    public class DebtorsService : IDebtorsService
    {
        private readonly IDebtorsRepository _repository;
        public DebtorsService(IDebtorsRepository debtorsRepository)
        {
            _repository = debtorsRepository;
        }

        public  List<DebtorModel> GetDebtors(Guid ID)
        {
            return _repository.GetAll(ID);
        }

        public string AddNewClient(DebtorModel debtor, Guid userId)
        {
            return _repository.AddNewClient(debtor, userId);
        }
    }
}
