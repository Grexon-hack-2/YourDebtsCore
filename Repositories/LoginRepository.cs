using Dapper;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Utils;

namespace YourDebtsCore.Repositories
{
    public interface ILoginRepository
    {
        DataUserModel Login(AuthorizationRequest request);
    }
    public class LoginRepository: ILoginRepository
    {
        private readonly string _connectionString;
        public LoginRepository(string configuration) 
        {
            _connectionString = configuration;
        }

        public DataUserModel Login(AuthorizationRequest request)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var userName = request.User;
            var pass = Encript.GetSha256(request.Password);
            string sql = "SELECT top 1 UserAdminID, NameUser, Email, PersonName FROM UserAdmin where NameUser=@UserName and Password=@Password";
            var user = connection.QueryFirstOrDefault<DataUserModel>(sql, new { userName , Password = pass});

            if(user == null) throw new Exception("Usuario o contraseña equivocada!!");

            return user;
        }
    }
}
