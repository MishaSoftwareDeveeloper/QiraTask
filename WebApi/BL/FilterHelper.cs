using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class FilterHelper
  {
    public List<Invoice> FilterBy(List<Invoice> invoices, string filter, string filterBy)
    {
      filter = Uri.UnescapeDataString(filter);
      switch(filterBy)
      {
        case "CreationDate":
          return invoices.Where(inv => inv.CreationDate.ToString("dd'/'MM'/'yyyy").Contains(filter)).ToList();
        case "Payment":
          return invoices.Where(inv => inv.Payment.ToString() == filter).ToList();
        case "Status":
          return invoices.Where(inv => inv.Status.ToString() == filter).ToList();
        default: return new List<Invoice>();

      }
    }
    public List<Invoice> OrderBy(List<Invoice> invoices, string  sortOrder)
    {
      switch (sortOrder)
      {
        case "CreationDate":
           return invoices.OrderByDescending(inv => inv.CreationDate).ToList();
        case "Payment":
          return invoices.OrderByDescending(inv => inv.Payment).ToList();
        case "Status":
          return invoices.OrderByDescending(inv => inv.Status).ToList();
        default: return new List<Invoice>();
      };
    }
  }
}
