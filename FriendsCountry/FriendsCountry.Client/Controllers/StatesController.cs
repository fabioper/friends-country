using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FriendsCountry.Client.VIewModels;
using Microsoft.AspNetCore.Mvc;

namespace FriendsCountry.Client.Controllers
{
    public class StatesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public StatesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateStateViewModel vm)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateStateViewModel vm)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] long id)
        {
            return View();
        }
    }
}