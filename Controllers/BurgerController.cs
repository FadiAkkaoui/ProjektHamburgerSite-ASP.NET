using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektHB.Models;

namespace ProjektHB.Controllers
{
    //Går till startup.cs
    [Authorize]
    public class BurgerController : Controller
    {
        /*Skapar ett objekt*/
        BurgerModel db = new BurgerModel();
        public IActionResult Index()
        {
            /*Skapar en lista*/
            List<Burger> burgerLista = db.Burgers.ToList();
            /*Returnerar data i listan till Index sidan*/
            return View(burgerLista);
        }
     
        public IActionResult Admin()
        {
            List<Burger> burgerLista = db.Burgers.ToList();
            return View(burgerLista);
            
            
        }
        public IActionResult CreateBurger()
        {
            return View();
        }
        [HttpPost]/*Hyper Text Transfer Protocol, skapad för att tillåta kommunication emellan*/
        /*Tar in inskickad data från CreateBurger.cshtml via Burger.cs */
        public IActionResult CreateBurger(Burger nyburger)
        {
            /*Lägger till data i databasen, koden nedanför sparar*/
            db.Burgers.Add(nyburger);
            db.SaveChanges();
            /*Efter slutförd arbete skickas man till adminsidan*/
            return RedirectToAction("Admin");
        }
        /*Tar in inskickad data, i detta fallet vald id*/
        public IActionResult UpdateBurger(int id)
        {
            /*Söker efter inskickad id i databasen och returnerar tillhörande info/data*/
            Burger burger = db.Burgers.Find(id);
            return View(burger);
        }
        [HttpPost]
        public IActionResult UpdateBurger(Burger uppdateraBurger)
        {
            /*Hämtar nya värden, Modified är för att uppdatera. I detta fallet tar den in ny inskickad data och ersätter */
            db.Entry(uppdateraBurger).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Admin");
        }

        public IActionResult DeleteBurger(int id)
        {
            //SQL fråga, som söker fram vald id och raderar från databasen, ett annat sätt att skriva.
            Burger burger = (from b in db.Burgers where b.id == id select b).FirstOrDefault();
            //Burger burger = db.Burgers.Find(id);
            return View(burger);
        }
        [HttpPost]
        public IActionResult DeleteBurger(Burger raderaBurger)
        {
            /*Raderar vald data från databasen*/
            db.Entry(raderaBurger).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Admin");
        }

    }
}
