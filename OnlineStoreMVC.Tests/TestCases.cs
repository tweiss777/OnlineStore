using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace OnlineStoreMVC.Tests
{
    public class TestCases: Controller
    {
        [Fact]
        public void Test1()
        {
            PersonContext pc = HttpContext.RequestServices.GetService(typeof(PersonContext)) as PersonContext;
            pc.ResetAutoIncrement();
        }
    }
}
