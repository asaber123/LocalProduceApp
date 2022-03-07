namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

public class Produce
{
    //properties

    public int? ProduceId { get; set; }
    [Required]
    public string? ProduceName { get; set; }

    [Required]
    public int? Price { get; set; }
    [Required]
    public string? PickupPlace { get; set; }
    [Required]
    public string? ProducerEmail { get; set; }
    [Required]
    public string? Theme { get; set; }
    [Required]
    public string? Description { get; set; }

    public int? ProducerId { get; set; }
    public Producer? Producer { get; set; }

    public List<Customer>? Customer { get; set; }

}