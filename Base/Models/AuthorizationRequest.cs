using System.ComponentModel.DataAnnotations;

namespace YourDebtsCore.Base.Models
{
    public class AuthorizationRequest
    {
        [Required(ErrorMessage = "El campo 'User' es obligatorio.")]
        public string User {  get; set; }

        [Required(ErrorMessage = "El campo 'Password' es obligatorio.")]
        public string Password { get; set; }
    }
}
