using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectCountry.Data;
using ProjectCountry.Models;

namespace ProjectCountry.Controllers
{
    public class StatesController : Controller
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        // GET: States
        public async Task<IActionResult> Index(int? countryId,int? personId)
        {
            var worldModel = new WorldModel();
            worldModel.States = await _context.States.Include(s => s.Country).Include(s=>s.People).AsNoTracking().ToListAsync();
            //1return View(await dataContext.ToListAsync());

            //2var viewModel = new CourseIndexViewModel();
            //viewModel.Courses = await _context.Courses
            //    .Include(i => i.Enrollments)
            //       .ThenInclude(i => i.Student)
            //      .AsNoTracking()
            //      .ToListAsync();
            if (personId.HasValue)
            {
                var persons = await _context.People.Where(e => e.State.StateId == personId).ToListAsync();
                worldModel.People = persons;
            }
            if (countryId.HasValue)
            {
                ViewData["CountryId"] = countryId.Value;
                //worldModel.States = worldModel.States.Where(
                //    x => x.CountryId == countryId).ToList();
                var states = await _context.Countries.Where(e => e.States.Any(e=>e.StateId == countryId)).ToListAsync();
                worldModel.Countries = states;
            }
            return View(worldModel);
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public IActionResult Create(int countryId)
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateName,CountryId,PlateCode,CountryName")] State state)
        {
            if (ModelState.IsValid)
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateName,CountryId,PlateCode,CountryName")] State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _context.States.FindAsync(id);
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
