namespace YourDebtsCore.Base.Models
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public bool Authorized {  get; set; }
        public string Msg { get; set; }
    }
}
