using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem { 
public class HouseFormViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 100 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, MinimumLength = 30, ErrorMessage = "Address must be between 30 and 200 characters")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 50, ErrorMessage = "Description must be between 50 and 1000 characters")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Image URL is required")]
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string ImageUrl { get; set; } = null!;

    [Required(ErrorMessage = "Price per month is required")]
    [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100,000")]
    [DataType(DataType.Currency)]
    public decimal PricePerMonth { get; set; }

 }
}