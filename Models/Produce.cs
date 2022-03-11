namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Display(Name = "Description:")]
    public string? Description { get; set; }
    
    [Display(Name = "Image:")]
    public string? ImgName {get; set;}

    [NotMapped]
    public IFormFile? ImgFile {get; set;}


    public int? ProducerId { get; set; }
    public Producer? Producer { get; set; }

    public List<Customer>? Customer { get; set; }

}