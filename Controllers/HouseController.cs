using System.Security.Claims;
using HouseRentingSystem.Data.Data;
using HouseRentingSystem.Data.Data.Entities;
using HouseRentingSystem.Models;
using HouseRentingSystem.Models.House;
using HouseRentingSystem.Models.House.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class HouseController : Controller
{
    public class HouseController : Controller
    {
        private readonly HouseRentingSystemDbContext context;

        public HouseController(HouseRentingSystemDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> AllHouses()
        {
            var housesViewModel = await context.Houses
            .AsNoTracking()
            .Select(h => new HouseViewModel
            {
                Id = h.Id,
                Name = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl
            })
            .ToListAsync();
            return View(housesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var searched = await context.Houses
                .Include(h => h.Agent)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Id);
            SearchMethod(searched);
            var model = new HouseDetailViewModel()
            {
                Id = searched.Id,
                Address = searched.Address,
                ImageUrl = searched.ImageUrl,
                Description = searched.Description,
                CreatedBy = searched.Agent.UserName,
                Price = searched.PricePerMonth,
                Name = searched.Title
            };

            return View(model);
        }

        private static void SearchMethod(House? searched)
        {
            search();
        }

        private static void search()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateHouse()
        {
            List<CategoryViewModel> houseCategories = await context.Categories
            .AsNoTracking()
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
            return View(houseCategories);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouse(HouseFormViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool addressExists = await context.Houses
                .AnyAsync(h => h.Address.ToLower() == model.Address.ToLower());

            if (addressExists)
            {
                ModelState.AddModelError("Address", "This address is already registered");
                return View(model);
            }

            var newHouse = new House
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.SelectedCategoryId,
                //AgentId = 0
            };

            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();

            return RedirectToAction("AllHouses");
        }
    }
}