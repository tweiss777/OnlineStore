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

        public async Task<IActionResult>CustomerIndex(String userID)
        {
            ViewData["Error"] = "";//used to display an error message if the user id is not found
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;

            List<Person> users = new List<Person>();

            if (String.IsNullOrEmpty(userID))
            {
                users = await context.GetAllUsersAsync();
                if(users.)
                return View(users);
            }

            users = await context.GetUserByIDAsync(Convert.ToInt32(userID));


            return View(users);
            
        }

    }
}