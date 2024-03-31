namespace YourDebtsCore.Base.Models
{
    public class DetailedList: DebtorModel
    {
        public List<DebtRegisterModel> ListaDeudas { get; set; }
    }

    public class DetailListInfoDB : DebtorModel
    {
        public string ListaDeudas { get; set; }
    }
}
