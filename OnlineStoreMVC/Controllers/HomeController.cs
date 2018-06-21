﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

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

        [Route("Login")]
        public IActionResult Login()
        {
            //will take you to the login page
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")]Person person){
            //Find name and password in the database
            //if name and password match login and store cookie in browser
            //else.
            ViewData["Message"] = "";
            ViewData["Error"] = "";
            PersonContext pc = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;

            if (!ModelState.IsValid)
            {
                //If email and or password is missing
                ViewData["Error"] = "Invalid username or password.";
                Console.WriteLine("Login failed. One or more fields are missing.");
                return RedirectToAction("Login");
            }

            Person user = await pc.GetUserByEmailPassword(person.Email, person.Password);
            
            //If no user is returned (user = null) then user will be redirected to the login page.
            if(user == null)
            {
                ViewData["Error"] = "Invalid username or password.";
                Console.WriteLine("Login failed. Invalid username or password");
                return RedirectToAction("Login");
            }

            //Create a session with the user that logged in
            Console.WriteLine("Login was successful.");
            return RedirectToAction("Index");




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
