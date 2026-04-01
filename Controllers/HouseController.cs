using HouseRentingSystem.Data.Data;
using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystem.Data.Data.DataConstants;

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
        var searched = await context.Houses.FirstOrDefaultAsync(h => h.Id == Id);
        return View(searched);
    }
    public IActionResult CreateHouse()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateHouse(HouseFormViewModel model)
    {
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
          
        };

        context.Houses.Add(newHouse);
        await context.SaveChangesAsync();

        return RedirectToAction("AllHouses");
    }
}
}