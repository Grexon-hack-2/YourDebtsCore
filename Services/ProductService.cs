using YourDebtsCore.Base.Models;
using YourDebtsCore.Repositories;

namespace YourDebtsCore.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsList(Guid id);
        Task<string> InsertProduct(ProductModel product, Guid idAdmin);
    }
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductModel>> GetProductsList(Guid id)
        {
            return await _repository.GetProductList(id);
        }

        public async Task<string> InsertProduct(ProductModel product, Guid idAdmin)
        {
            return await _repository.InsertProduct(product, idAdmin);
        }
    }
}
