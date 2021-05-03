using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProjectCountry.Data;
using ProjectCountry.Models;

namespace ProjectCountry.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var CountryState = new WorldModel();
            ViewBag.Count = new SelectList(_context.Countries, "CountryId", "CountryName");
            ViewBag.State = new SelectList(_context.States, "StateId", "StateName");
            return View(CountryState);

            //bir model oluşturacaksın.

            // ülkeleri getir ve modele ekle

            // eğer id boş değilse modele o ülkeye bağlı stateleri getir

            //stated leri modele ekle

            //modeli view a gönder

            //return View(/*model*/); // View adı olarak 'List' kullan
        }
        [HttpPost]
        public IActionResult CountryList(WorldModel worldModel) // id null olabilmeli
        {
            var country = (from s in _context.Countries
                           where s.CountryId == worldModel.CountryId
                           select new { s.CountryName}).ToList();

            var Ulke = "";

            foreach (var k in country)
            {
                Ulke = k.CountryName;
            }

            TempData["Country"] = Ulke;

            ViewBag.state = new SelectList(_context.States.Where(s => s.Country.CountryName == Ulke), "StateId", "StateName");

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
