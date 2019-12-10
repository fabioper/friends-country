using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> IndexAsync()
        {
            var countries = await _repository.GetAllAsync();

            return Ok(countries);
        }

        [HttpPost("")]
        public async Task<IActionResult> IndexAsync(Country country)
        {
            if (ModelState.IsValid)
            {
                var newCountry = new Country
                {
                    Name = country.Name,
                    FlagUri = country.FlagUri,
                    States = country.States
                };

                await _repository.AddAsync(country);

                return Ok(new {
                    code = Response.StatusCode,
                    message = "País criado com sucesso!"
                });
            }

            return BadRequest(new {
                code = Response.StatusCode,
                message = "País não foi criado"
            });
        }
    }
}