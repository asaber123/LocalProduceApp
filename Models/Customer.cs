namespace LocalProduceApp.Models;
using System.ComponentModel.DataAnnotations;

//This is the model for the customers that makes an order. This model has a one to many realtionship to the table produce
//The customer/order can only have one customer but the produce can have many orders/customers. 
//All properties that has [required] have to be filled in to the dtaabase to store the data. 
public class Customer
{

    public int? CustomerId { get; set; }

    [Required]
    [Display(Name = "Full name:")]
   // [StringLength(60, ErrorMessage = "Your name must be in the range of 5 and 60 letters.", MinimumLength = 5)]

    public string? CustomerName { get; set; }
    [Required]
    [Display(Name = "Phone number:")]
    //[Phone]
    public int? PhoneNumber { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    [Display(Name = "Producer:")]
    public int? ProduceId { get; set; }
    public Produce? Produce { get; set; }

}