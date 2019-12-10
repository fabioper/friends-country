using FriendsCountry.Countries.API.Models;
using FriendsCountry.Domain.Entities;
using FriendsCountry.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Countries.API.Controllers
{
    [Route("api/states")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _repository;

        public StatesController(IStatesRepository repository)
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
        public async Task<IActionResult> Index(CreateState state)
        {
            if (ModelState.IsValid)
            {
                var newState = new State
                {
                    FlagUri = "",
                    Name = state.Name
                };

                await _repository.AddAsync(newState);

                return Ok();
            }

            return BadRequest(ModelState.Values.Select(e => e.Errors));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var state = await _repository.GetByIdAsync(id);

            if (state != null)
            {
                return Ok(state);
            }

            return NotFound();
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, CreateState vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors));

            var state = await _repository.GetByIdAsync(id);

            if (state != null)
            {
                state.FlagUri = "";
                state.Name = vm.Name;

                await _repository.UpdateAsync(state);

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove([FromRoute] long id)
        {
            var state = await _repository.GetByIdAsync(id);

            if (state != null)
            {
                await _repository.RemoveAsync(state);

                return Ok();
            }

            return BadRequest();
        }
    }
}
