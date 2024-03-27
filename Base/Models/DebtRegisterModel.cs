namespace YourDebtsCore.Base.Models
{
    public class DebtRegisterModel
    {
        public Guid DebsID { get; set; }
        public Guid DebtorsID { get; set; }
        public Guid ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalAccount { get; set; }
        public DateTimeOffset DateOfPurchase { get; set; }
    }
}
