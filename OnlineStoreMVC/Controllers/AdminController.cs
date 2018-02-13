using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using OnlineStoreMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

//remove unecessary import statements
namespace OnlineStoreMVC.Controllers
{
    public class AdminController : Controller
    {

        public async Task<IActionResult>CustomerIndex(int? id) //change param type to int and make it nullable
        {
            int? userID = id;
            ViewData["Message"] = "";//used to display an error message if the user id is not found
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;

            List<Person> users = new List<Person>();

            if (userID == null)
            {
                users = await context.GetAllUsersAsync();
                return View(users);
            }

            users = await context.GetUserByIDAsync((int)userID);

            if(users.Count < 1)
            {
                ViewData["Message"] = "User ID " + userID + " not found!";
                users = await context.GetAllUsersAsync();
            }

            return View(users);
            
        }

        public async Task<IActionResult> Edit(int? id)
        { //This Action takes you to the edit view which is currently in development
            int? userID = id;
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            List<Person> users = new List<Person>();
            if(userID != null)
            {
                users = await context.GetUserByIDAsync((int)userID);
            }

            Person user = users[0];

            return RedirectToAction("Redirect404Error", "Redirect404");
        }


        public async Task<IActionResult> Delete(int? id)
        { 
            throw new NotImplementedException("please implement this method");
            
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFinal(int id)
        {
            throw new NotImplementedException("please implement this method");
        }

    }
}