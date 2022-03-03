namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

public class Producer
{

    public int? ProducerId { get; set; }

    [Required]
    public string? ProducerName { get; set; }
    [Required]
    public int? ProducerNumber { get; set; }
    [Required]
    public int? ProducerEmail { get; set; }

 
   public List<Produce>? Produce { get; set; }

}