using CsvHelper.Configuration;
using Models;

namespace DAL
{
  public class InvoiceMap : ClassMap<Invoice>
  {
    public InvoiceMap(){
      Map(p => p.CreationDate).Index(1);
      Map(p => p.Number).Index(2);
      Map(p => p.Payment).Index(3);
      Map(p => p.Amount).Index(4);
      Map(p => p.Status).Index(5);
      Map(p => p.ChangeDate).Index(6);
    }

  }
}
