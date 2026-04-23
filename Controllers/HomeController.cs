using HouseRentingSystem.Data.Data;
using HouseRentingSystem.Models.House;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Controllers;

public class HomeController : Controller
{
    private readonly HouseRentingSystemDbContext context;

    public HomeController(HouseRentingSystemDbContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        var houses = await context.Houses
            .AsNoTracking()
            .Where(h => !h.IsDeleted)
            .OrderByDescending(h => h.Id)
            .ToListAsync();

        var renterIdProperty = houses.FirstOrDefault()?.GetType().GetProperty("RenterId");

        var model = new HouseFormViewModel
        {
            TotalHouses = houses.Count,
            TotalRents = renterIdProperty == null
                ? 0
                : houses.Count(h => renterIdProperty.GetValue(h) != null),
            Houses = houses
                .Take(3)
                .Select(h => new HouseFormViewModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl,
                    Address = h.Address
                })
                .ToList()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        var currentStatusCode = statusCode ?? 500;
        Response.StatusCode = currentStatusCode;

        if (currentStatusCode == 400 || currentStatusCode == 404)
        {
            ViewData["StatusCode"] = currentStatusCode;
            ViewData["Headline"] = currentStatusCode == 404
                ? "We couldn't find that page"
                : "That request could not be processed";
            ViewData["Message"] = currentStatusCode == 404
                ? "The page may have been moved, renamed or never existed in the first place."
                : "Please check the request and try again from a valid page in the system.";

            return View("Error400");
        }

        if (currentStatusCode == 401 || currentStatusCode == 403)
        {
            return View("Error401");
        }

        return View();
    }
}
