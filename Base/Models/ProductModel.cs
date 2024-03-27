namespace YourDebtsCore.Base.Models
{
    public class ProductModel
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal QuantityInStock { get; set; }
        public decimal MoneyInvested { get; set; }
    }
}
