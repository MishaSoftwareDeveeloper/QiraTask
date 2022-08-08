

namespace Models
{
  public class Invoice
  {
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public int Number { get; set; }
    public int Payment { get; set; }
    public double Amount { get; set; }
    public int Status { get; set; }
    public DateTime? ChangeDate { get; set; } = null;

  }
}
