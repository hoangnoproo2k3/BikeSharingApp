using Microsoft.AspNetCore.Mvc;
using BikeSharingApp.Data;
using BikeSharingApp.Models;
using Microsoft.EntityFrameworkCore;
using BikeSharingApp.Repositories;

namespace BikeSharingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly ApplicationDbContext _context;

        public UserController(IUserRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<IActionResult> ListUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var users = from u in _context.Users
                        select u;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.FullName.Contains(searchTerm) ||
                                         u.Email.Contains(searchTerm) ||
                                         u.Phone.Contains(searchTerm));
            }

            return PartialView("_UserTablePartial", await users.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _context.Roles.ToList();
            var viewModel = new CreateUserModel
            {
                Roles = roles
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Create(User user)
        {
            // if (!ModelState.IsValid)
            // {
            //     foreach (var modelStateKey in ModelState.Keys)
            //     {
            //         var value = ModelState[modelStateKey];
            //         foreach (var error in value.Errors)
            //         {
            //             Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
            //         }
            //     }
            // }

            user.CreatedDate = DateTime.Now;
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListUsers", "User");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.ToListAsync();
            var viewModel = new CreateUserModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                Password = user.Password,
                Roles = roles
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateUserModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.RoleId = model.RoleId;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListUsers));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {

                    return NotFound();
                }
                else
                {
                    model.Roles = await _context.Roles.ToListAsync();
                    return View(model);
                    throw;
                }

            }

        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return NoContent(); // Return NoContent status code
        }
    }
}