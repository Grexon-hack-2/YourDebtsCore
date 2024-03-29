using System.ComponentModel.DataAnnotations;

namespace YourDebtsCore.Base.Models
{
    public class DebtRegisterModel
    {
        public Guid DebtsID { get; set; }

        [Required(ErrorMessage = "El deudor es requerido")]
        public Guid DebtorsID { get; set; }

        [Required(ErrorMessage = "El producto es requerido")]
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "La cantidad comprada es requerida")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "La cuenta total es requerida")]
        public decimal TotalAccount { get; set; }
        public DateTimeOffset DateOfPurchase { get; set; }
    }
}
