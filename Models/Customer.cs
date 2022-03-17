namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

public class Customer
{

    public int? CustomerId { get; set; }

    [Required]
    [Display(Name = "Full name:")]
    [StringLength(60)]
    public string? CustomerName { get; set; }
    [Required]
    [Display(Name = "Phone number:")]
    public int? PhoneNumber { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    
    public int? ProduceId { get; set; }
    public Produce? Produce { get; set; }

}