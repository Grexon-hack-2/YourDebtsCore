namespace YourDebtsCore.Base.Models
{
    public class PayDebtsModel
    {
        public Guid AbonoID { get; set; }
        public Guid DebtorsID { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
