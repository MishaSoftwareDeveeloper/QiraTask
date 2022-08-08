using Models;
using DAL;
namespace BL
{
  public class InvoiceHandler
  {
    private int pageSize = 4;
    public List<Invoice> GetInvoices()
    {
      try
      {
        return new CSVHelper().GetRecords();
      }
      catch (Exception ex)
      {
        throw new Exception("GetInvoices failed");
      }
    }

    public Invoice GetInvoiceById(int id)
    {
      try
      {
        var invoices = new CSVHelper().GetRecords();
        return invoices.Find(inv => inv.Id == id);
      }
      catch (Exception ex)
      {
        throw new Exception("GetInvoices failed");
      }
    }

    public List<Invoice> FilterInvoices(FilterModel fModel)
    {
      try
      {
        var invoices = new CSVHelper().GetRecords();
        List<Invoice> filtered = new List<Invoice>();
        FilterHelper fHelper = new FilterHelper();

        if (!string.IsNullOrEmpty(fModel.filter) && !string.IsNullOrEmpty(fModel.filterBy))
        {
          filtered = fHelper.FilterBy(invoices, fModel.filter, fModel.filterBy);
        }
        filtered = filtered.Count > 0 ? filtered : invoices;
        if (!string.IsNullOrEmpty(fModel.sortOrder))
        {
          filtered = fHelper.OrderBy(filtered, fModel.sortOrder);
        }
        return filtered.Skip(fModel.paging * pageSize).Take(pageSize).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }


    public void Update(Invoice inv)
    {
      try
      {
        if (InvoiceIsValid(inv))
        {
          CSVHelper csvHelper = new CSVHelper();
          var invoicesRecoeds = csvHelper.GetRecords();
          var invoice = invoicesRecoeds.Where(invs => invs.Id == inv.Id).FirstOrDefault();

          if (invoice != null)
          {
            foreach (var recInv in invoicesRecoeds)
            {
              if (recInv.Id == invoice.Id)
              {
                recInv.ChangeDate = DateTime.Now;
                recInv.Number = inv.Number;
                recInv.Payment = inv.Payment;
                recInv.Status = inv.Status;
              }
            }
            csvHelper.UpdateRecord(invoicesRecoeds);
          }
          else
          {
            throw new InvalidDataException("Update failed");
          } 
        }
        else
        {
          throw new InvalidDataException("Update failed");
        }
      }
      catch (Exception)
      {

        throw new Exception("Update failed");
      }
    }

    public void Save(Invoice inv)
    {
      try
      {
        if (InvoiceIsValid(inv))
        {
          CSVHelper csvHelper = new CSVHelper();
          var invoicesRecoeds = csvHelper.GetRecords();
          inv.Id = invoicesRecoeds.Count + 1;
          invoicesRecoeds.Add(inv);
          csvHelper.SaveRecord(invoicesRecoeds); 
        }
        else
        {
          throw new InvalidDataException("Save failed");
        }
      }
      catch (Exception)
      {
        throw new Exception("Save failed"); ;
      }
    }

    public bool InvoiceIsValid(Invoice inv)
    {
      return inv.Number > 0 && inv.Payment > 0 && inv.Status > 0 && inv.Amount > 0;
    }
  }

}
