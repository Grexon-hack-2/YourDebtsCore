using System.Data;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IDebtRepository
    {
        Task<string> InsertDebt(DebtRegisterModel debtRegister);
    }
    public class DebtRepository: IDebtRepository
    {
        private readonly string _connStringDB;

        public DebtRepository(string connStringDB)
        {
            _connStringDB = connStringDB;
        }

        public async Task<string> InsertDebt(DebtRegisterModel debtRegister)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                string sp = "Insert_Debt";

                SqlCommand cmd = new(sp, conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("@DebsID", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@DebtorsID", debtRegister.DebtorsID);
                cmd.Parameters.AddWithValue("@ProductID", debtRegister.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", debtRegister.Quantity);
                cmd.Parameters.AddWithValue("@TotalAccount", debtRegister.TotalAccount);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "Deuda agregada exitosamente";
                }
                return "La deuda no pudo ser agregada en la base de datos";
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
