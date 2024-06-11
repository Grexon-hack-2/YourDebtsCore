using Dapper;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IHistoryRepository
    {
        Task<List<History_OtherDebt>> GetHistoryOtherDebts(Guid adminID);
        Task<List<History_Abono>> GetHistoryAbono(Guid adminID);
        Task<List<History_Product>> GetHistoryProducts(Guid adminID);
    }
    public class HistoryRepository: IHistoryRepository
    {
        private string _connectionString;
        public HistoryRepository(string conn) 
        { 
            _connectionString = conn;
        }

        public async Task<List<History_OtherDebt>> GetHistoryOtherDebts(Guid adminID)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.OpenAsync().Wait();

                var text = "SELECT * FROM History_OtherDebts where UserAdminID = @adminID ORDER BY DebtorName ASC";

                var response = await conn.QueryAsync<History_OtherDebt>(text, new {adminID});

                return response.ToList();

            }
            catch(SqlException ex) 
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<History_Abono>> GetHistoryAbono(Guid adminID)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.OpenAsync().Wait();

                var text = "SELECT * FROM History_AbonoDebt where UserAdminID = @adminID ORDER BY NameDebtor ASC";

                var response = await conn.QueryAsync<History_Abono>(text, new { adminID });

                return response.ToList();

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

        public async Task<List<History_Product>> GetHistoryProducts(Guid adminID)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.OpenAsync().Wait();

                var text = "SELECT * FROM History_Products where UserAdminID = @adminID ORDER BY Name ASC";

                var response = await conn.QueryAsync<History_Product>(text, new { adminID });

                return response.ToList();

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
