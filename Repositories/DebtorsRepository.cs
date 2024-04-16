using Dapper;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Repositories
{
    public interface IDebtorsRepository
    {
        List<DebtorModel> GetAll(Guid ID);
        string AddNewClient(DebtorModel debtor, Guid userId);
        Task<DetailedList> DataFullRepository(Guid idClient);
        Task<string> DeleteClientRepository(Guid idClient);
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
                string sql = "SELECT (SELECT SUM(AmountPaid) from AbonoDebt where DebtorsID = dt.DebtorsID) as AmountPaid, * FROM Debtors dt where dt.UserAdminID = @ID";
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
                    Audit_CreatedOnDate = DateTimeOffset.Now,
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

        public async Task<DetailedList> DataFullRepository(Guid idClient)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                string sp = "GetDataFull_DebtClient";
                var Data = await connection.QueryAsync<DetailListInfoDB>(sp, new {idClient} , commandType: CommandType.StoredProcedure);
                var converteData = new DetailedList();

                if (Data.Any())
                {
                    var firstData = Data.FirstOrDefault();

                    converteData.DebtorsID = firstData.DebtorsID;
                    converteData.Phone = firstData.Phone;
                    converteData.Audit_CreatedOnDate = firstData.Audit_CreatedOnDate;
                    converteData.Debt = firstData.Debt;
                    converteData.Detail = firstData.Detail;
                    converteData.Name = firstData.Name;
                    converteData.ListaDeudas = string.IsNullOrEmpty(firstData.ListaDeudas) ? null : JsonConvert.DeserializeObject<List<DebtRegisterModel>>(firstData.ListaDeudas);
                    converteData.AmountPaid = firstData.AmountPaid;
                }


                return converteData;
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

        public async Task<string> DeleteClientRepository(Guid idClient)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                string sp = "DeleteClientAndDebts";

                SqlCommand command = new(sp, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@idClient", idClient);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "El deudor a sido eliminado correctamente";
                }
                return "No existe registro del deudor en la base de datos";
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
