using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using System.Web;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InvoicesController : ControllerBase
  {
    // GET: api/<InvoicesController>
    [HttpGet]
    public List<Invoice> Get()
    {
      return new InvoiceHandler().GetInvoices();
    }

    // GET api/<InvoicesController>/5
    [HttpGet("{id}")]
    public Invoice Get(int id)

    {
      return new InvoiceHandler().GetInvoiceById(id);
    }

    [HttpGet("Filter")]
    public List<Invoice> Filter([FromQuery] FilterModel filterModel)
    {
      return new InvoiceHandler().FilterInvoices(filterModel); 
    }

    // POST api/<InvoicesController>
    [HttpPost]
    public void Post([FromBody] Invoice inv)
    {
      new InvoiceHandler().Save(inv);
    }

    // PUT api/<InvoicesController>/5
    [HttpPut]
    public void Put([FromBody] Invoice inv)
    {
      new InvoiceHandler().Update(inv);
    }

    // DELETE api/<InvoicesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
