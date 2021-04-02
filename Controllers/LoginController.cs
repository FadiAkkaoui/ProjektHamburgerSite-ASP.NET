using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjektHB.Models;

namespace ProjektHB.Controllers
{
    public class LoginController : Controller
    { 

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //async arbetar så att det inte laggar imellan medans sökningen sker, tar emot användarnamn och lösenord från view
        public async Task<IActionResult> IndexAsync(AdminLogin userinfo,string returnur1 =null )
        {
            //Hämta värden från en annan metod, tar in användarnamnet
            bool userOk=CheckUser(userinfo);
            //Om returnerad värde är true, körs koden
            if(userOk==true)
            {
                //Begär identitet/Tillåtelse för att få tillgång till webbplats, Säkerthet
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, userinfo.användarnamn));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                if(returnur1 != null)
                {
                    return Redirect(returnur1);
                }
                else
                {
                    //Om inloggning OK skickas man till adminsidan
                    return RedirectToAction("Admin", "Burger");
                }
            }
            else
            {
                //om inte OK visas ErrorMessage
                ViewBag.ErrorMessage = "Inloggning inte godkänt!";
            }
            return View();
        }
        //Användarnamn av admin, data kommer från async ovanför
        public bool CheckUser(AdminLogin userinfo)
        {
            if (userinfo.användarnamn == "Fadi" && userinfo.lösenord == "123")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //För att logga ut
        public async Task<IActionResult> SignOut(AdminLogin userinfo)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Admin", "Burger");
        }
    }
}
