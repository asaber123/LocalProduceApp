namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//This is the model for the produce. This model has a one to many realtionship to the table producer and also to customer
//The customer/order can only have one customer but the produce can have many orders/customers. 
//The produce can only have one producer but the producer can upload many produce. 
//All properties that has [required] have to be filled in to the dtaabase to store the data. 
//In this model users can also choose to upload a picture file. 
public class Produce
{
    //properties

    public int? ProduceId { get; set; }
    [Required]
    [Display(Name = "Name of produce:")]
    public string? ProduceName { get; set; }

    [Required]
    public int? Price { get; set; }
    
    [Required]
    [Display(Name = "Pickup place and location:")]
    public string? PickupPlace { get; set; }
    
    [Required]
    [Display(Name = "Producers email:")]
    public string? ProducerEmail { get; set; }
    
    [Required]
    [Display(Name = "Theme:")]
    public string? Theme { get; set; }
    [Required]
    [StringLength(60, ErrorMessage = "Maximum 60 letters.")]
    [Display(Name = "Description:")]
    public string? Description { get; set; }
    
    [Display(Name = "Image:")]
    public string? ImgName {get; set;}

    [NotMapped]
    public IFormFile? ImgFile {get; set;}

    [Required]
    [Display(Name = "Producer:")]
    public int? ProducerId { get; set; }
    public Producer? Producer { get; set; }

    public List<Customer>? Customer { get; set; }

}