using System.Security.Cryptography;
using System.Text;

namespace YourDebtsCore.Base.Utils
{
    public class Encript
    {
        public static string GetSha256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            byte[] stream;
            StringBuilder sb = new();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }
    }
}
