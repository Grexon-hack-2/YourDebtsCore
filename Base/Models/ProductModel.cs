using System.ComponentModel.DataAnnotations;

namespace YourDebtsCore.Base.Models
{
    public class ProductModel
    {
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "La cantidad de producto en almacen es obligatorio")]
        public decimal QuantityInStock { get; set; }

        [Required(ErrorMessage = "El dinero invertido es obligatorio")]
        public decimal MoneyInvested { get; set; }
        public decimal? QuantityPurchased { get; set; }
    }
}
