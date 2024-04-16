using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IHistoryService
    {
        Task<List<History_OtherDebt>> GetHistoryOtherDebt(Guid adminID);
        Task<List<History_Abono>> GetHistoryAbono(Guid adminID);
        Task<List<History_Product>> GetHistoryProducts(Guid adminID);
    }
    public class HistoryService: IHistoryService
    {
        private IHistoryRepository _repository;

        public HistoryService(IHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<History_OtherDebt>> GetHistoryOtherDebt(Guid adminID)
        {
            return await _repository.GetHistoryOtherDebts(adminID);
        }

        public async Task<List<History_Abono>> GetHistoryAbono(Guid adminID)
        {
            return await _repository.GetHistoryAbono(adminID);
        }

        public async Task<List<History_Product>> GetHistoryProducts(Guid adminID)
        {
            return await _repository.GetHistoryProducts(adminID);
        }
    }
}
