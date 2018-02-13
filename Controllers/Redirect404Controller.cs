using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;
using Microsoft.Net.Http;
using System.Collections.Generic;


namespace OnlineStoreMVC.Controllers
{
    public class Redirect404Controller: Controller
    {
        [Route("Redirect404")]
        public IActionResult Redirect404Error() => View();
    }
}