namespace YourDebtsCore.Base.Models
{
    public class OtherDebtsRequestModel
    {
        public Guid DebtorsID { get; set; }
        public string DebtorName { get; set; }
        public decimal Debt {  get; set; }
        public string NameDebt {  get; set; }
    }

    public class OtherDebtsResponseModel: OtherDebtsRequestModel 
    { 
        public Guid OtherDebtsID { get; set; }
        public DateTimeOffset Audit_CreatedOnDate { get; set; }
    }
}
