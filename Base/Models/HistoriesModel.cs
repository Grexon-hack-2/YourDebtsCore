namespace YourDebtsCore.Base.Models
{
    public class History_OtherDebt
    {
        public Guid H_OtherDebtID { get; set; }
        public Guid UserAdminID { get; set; }
        public string DebtorName { get; set; }
        public decimal Debt { get; set; }
        public string NameDebt {  get; set; }
        public DateTimeOffset Audit_CreatedOnDate { get; set; }
        public Guid DebtorsID { get; set; }
        public string Status { get; set; }

    }

    public class History_Abono
    {
        public Guid H_AbonoID { get; set; }
        public Guid UserAdminID { get; set;}
        public Guid DebtorsID { get; set; }
        public decimal AmountPaid { get; set; }
        public string NameDebtor { get; set; }
        public DateTimeOffset Audit_CreatedOnDate { get; set; }
    }

    public class History_Product
    {
        public Guid H_ProductID { get; set; }
        public Guid UserAdminID { get;set; }
        public string Name {  get; set; }
        public decimal UnitPrice { get; set; }
        public decimal MoneyInvested { get; set; }
        public DateTimeOffset Audit_CreatedOnDate { get; set; }
    }
}
