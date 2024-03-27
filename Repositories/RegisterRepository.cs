using Dapper;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Utils;

namespace YourDebtsCore.Repositories
{
    public interface IRegisterRepository
    {
        string Register_User(RegisterModel register);
    }
    public class RegisterRepository: IRegisterRepository
    {
        private readonly string _connString;
        public RegisterRepository(string connString) 
        { 
            _connString = connString;
        }

        public string Register_User(RegisterModel register)
        {
            using var conn = new SqlConnection(this._connString);
            conn.Open();
            var UserAdminID = Guid.NewGuid();
            var NameUser = register.User;
            var Password = Encript.GetSha256(register.Password);
            var Email = register.Email;
            var sql = "INSERT INTO UserAdmin VALUES (@UserAdminID, @NameUser, @Password, @Email)";
            var rowAffect = conn.Execute(sql, new { UserAdminID, NameUser, Password, Email });

            if (rowAffect == 0) throw new Exception("Error DB: Registro no ingresado");
            else return "Usuario ingresado satisfactoriamente";
        }
    }
}
