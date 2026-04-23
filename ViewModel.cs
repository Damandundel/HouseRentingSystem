namespace HouseRentingSystem.Models.Home;

public class ViewModel
{
    public int TotalHouses { get; set; }
    public int TotalRents { get; set; }
    public List<HouseIndexViewModel> Houses { get; set; } = new();
}