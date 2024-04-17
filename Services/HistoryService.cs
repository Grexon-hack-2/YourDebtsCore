using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IHistoryService
    {
        Task<List<History_OtherDebt>> GetHistoryOtherDebt(Guid adminID);
        Task<Dictionary<string, List<History_Abono>>> GetHistoryAbono(Guid adminID);
        Task<Dictionary<string, List<History_Product>>> GetHistoryProducts(Guid adminID);
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

        public async Task<Dictionary<string, List<History_Abono>>> GetHistoryAbono(Guid adminID)
        {
            var response = await _repository.GetHistoryAbono(adminID);

            var orderByDate = response.GroupBy(item => item.Audit_CreatedOnDate.Month);

            var result = new Dictionary<string, List<History_Abono>>();

            foreach (var group in orderByDate)
            {
                var monthName = new DateTime(2024, group.Key, 1).ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
                result[monthName.ToLower()] = group.Select(item => item).ToList();
            }

            return result;

        }

        public async Task<Dictionary<string, List<History_Product>>> GetHistoryProducts(Guid adminID)
        {
            var response = await _repository.GetHistoryProducts(adminID);

            var orderByDate = response.GroupBy(item => item.Audit_CreatedOnDate.Month);

            var result = new Dictionary<string, List<History_Product>>();

            foreach ( var group in orderByDate)
            {
                var monthName = new DateTime(2024, group.Key, 1).ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
                result[monthName.ToLower()] = group.Select(item => item).ToList();
            }

            return result;
        }
    }
}
