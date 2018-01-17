using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;

namespace OnlineStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region sign-up, login, and confirmation
        public IActionResult Login()
        {
            //will take you to the login page
            return View();
        }

        public IActionResult CustomerRegistration()
        {
            return View();
        }

        //Something is wrong here...
        // public IActionResult RegistrationConfirmation(]Person person)
        // {
        //     return View(person);
        // }

        #endregion



    }
}
