using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsCountry.Countries.API.Models;
using FriendsCountry.Domain.Entities;
using FriendsCountry.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FriendsCountry.Friends.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsRepository _repository;

        public FriendsController(IFriendsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var countries = await _repository.GetAllAsync();

            return Ok(countries);
        }

        [HttpPost("")]
        public async Task<IActionResult> Index(CreateFriend friend)
        {
            if (ModelState.IsValid)
            {
                var newFriend = new Friend
                {
                    Name = friend.Name
                };

                await _repository.AddAsync(newFriend);

                return Ok();
            }

            return BadRequest(ModelState.Values.Select(e => e.Errors));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var friend = await _repository.GetByIdAsync(id);

            if (friend != null)
            {
                return Ok(friend);
            }

            return NotFound();
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, CreateFriend vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors));

            var friend = await _repository.GetByIdAsync(id);

            if (friend != null)
            {
                friend.Name = vm.Name;
                friend.PhotoUri = vm.PhotoUri;
                friend.Email = vm.Email;
                friend.FamilyName = vm.FamilyName;
                friend.Birthdate = vm.Birthdate;
                friend.Phone = vm.Phone;
                friend.CountryId = vm.CountryId;
                friend.StateId = vm.StateId;

                await _repository.UpdateAsync(friend);

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove([FromRoute] long id)
        {
            var friend = await _repository.GetByIdAsync(id);

            if (friend != null)
            {
                await _repository.RemoveAsync(friend);

                return Ok();
            }

            return BadRequest();
        }
    }
}