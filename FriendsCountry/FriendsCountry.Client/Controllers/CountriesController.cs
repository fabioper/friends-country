using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FriendsCountry.Client.VIewModels;
using FriendsCountry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FriendsCountry.Client.Controllers
{
    [Route("countries")]
    public class CountriesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CountriesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44329/api/countries");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseBody);

                return View(countries);
            }
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCountryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            using (var client = _clientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:44329/api/countries", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync($"https://localhost:44329/api/countries/{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<Country>(responseBody);

                return View(country);
            }
        }

        [HttpGet("{id:long}/edit")]
        public async Task<IActionResult> Update([FromRoute] long id)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync($"https://localhost:44329/api/countries/{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<UpdateCountryViewModel>(responseBody);

                return View(country);
            }
        }

        [HttpPost("{id:long}/edit")]
        public async Task<IActionResult> Update([FromRoute] long id, UpdateCountryViewModel vm)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"https://localhost:44329/api/countries/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
        }

        [HttpGet("{id:long}/delete")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.DeleteAsync($"https://localhost:44329/api/countries/{id}");

                return RedirectToAction(nameof(Index));
            }
        }
    }
}