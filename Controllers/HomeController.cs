using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

       public IActionResult RegistrationConfirmation([Bind("Password,Firstname,Lastname,Addr1,Addr2,Email")] Person person)
        {
            //used to debug potential errors in validation
            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                return View(person); //Form validation is not showing up in the page
            }
            return RedirectToAction("CustomerRegistration");
        }


        [HttpPost,ValidateAntiForgeryToken,ActionName("Create")]
        public async Task<IActionResult>ThankYou([Bind("Password,Firstname,Lastname,Addr1,Addr2,Email")] Person person)
        {
            return null;
        }

        #endregion



    }
}
