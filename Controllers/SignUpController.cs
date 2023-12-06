using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Management.Models;
using Task_Management.Data;

namespace Task_Management.Controllers
{
    public class SignUpController : Controller
    {
        private readonly Task_ManagementContext _context;

        public SignUpController(Task_ManagementContext context)
        {
            _context = context;
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
                return RedirectToAction("Index", "TasksStatus");
            }
            return View(model);
        }
    }
}
