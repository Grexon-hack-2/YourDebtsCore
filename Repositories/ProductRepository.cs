using Dapper;
using System.Data;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetProductList(Guid ID);
        Task<string> InsertProduct(ProductModel product, Guid idAdmin);
        Task<string> DeleteProduct(Guid ID);
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

                var sql = "SELECT * FROM Products where IdAdmin=@ID ORDER BY Name ASC";

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

                var spSQL = "InsertProduct";

                var dataInsert = new
                {
                    ProductID = Guid.NewGuid(),
                    product.Name,
                    product.UnitPrice,
                    product.QuantityInStock,
                    product.MoneyInvested,
                    IdAdmin = idAdmin,
                    QuantityPurchased = product.QuantityInStock
                };

                var rowAffected = await conn.QueryAsync<InsertAndUpdateModel>(spSQL, dataInsert, commandType: CommandType.StoredProcedure);

                var resultData = rowAffected.FirstOrDefault();

                if (resultData.Insertado > 0)
                {
                    await InsertHistoryProduct(product, idAdmin, conn);
                    return "El producto fue agregado con exito";
                }
                else if (resultData.Actualizado > 0)
                {
                    return "El producto fua actualizado con exito"; 
                }
                else
                    return "El producto no pudo ser agregado";

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

        public async Task<string> DeleteProduct(Guid ID)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                var sql = "DELETE Products WHERE ProductID = @ID";

                var rowAffected = await conn.ExecuteAsync(sql, new {ID});

                if (rowAffected > 0) return "Producto eliminado con exito";
                else return "El producto no fue encontrado en la base de datos";
            }
            catch(SqlException ex)
            {
                throw new Exception($"No pudo ser eliminado el producto: {ID} | Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"No pudo ser eliminado el producto: {ID} | Error: {ex.Message}");
            }
        }

        private async Task InsertHistoryProduct(ProductModel product, Guid idAdmin, SqlConnection conn)
        {
            try
            {

                var sql = "INSERT INTO History_Products(UserAdminID, Name, UnitPrice, MoneyInvested, QuentityPurchased) Values(@IdAdmin, @Name, @UnitPrice, @MoneyInvested, @QuantityPurchased)";

                var dataInsert = new
                {
                    IdAdmin = idAdmin,
                    product.Name,
                    product.UnitPrice,
                    product.MoneyInvested,
                    QuantityPurchased = product.QuantityInStock
                };

                await conn.ExecuteAsync(sql, dataInsert);


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
