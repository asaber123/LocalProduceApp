namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

public class Producer
{

    public int? ProducerId { get; set; }

    [Required]
    [Display(Name = "Full name:")]
    public string? ProducerName { get; set; }
    [Required]
    [Display(Name = "Phone number:")]
    public int? ProducerNumber { get; set; }
    [Required]
    [Display(Name = "Producers email:")]
    public string? ProducerEmail { get; set; }

 
   public List<Produce>? Produce { get; set; }

}