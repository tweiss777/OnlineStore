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
            //used to display an error message if the user id is not found
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
            ViewData["Message"]="";
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
            TempData["Message"]=String.Format("Customer id={0} information successfully updated",user.UserID);

            return RedirectToAction("CustomerIndex");
            
        }

        // This loads up the edit password view
        public Task<IActionResult> EditPassword(int? id)
        {
            Password password = new Password();
            if(id != null)
            {
                password.id = (int)id;
            }

            return View(model: password);

        }

        // Actual method for edit password
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int id,[Bind("id,OldPassword,NewPassword,NewPasswordConfirmed")]Password password)
        {
            ViewData["Message"]="";
            ViewData["Error"] = ""; // error message
            bool was_succes = false;
            //check if the model is valid
            if(!ModelState.IsValid)
            {
                ViewData["Error"] = "One or more fields are missing";
                return View(password);
            }

            if(password.NewPassword != password.NewPasswordConfirmed)
            {
                ViewData["Error"] = "Passwords must match!";
                return View(password);
            }
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            
            List<Person> users = await context.GetUserByIDAsync(id);
            Person user = users[0];

            if(password.OldPassword != user.Password)
            {
                ViewData["Error"] = "Old password entered does not match the one on record.";
                return View(password);
            }
            
            was_succes = await context.UpdateUserPasswordAsync(id,password.NewPassword);

            if(was_succes == false) return RedirectToAction("Redirect404Error","Redirect404");
            
            TempData["Message"] = String.Format("Password change successful for user id {0}!",user.UserID);
            return RedirectToAction("CustomerIndex");


        }


        public async Task<IActionResult> Delete(int? id)
        { 
            throw new NotImplementedException("please implement this method");
            
        }

        [ActionName("Delete")]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFinal(int id)
        {
            PersonContext context = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            List<Person> users = await context.GetUserByIDAsync(id);
            Person user = users[0];
            if(ModelState.IsValid)
            {
                return View(user);            
            }
            TempData["Message"] = String.Format("user id {0} not found!",user.UserID);
            return RedirectToAction("CustomerIndex");
        }

    }
}