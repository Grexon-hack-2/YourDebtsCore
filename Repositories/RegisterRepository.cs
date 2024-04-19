using Dapper;
using Microsoft.Win32;
using System.Data.SqlClient;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace YourDebtsCore.Repositories
{
    public interface IRegisterRepository
    {
        string Register_User(RegisterModel register);
        Task<string> UpdateRegister(RegisterModel updateData, Guid adminID);
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
            try
            {
                using var conn = new SqlConnection(this._connString);
                conn.Open();
                var UserAdminID = Guid.NewGuid();
                var NameUser = register.User;
                var Password = Encript.GetSha256(register.Password);
                var Email = register.Email;
                var PersonName = register.Name;
                var Image = register.Image;

                if(register.Image != null)
                {
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(register.Image);

                    Image = Convert.ToBase64String(bytes);
                }


                var sql = "INSERT INTO UserAdmin VALUES (@UserAdminID, @NameUser, @Password, @Email, @PersonName, @Image)";
                var rowAffect = conn.Execute(sql, new { UserAdminID, NameUser, Password, Email, PersonName, Image });

                if (rowAffect == 0) throw new Exception("Error DB: Registro no ingresado");
                else return "Usuario ingresado satisfactoriamente";
            }
            catch(SqlException ex)
            {
                throw new Exception($"Error DB:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DB:{ex.Message}");
            }

        }

        public async Task<string> UpdateRegister(RegisterModel updateData, Guid adminID)
        {
            try
            {
                using var conn = new SqlConnection(_connString);
                await conn.OpenAsync();

                var NameUser = updateData.Name;
                var Email = updateData.Email;

                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(updateData.Image);

                var Image = Convert.ToBase64String(bytes);

                var text = "update UserAdmin set PersonName = @NameUser, Email = @Email, Image = @Image where UserAdminID = @adminID";

                var rowAffect = await conn.ExecuteAsync(text, new { NameUser, Email, Image, adminID });

                if (rowAffect == 0) throw new Exception("Error DB: Registro no actualizado");
                else return "Sus datos han sido actualizados satisfactoriamente";

            }
            catch (SqlException ex)
            {
                throw new Exception($"Error DB:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DB:{ex.Message}");
            }
        }
    }
}
