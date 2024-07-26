using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;
using BikeSharingApp.Repositories;
using BikeSharingApp.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeSharingApp.Controllers
{
    public class BikesController : Controller
    {
        private readonly IBikesRepository _repository;
        private readonly ILogger<BikesController> _logger;

        public BikesController(IBikesRepository repository, ILogger<BikesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> ListBikes()
        {
            var bikes = await _repository.GetAllBikesAsync();
            return View(bikes);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bike = await _repository.GetBikeByIdAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new CreateBikeViewModel
            {
                Locations = (await _repository.GetAllLocationsAsync()).Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBikeViewModel model)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating bike with the following details: {@BikeModel}", model);
                    var bike = new Bike
                    {
                        BikeName = model.BikeName,
                        Address = model.Address,
                        Price = model.Price,
                        OwnerPhone = model.OwnerPhone,
                        BikeStatus = model.BikeStatus,
                        Description = model.Description,
                        Img = await SaveFileAsync(model.Img),
                        LocationId = model.LocationId,
                        PricePerHour = model.PricePerHour,
                        OwnerId = user.Id
                    };

                    await _repository.AddBikeAsync(bike);

                    TempData["SuccessMessage"] = "Bike created successfully.";
                    return RedirectToAction("ListBikes");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"===============Failed to create bike: {ex}");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the bike. Please try again.");
                }
            }

            // Reload locations for view in case of validation errors
            model.Locations = (await _repository.GetAllLocationsAsync()).Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            return View(model);
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/images/" + fileName; // Return relative path for storing in the database
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bike = await _repository.GetBikeByIdAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            var model = new CreateBikeViewModel
            {
                Id = bike.Id,
                BikeName = bike.BikeName,
                Address = bike.Address,
                Price = bike.Price,
                OwnerPhone = bike.OwnerPhone,
                BikeStatus = bike.BikeStatus,
                Description = bike.Description,
                LocationId = bike.LocationId,
                PricePerHour = bike.PricePerHour,
                ExistingImg = bike.Img,
                Locations = (await _repository.GetAllLocationsAsync()).Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList()
            };

            return View("Create", model);  // Sử dụng lại view Create cho chức năng Edit
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateBikeViewModel model)
        {
            var user = HttpContext.Session.Get<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bike = await _repository.GetBikeByIdAsync(model.Id);
                    if (bike == null)
                    {
                        return NotFound();
                    }

                    bike.BikeName = model.BikeName;
                    bike.Address = model.Address;
                    bike.Price = model.Price;
                    bike.OwnerPhone = model.OwnerPhone;
                    bike.BikeStatus = model.BikeStatus;
                    bike.Description = model.Description;
                    bike.LocationId = model.LocationId;
                    bike.PricePerHour = model.PricePerHour;

                    if (model.Img != null)
                    {
                        bike.Img = await SaveFileAsync(model.Img);
                    }

                    await _repository.UpdateBikeAsync(bike);

                    TempData["SuccessMessage"] = "Bike updated successfully.";
                    return RedirectToAction("ListBikes");
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment the line below once you have a logger)
                    // _logger.LogError($"Failed to update bike: {ex}");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the bike. Please try again.");
                }
            }

            // Reload locations for view in case of validation errors
            model.Locations = (await _repository.GetAllLocationsAsync()).Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            return View("Create", model);  // Sử dụng lại view Create cho chức năng Edit
        }

    }
}