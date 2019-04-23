using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly project_dbContext _context;

        public JobsController(project_dbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        /*public async Task<IActionResult> Index()
        {
            var project_dbContext = _context.Jobs.Include(j => j.Creator).Include(j => j.DriverUsernameNavigation);
            return View(await project_dbContext.ToListAsync());
        }*/

        //Sort
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["PackageSizeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "package_size" : "";
            ViewData["CurrentFilter"] = searchString;

            var jobs = from s in _context.Jobs select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(s => s.PickUpArea.Contains(searchString) || s.DeliveryArea.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "package_size":
                    jobs = jobs.OrderByDescending(s => s.PackageSizeInWeight);
                    break;
                default:
                    jobs = jobs.OrderBy(s => s.PackageSizeInWeight);
                    break;
            }
            return View(await jobs.AsNoTracking().ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .Include(j => j.Creator)
                .Include(j => j.DriverUsernameNavigation)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.JobCreators, "CreatorId", "CreatorId");
            ViewData["DriverUsername"] = new SelectList(_context.Drivers, "Username", "Username");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,CreatorId,PackageSizeInWeight,PackageSizeInDimensions,PickUpArea,DeliveryArea,AdditionalInfo,JobConfirmed,DriverUsername,Price,DeliveryDate")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.JobCreators, "CreatorId", "CreatorId", jobs.CreatorId);
            ViewData["DriverUsername"] = new SelectList(_context.Drivers, "Username", "Username", jobs.DriverUsername);
            return View(jobs);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs.FindAsync(id);
            if (jobs == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.JobCreators, "CreatorId", "CreatorId", jobs.CreatorId);
            ViewData["DriverUsername"] = new SelectList(_context.Drivers, "Username", "Username", jobs.DriverUsername);
            return View(jobs);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,CreatorId,PackageSizeInWeight,PackageSizeInDimensions,PickUpArea,DeliveryArea,AdditionalInfo,JobConfirmed,DriverUsername,Price,DeliveryDate")] Jobs jobs)
        {
            if (id != jobs.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobsExists(jobs.JobId))
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
            ViewData["CreatorId"] = new SelectList(_context.JobCreators, "CreatorId", "CreatorId", jobs.CreatorId);
            ViewData["DriverUsername"] = new SelectList(_context.Drivers, "Username", "Username", jobs.DriverUsername);
            return View(jobs);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .Include(j => j.Creator)
                .Include(j => j.DriverUsernameNavigation)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobs = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(jobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobsExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
