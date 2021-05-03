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
    public class PeopleController : Controller
    {
        private readonly DataContext _context;

        public PeopleController(DataContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(int? countryId, int? stateId)
        {
            var worldModel = new WorldModel();
            worldModel.People = await _context.People
                .Include(p => p.State)
                .ThenInclude(p => p.Country).ToListAsync();

            return View(worldModel);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.State)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            //var countryName = _context.Countries.Where(c => c.CountryId == countryId).Select(c => c.CountryName).FirstOrDefault();
            //var stateName = _context.States.Where(c => c.StateId == stateId).Select(c => c.StateName).FirstOrDefault();

            //ViewData["StateId"] = stateName;
            //ViewData["CountryId"] = countryName;
            return View();
            //ViewData["state"] = new SelectList(_context.States, "StateId", "StateName");
            //ViewData["country"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            //return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Name,LastName,StateName,CountryName,StateId")] Person person)
        {

            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Next));
            }

            //ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", person.State.StateId);
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", person.State.Country.CountryId);
            return View(person);

            //var countryName = _context.Countries.Where(c => c.CountryId == countryId).Select(c => c.CountryName).FirstOrDefault();
            //var stateName = _context.States.Where(c => c.CountryId == countryId).Select(c => c.StateName).FirstOrDefault();
            //var worldModel = new WorldModel();
            //worldModel.People = _context.People.Include(c => c.State).Where(c => c.StateId==stateId).ToList();

            //if (stateId == null)
            //{
            //    NotFound();
            //}

            //var db = new WorldModel();
            //var state = from p in db.States
            //            where p.StateId == stateId
            //            select p.StateId;

            //if (stateId == null)
            //{
            //    return NotFound();
            //}

            //var state = await _context.People
            //    .Include(p => p.State)
            //    .FirstOrDefaultAsync(m => m.StateId == stateId);  
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            //ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", person.State.StateId);
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", person.State.Country.CountryId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Name,LastName,StateId,StateName,CountryId,CountryName")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            //ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", person.State.StateId);
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", person.State.Country.CountryId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.State)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }

        public ActionResult Next()
        {
            //ViewData["Name"] = new SelectList(_context.People, "Name", "Name");
            //ViewData["LastName"] = new SelectList(_context.People, "LastName","LastName");

            var pers = (from s in _context.People
                           select new { s.Name,s.LastName }).ToList();
            var name = "";
            var lastname = "";

            foreach (var k in pers)
            {
                name = k.Name;
                lastname = k.LastName;
            }


            //ViewBag.Ad = _context.People.Where(s => s.Name == name).Select(s=>s.Name).FirstOrDefault();
            //ViewBag.Soyad = _context.People.Where(s => s.LastName == lastname).Select(s=>s.LastName).FirstOrDefault();
            ViewBag.Ad = new SelectList(_context.People.Where(s => s.Name == name),"Name","Name");
            ViewBag.Soyad = new SelectList(_context.People.Where(s => s.LastName == lastname),"LastName","LastName");
            
            //ViewBag.Ad = (from s in _context.People
            //              where (s.Name == name)
            //              select (s.Name)).FirstOrDefault();
            //ViewBag.Soyad = (from s in _context.People
            //              where (s.LastName == lastname)
            //              select (s.LastName).First());


            return View();
        }
        [HttpPost, ActionName("Next")]
        [ValidateAntiForgeryToken]
        public ActionResult Next([Bind("PersonId,Name,LastName,Address,Tel")] Person person)
        {

            if (ModelState.IsValid)
            {
                _context.Add(person);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}
