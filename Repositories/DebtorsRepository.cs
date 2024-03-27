using Dapper;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IDebtorsRepository
    {
        List<DebtorModel> GetAll(Guid ID);
        string AddNewClient(DebtorModel debtor, Guid userId);
    }
    public class DebtorsRepository: IDebtorsRepository
    {
        private string _connectionString;
        public DebtorsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DebtorModel> GetAll(Guid ID)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                string sql = "SELECT * FROM Debtors where UserAdminID = @ID";
                return connection.Query<DebtorModel>(sql, new { ID }).ToList();
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

        public string AddNewClient(DebtorModel debtor, Guid userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                string sql = "INSERT INTO Debtors VALUES (@DebtorsID, @Name, @Phone, @Debt, @Audit_CreatedOnDate, @Detail, @UserAdminID)";
                var data = new
                {
                    DebtorsID = Guid.NewGuid(),
                    Name = debtor.Name,
                    Phone = debtor.Phone,
                    Debt = debtor.Debt,
                    Audit_CreatedOnDate = debtor.Audit_CreatedOnDate,
                    Detail = debtor.Detail,
                    UserAdminID = userId
                };
                

                var rowAffect = connection.Execute(sql, data);

                if (rowAffect == 0) return "Dato no insertado, intentelo de nuevo";
                else return "Deudor ingresado satisfactoriamente";
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
