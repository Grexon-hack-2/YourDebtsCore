using System.ComponentModel.DataAnnotations;

namespace YourDebtsCore.Base.Models
{
    public class DebtorModel
    {
        public Guid DebtorsID { get; set; }

        [Required(ErrorMessage ="El campo 'Name' es obligatorio")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Detail { get; set; }

        [Required(ErrorMessage = "El campo 'Audit_CreatedOnDate' es obligatorio")]
        public DateTimeOffset Audit_CreatedOnDate { get; set; }

        [Required(ErrorMessage = "El campo 'Debt' es obligatorio")]
        public decimal Debt { get; set; }
    }
}
