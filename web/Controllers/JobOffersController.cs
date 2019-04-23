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
    public class JobOffersController : Controller
    {
        private readonly project_dbContext _context;

        public JobOffersController(project_dbContext context)
        {
            _context = context;
        }

        // GET: JobOffers
        public async Task<IActionResult> Index(string id)
        {
            ViewData["CurrentFilter"] = id;

            var jobOffers = from j in _context.JobOffers select j;

            if (!String.IsNullOrEmpty(id))
            {
                jobOffers = jobOffers.Where(j => j.JobId == System.Convert.ToInt32(id));
            }

            //var project_dbContext = _context.JobOffers.Include(j => j.DriverUsernameNavigation).Include(j => j.Job);
            return View(await jobOffers.AsNoTracking().ToListAsync());
        }

        // GET: JobOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers
                .Include(j => j.DriverUsernameNavigation)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.JobOfferId == id);
            if (jobOffers == null)
            {
                return NotFound();
            }

            return View(jobOffers);
        }

        // GET: JobOffers/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId");
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId");
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,DriverId,ProposedDeliveryPrice,ProposedPickupDate,ApproxDeliveryDate,JobOfferConfirmed")] JobOffers jobOffers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobOffers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", jobOffers.DriverId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", jobOffers.JobId);
            return View(jobOffers);
        }

        // GET: JobOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers.FindAsync(id);
            if (jobOffers == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", jobOffers.DriverId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", jobOffers.JobId);
            return View(jobOffers);
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,DriverId,ProposedDeliveryPrice,ProposedPickupDate,ApproxDeliveryDate,JobOfferConfirmed")] JobOffers jobOffers)
        {
            if (id != jobOffers.JobOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOffers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOffersExists(jobOffers.JobOfferId))
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
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", jobOffers.DriverId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", jobOffers.JobId);
            return View(jobOffers);
        }

        // GET: JobOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers
                .Include(j => j.DriverUsernameNavigation)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.JobOfferId == id);
            if (jobOffers == null)
            {
                return NotFound();
            }

            return View(jobOffers);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOffers = await _context.JobOffers.FindAsync(id);
            _context.JobOffers.Remove(jobOffers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOffersExists(int id)
        {
            return _context.JobOffers.Any(e => e.JobOfferId == id);
        }
    }
}
