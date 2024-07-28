using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Models;
using BikeSharingApp.Data;

namespace BikeSharingApp.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        var bikes = _context.Bikes.ToList();
        var locations = _context.Locations.ToList();

        var viewModel = new MultipleListsViewModel
        {
            Users = users,
            Bikes = bikes,
            Locations = locations
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
