using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Management.Models;
using Task_Management.Data;

namespace Task_Management.Controllers
{
    public class LoginController : Controller
    {
        private readonly Task_ManagementContext _context;

        public LoginController(Task_ManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users model)
        {
            var email = model.Email;
            var password = model.Password;
            var user = from con in _context.Users
                        where con.Email == email && con.Password == password
                        select con;
            if(user != null)
            {
                ViewData["User"] = user.FirstOrDefault().Name;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Users model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Guid.NewGuid();
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
