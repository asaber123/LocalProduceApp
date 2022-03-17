namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;
//This is the model for the producer. This model has a one to many realtionship to the table produce
//The produce can only have one producer but the producer can upload many produce. 
//All properties that has [required] have to be filled in to the dtaabase to store the data. 
public class Producer
{

    public int? ProducerId { get; set; }

    [Required]
    [Display(Name = "Full name:")]
    public string? ProducerName { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string ProducerNumber { get; set; }
    [Required]
    [Display(Name = "Producers email:")]
    public string? ProducerEmail { get; set; }

 
   public List<Produce>? Produce { get; set; }

}