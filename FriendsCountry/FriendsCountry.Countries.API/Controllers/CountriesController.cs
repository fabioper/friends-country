using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsCountry.Countries.API.Models;
using FriendsCountry.Domain.Entities;
using FriendsCountry.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FriendsCountry.Countries.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _repository;

        public CountriesController(ICountriesRepository repository)
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
        public async Task<IActionResult> Index(CreateCountry country)
        {
            if (ModelState.IsValid)
            {
                var newCountry = new Country
                {
                    Name = country.Name
                };

                await _repository.AddAsync(newCountry);

                return Ok();
            }

            return BadRequest(ModelState.Values.Select(e => e.Errors));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var country = await _repository.GetByIdAsync(id);

            if (country != null)
            {
                return Ok(country);
            }

            return NotFound();
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, CreateCountry vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors));

            var country = await _repository.GetByIdAsync(id);

            if (country != null)
            {
                country.Name = vm.Name;

                await _repository.UpdateAsync(country);

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove([FromRoute] long id)
        {
            var country = await _repository.GetByIdAsync(id);

            if (country != null)
            {
                await _repository.RemoveAsync(country);

                return Ok();
            }

            return BadRequest();
        }
    }
}