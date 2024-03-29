using Dapper;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetProductList(Guid ID);
        Task<string> InsertProduct(ProductModel product, Guid idAdmin);
    }
    public class ProductRepository: IProductRepository
    {
        private readonly string _connStringDB;
        public ProductRepository(string connStringDB)
        {
            _connStringDB = connStringDB;
        }

        public async Task<List<ProductModel>> GetProductList(Guid ID)
        {
            try
            {
                using var connection = new SqlConnection(_connStringDB);
                await connection.OpenAsync();

                var sql = "SELECT * FROM Products where IdAdmin=@ID";

                var data = connection.Query<ProductModel>(sql,new { ID });

                return data.ToList();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> InsertProduct(ProductModel product, Guid idAdmin)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                var sql = "INSERT INTO Products Values(@ProductID, @Name, @UnitPrice, @QuantityInStock, @MoneyInvested, @IdAdmin)";

                var dataInsert = new
                {
                    ProductID = Guid.NewGuid(),
                    product.Name,
                    product.UnitPrice,
                    product.QuantityInStock,
                    product.MoneyInvested,
                    IdAdmin = idAdmin
                };

                var rowAffected = await conn.ExecuteAsync(sql, dataInsert);

                if (rowAffected > 0) return "El producto fue agregado con exito";
                else return "El producto no pudo ser agregado";

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
