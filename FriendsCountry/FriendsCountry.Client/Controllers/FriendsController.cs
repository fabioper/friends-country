using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FriendsCountry.Client.VIewModels;
using FriendsCountry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FriendsCountry.Client.Controllers
{
    [Route("friends")]
    public class FriendsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public FriendsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44374/api/friends");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var friends = JsonConvert.DeserializeObject<IEnumerable<Friend>>(responseBody);

                return View(friends);
            }
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            using (var client = _clientFactory.CreateClient())
            {
                var countriesResponse = await client.GetAsync("https://localhost:44329/api/countries");

                countriesResponse.EnsureSuccessStatusCode();

                string countriesResponseBody = await countriesResponse.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(countriesResponseBody).Select(s => new { s.Id, s.Name }).ToList();

                var statesResponse = await client.GetAsync("https://localhost:44329/api/states");

                statesResponse.EnsureSuccessStatusCode();

                string statesResponseBody = await statesResponse.Content.ReadAsStringAsync();
                var states = JsonConvert.DeserializeObject<IEnumerable<State>>(statesResponseBody).Select(c => new { c.Id, c.Name }).ToList();

                return View(new CreateFriendViewModel {
                    Countries = new SelectList(countries, "Id", "Name"),
                    States = new SelectList(states, "Id", "Name")
                });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateFriendViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            using (var client = _clientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:44374/api/friends", content);

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
                var response = await client.GetAsync($"https://localhost:44374/api/friends/{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var friend = JsonConvert.DeserializeObject<Friend>(responseBody);

                return View(friend);
            }
        }

        [HttpGet("{id:long}/edit")]
        public async Task<IActionResult> Update([FromRoute] long id)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var countriesResponse = await client.GetAsync("https://localhost:44329/api/countries");

                countriesResponse.EnsureSuccessStatusCode();

                string countriesResponseBody = await countriesResponse.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(countriesResponseBody).Select(s => new { s.Id, s.Name }).ToList();

                var statesResponse = await client.GetAsync("https://localhost:44329/api/states");

                statesResponse.EnsureSuccessStatusCode();

                string statesResponseBody = await statesResponse.Content.ReadAsStringAsync();
                var states = JsonConvert.DeserializeObject<IEnumerable<State>>(statesResponseBody).Select(c => new { c.Id, c.Name }).ToList();

                var response = await client.GetAsync($"https://localhost:44374/api/friends/{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var friend = JsonConvert.DeserializeObject<UpdateFriendViewModel>(responseBody);
                friend.Countries = new SelectList(countries, "Id", "Name");
                friend.States = new SelectList(states, "Id", "Name");

                return View(friend);
            }
        }

        [HttpPost("{id:long}/edit")]
        public async Task<IActionResult> Update([FromRoute] long id, UpdateFriendViewModel vm)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"https://localhost:44374/api/friends/{id}", content);

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
                var response = await client.DeleteAsync($"https://localhost:44374/api/friends/{id}");

                return RedirectToAction(nameof(Index));
            }
        }
    }
}