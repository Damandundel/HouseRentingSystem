
using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : Controller
    {
        private List<HouseViewModel> houses = new List<HouseViewModel>()
        {
            new HouseViewModel()
            {
                Id = 1,
                Name = "Beach House",
                Address = "Miami, Florida",
                ImageUrl = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQvcje9el9YUxSqN4VSt3llpb6su9ghN-_ZbA&s"

            },

            new HouseViewModel()
            {
                Id = 2,
                Name = "Mountain House",
                Address = "Rila Mountain, Bulgaria",
                ImageUrl = @"https://bghike.com/en/images/huts_pic/rila_lakes_main.jpg"

            },

            new HouseViewModel()
            {
                Id = 3,
                Name = "Urban House",
                Address = "Luylin, Sofia",
                ImageUrl = @"https://cdnp.ues.bg/projects/watermark_thumbs_768/257/180582.jpg"

            }
        };
        public IActionResult AllHouses()
        {
            return View(houses);
        }
        public IActionResult Details(int id)
        {
            return View(houses.FirstOrDefault(h => h.Id == id));
        }
    }
}