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
    public class JobCreatorsController : Controller
    {
        private readonly project_dbContext _context;

        public JobCreatorsController(project_dbContext context)
        {
            _context = context;
        }

        // GET: JobCreators
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var jobCreators = from j in _context.JobCreators select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                jobCreators = jobCreators.Where(s => s.Username.Contains(searchString));
            }

            return View(await jobCreators.AsNoTracking().ToListAsync());
        }

        // GET: JobCreators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCreators = await _context.JobCreators
                .FirstOrDefaultAsync(m => m.CreatorId == id);
            if (jobCreators == null)
            {
                return NotFound();
            }

            return View(jobCreators);
        }

        // GET: JobCreators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobCreators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,Address,PhoneNo")] JobCreators jobCreators)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobCreators);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobCreators);
        }

        // GET: JobCreators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCreators = await _context.JobCreators.FindAsync(id);
            if (jobCreators == null)
            {
                return NotFound();
            }
            return View(jobCreators);
        }

        // POST: JobCreators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Username,Email,Password,Address,PhoneNo")] JobCreators jobCreators)
        {
            if (id != jobCreators.CreatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCreators);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCreatorsExists(jobCreators.CreatorId))
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
            return View(jobCreators);
        }

        // GET: JobCreators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCreators = await _context.JobCreators
                .FirstOrDefaultAsync(m => m.CreatorId == id);
            if (jobCreators == null)
            {
                return NotFound();
            }

            return View(jobCreators);
        }

        // POST: JobCreators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCreators = await _context.JobCreators.FindAsync(id);
            _context.JobCreators.Remove(jobCreators);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCreatorsExists(int id)
        {
            return _context.JobCreators.Any(e => e.CreatorId == id);
        }
    }
}
