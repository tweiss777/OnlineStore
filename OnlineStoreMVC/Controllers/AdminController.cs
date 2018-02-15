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

            return View(user);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Firstname,Lastname,Addr1,Addr2,Email")]Person user)
        {
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;

            ModelState.Remove("Password");

            if(id != user.UserID)
            {
                return RedirectToAction("Redirect404Error","Redirect404");
            }

            if(!ModelState.IsValid)
            {
                return RedirectToAction("Edit");
            }

            var success = await context.UpdateUserAsync(id,user.Firstname,user.Lastname,user.Addr1,user.Email,user.Addr2);
            if(success != true)
            {
                return RedirectToAction("Redirect404Error","Redirect404");

            }

            return RedirectToAction("CustomerIndex");
            
        }

        public async Task<IActionResult> EditPassword(int? id)
        {
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            List<Person> users = new List<Person>();


            if(id != null)
            {
                users = await context.GetUserByIDAsync((int)id);
            }

            Person user = users[0];

            return View(user);


        }


        public async Task<IActionResult> Delete(int? id)
        { 
            throw new NotImplementedException("please implement this method");
            
        }

        [ActionName("Delete")]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFinal(int id)
        {
            throw new NotImplementedException("please implement this method");
        }

    }
}