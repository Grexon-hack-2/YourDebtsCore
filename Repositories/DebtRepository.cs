using Dapper;
using System.Data;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IDebtRepository
    {
        Task<string> InsertDebt(DebtRegisterModel debtRegister);
        Task<string> InsertPay(PayDebtsModel debtRegister);
        Task<string> InsertOtherDebt(OtherDebtsRequestModel debt, Guid adminUser);
        Task<List<OtherDebtsResponseModel>> GetDataOtherDebts(Guid userAdmin);
        Task<OtherDebtsResponseModel> GetDataOtherDebtById(Guid userAdmin, Guid debtorId);
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

        public async Task<string> InsertPay(PayDebtsModel debtRegister)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                string sp = "Insert_Pay_Debts";

                SqlCommand cmd = new(sp, conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("@AbonoID", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@DebtorsID", debtRegister.DebtorsID);
                cmd.Parameters.AddWithValue("@AmountPaid", debtRegister.AmountPaid);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "El abono a sido ingresado exitosamente";
                }
                return "Ha ocurrido un error en la base de datos";
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

        public async Task<string> InsertOtherDebt(OtherDebtsRequestModel debt, Guid adminUser)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                string sp = "Insert_OtherDebts";

                SqlCommand cmd = new(sp, conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("@UserAdminID", adminUser);
                cmd.Parameters.AddWithValue("@DebtorsID", debt.DebtorsID);
                cmd.Parameters.AddWithValue("@DebtorName", debt.DebtorName);
                cmd.Parameters.AddWithValue("@Debt", debt.Debt);
                cmd.Parameters.AddWithValue("@NameDebt", debt.NameDebt);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "La deuda a sido ingresada exitosamente";
                }
                return "Ha ocurrido un error en la base de datos";
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

        public async Task<List<OtherDebtsResponseModel>> GetDataOtherDebts(Guid userAdmin)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                string text = "select * from OtherDebts where UserAdminID = @userAdmin";

                var result = await conn.QueryAsync<OtherDebtsResponseModel>(text, new {userAdmin});

                return result.ToList();

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

        public async Task<OtherDebtsResponseModel> GetDataOtherDebtById(Guid userAdmin, Guid debtorId)
        {
            try
            {
                using var conn = new SqlConnection(_connStringDB);
                await conn.OpenAsync();

                string text = "select * from OtherDebts where UserAdminID = @userAdmin and DebtorsID = @debtorId";

                var result = await conn.QueryAsync<OtherDebtsResponseModel>(text, new { userAdmin, debtorId });

                return result.ToList().FirstOrDefault();

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
