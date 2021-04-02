using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektHB.Models;


namespace ProjektHB.Controllers
{
    public class UserController : Controller
    {
        /*Skapar ett objekt*/
        BurgerModel db = new BurgerModel();
        public IActionResult Index()
        {
            /*Skapar en lista*/
            List<Burger> burgerLista = db.Burgers.ToList();
            /*Returnerar data i listan till Index sidan för att visa burgare till användare*/
            return View(burgerLista);
        }
        public IActionResult Oppettider()
        {
            return View();
        }
        public IActionResult KontaktaOss()
        {
            return View();
        }
    }
}
