

using CsvHelper;
using CsvHelper.Configuration;
using Models;
using System.Globalization;

namespace DAL
{
  public class CSVHelper
  {
    private CsvConfiguration configuration;
    public CSVHelper(){
      configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = false,
        MissingFieldFound = null,
        Delimiter = ";",
      };
    }
    public List<Invoice> GetRecords()
    {

      List<Invoice> records = null;
      try
      {
       
        using (var reader = new StreamReader(@"../DAL/Invoices.csv"))
        using (var csv = new CsvReader(reader, configuration))
        {
          csv.Context.RegisterClassMap<InvoiceMap>();
          int increment = 1;
          records = csv.GetRecords<Invoice>().Select(inv => { inv.Id = increment ++; return inv; }).ToList();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return records;
    }

    public void UpdateRecord(List<Invoice> invoices)
    {
      try
      {
        WriteToCsv(invoices);
      }
      catch (Exception)
      {
        throw new ("Updated failed");
      }
    }

    public void SaveRecord(List<Invoice> invoices)
    {
      try
      {
        WriteToCsv(invoices);
      }
      catch (Exception)
      {
        throw new("Save failed");
      }
    }


    private void WriteToCsv(List<Invoice> invoices)
    {
      try
      {
        using (var writer = new StreamWriter(@"../DAL/Invoices.csv"))
        using (var csv = new CsvWriter(writer, configuration))
        {
          csv.WriteRecords(invoices);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
