using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using OnlineStoreMVC.HelperLayers;
using System.Security.Claims;

namespace OnlineStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            //Check if there is a cookie.
            //If cookie exists 
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

        [HttpPost,ActionName("Login"),ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser([Bind("Email,Password")]Person person){
            //Find name and password in the database
            //if name and password match login and store cookie in browser
            //else.
            PersonContext pc = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            


            if(!ModelState.IsValid)
            {
                //If email and or password is missing
                TempData["Error"] = "Invalid username or password.";
                return View();
            }

            Person user = await pc.GetUserByEmailPassword(person.Email, person.Password);

            if (user == null)
            {
                TempData["Error"] = "Invalid username or password";
                return View();
            }

            LoginHelper loginHelper = new LoginHelper(user);
            ClaimsPrincipal principle = loginHelper.GetClaimsPrincipal();

            try{
                await HttpContext.SignInAsync(principle);
            }
            catch(Exception e){
                TempData["Error"] = "Something went wrong while authenticating please try again later";
                return View();
            }

            //return redirect to the authenticated controller.
            return RedirectToAction("Index", "UserHome");


            // Set the cookie and return the view.
        }

        public IActionResult CustomerRegistration()
        {
           if(HttpContext.Session.GetString("person")!=null)
            {
                Person person = JsonConvert.DeserializeObject<Person>(HttpContext.Session.GetString("person"));
                return View(person);
            }

            return View();

        }
       [HttpPost,ValidateAntiForgeryToken]
       public IActionResult RegistrationConfirmation([Bind("Password,Firstname,Lastname,Addr1,Addr2,Email")] Person person)
        {
            //used to debug potential errors in validation
            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            

            if (ModelState.IsValid)
            {

                string personJSON = JsonConvert.SerializeObject(person); //converts object to json
                HttpContext.Session.SetString("person", personJSON); //store object in a session variable

                return View(person); //Form validation is not showing up in the page
            }
            return RedirectToAction("CustomerRegistration");
        }

        public async Task<IActionResult>ThankYou() //redirects to thank you page upong submitting 
        {
            //initialize a new person context below
            PersonContext personContext = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;

            

            if(HttpContext.Session.GetString("person") != null)
            {

                Person new_user = JsonConvert.DeserializeObject<Person>(HttpContext.Session.GetString("person"));//deserialize json to object
               //insert into database
               Console.WriteLine("adding user with the first name {0}",new_user.Firstname);
               var success = await personContext.InsertNewUserAsync(new_user);
               HttpContext.Session.Clear(); //clear session variable
               
               if(success != true)
               {
                   return RedirectToAction("Redirect404Error","Redirect404");//redirects to 404 page if something goes wrong 
               }
            }
           
            

            //return the view
            return View();//change return value 
        }
            //need to await an async task
        #endregion

        public async Task<IActionResult> TestPage()//used to test the context classes and commands
        {//this is to be removed upon final deployment
            PersonContext personContext = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            List<Person> users = new List<Person>();

            users = await personContext.GetAllUsersAsync();
            

            return View(users);
            //return RedirectToAction("Redirect404Error","Redirect404");//redirect to controller works
            //RedirectToAction(action,controller)

        }



    }
}
