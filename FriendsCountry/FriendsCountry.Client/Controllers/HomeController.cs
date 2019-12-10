using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FriendsCountry.Client.ViewModels;
using FriendsCountry.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FriendsCountry.Client.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}