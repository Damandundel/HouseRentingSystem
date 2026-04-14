using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Models.House.Helpers
{
    public class CategoryViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
    }
}
