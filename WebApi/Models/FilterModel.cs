using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public class FilterModel
  {
    public int paging { get; set; }
    public string? filter { get; set; }
    public string? filterBy { get; set; }
    public string? sortOrder { get; set; }
  }
}
