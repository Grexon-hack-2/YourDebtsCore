namespace YourDebtsCore.Base.Models
{
    public class RegisterModel : AuthorizationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
