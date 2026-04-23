public class HouseFormViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal PricePerMonth { get; set; }
    public List<CategoryViewModel>? Categories { get; set; }
    public int SelectedCategoryId { get; set; }

    // Add the following properties to fix CS0117
    public int TotalHouses { get; set; }
    public int TotalRents { get; set; }
    public List<HouseFormViewModel> Houses { get; set; } = new();
}