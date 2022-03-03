namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

public class Customer
{

    public int? CustomerId { get; set; }

    [Required]
    public string? CustomerName { get; set; }
    [Required]
    public int? PhoneNumber { get; set; }
    [Required]
    public string? UserPickupPlace { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    
    public int? ProduceId { get; set; }
    public Produce? Produce { get; set; }

}