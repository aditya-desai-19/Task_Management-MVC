using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Management.Data;
using Task_Management.Models;
using Task_Management.ViewModels;

namespace Task_Management.Controllers
{
    public class TasksStatusController : Controller
    {
        private readonly Task_ManagementContext _context;

        public TasksStatusController(Task_ManagementContext context)
        {
            _context = context;
        }

        // GET: TasksStatus
        public async Task<IActionResult> Index()
        {
            List<TaskViewModel> taskList = new List<TaskViewModel>();
            var query = from u in _context.Users
                        join t in _context.Tasks on u.UserId equals t.UserId
                        select new TaskViewModel
                        {
                            Name = t.Name,
                            Description = t.Description,
                            Status = t.Status,
                            StudentName = u.Name
                        };

            taskList = await query.ToListAsync();
            return View(taskList);
        }

        // GET: TasksStatus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: TasksStatus/Create
        public async Task<IActionResult> Create()
        {
            var studentList = await _context.Users.Select
                                (u => new StudentList
                                    { 
                                        StudentId = u.UserId,
                                        StudentName = u.Name 
                                    }
                                ).ToListAsync();
            var viewModel = new TaskViewModel
            {
                StudentList = studentList
            };
            return View(viewModel);
        }

        // POST: TasksStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskViewModel model)
        {
            try
            {
                var taskId = Guid.NewGuid();
                var name = model.Name;
                var desc = model.Description;
                var status = model.Status;
                var userId = model.UserId;
                _context.Add(new Tasks
                {
                    TaskId = taskId,
                    Name = name,
                    Description = desc,
                    Status = status,
                    UserId = userId
                });
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception();
            }
            
            return RedirectToAction("Index", "Home");
        }

        // GET: TasksStatus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            return View(tasks);
        }

        // POST: TasksStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TaskId,Name,Description,Status")] Tasks tasks)
        {
            if (id != tasks.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tasks);
        }

        // GET: TasksStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: TasksStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'Task_ManagementContext.Tasks'  is null.");
            }
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(Guid id)
        {
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
