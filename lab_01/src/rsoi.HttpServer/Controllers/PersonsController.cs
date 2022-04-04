using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rsoi.Core.Repositories;
using rsoi.Dto.Http;
using rsoi.Dto.Http.Converters;

namespace rsoi.Controllers
{
    [ApiController]
    [Route("api/v1/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        
        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPersons()
        {
            try
            {
                var persons = await _personRepository.GetPersonsAsync();
                var convertedPersons = new List<PersonDto>();

                foreach (var person in persons)
                {
                    var convertedPerson = FromCorePersonToDtoConverter.Convert(person);
                    convertedPersons.Add(convertedPerson);
                }

                return Ok(convertedPersons);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FindPerson([FromRoute][Required] int id)
        {
            try
            {
                var person = await _personRepository.FindPersonAsync(id);

                if (person == null)
                    return NotFound("Person with this Id does not exist.");

                var personDto = FromCorePersonToDtoConverter.Convert(person!);
                return Ok(personDto);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreatePerson([FromBody][Required] PersonDto personDto)
        {
            try
            {
                var persons = await _personRepository.GetPersonsAsync();
                var newIndex = persons.Count();
                
                await _personRepository.AddPersonAsync(newIndex,
                    personDto.Name!,
                    personDto.Age,
                    personDto.Address!,
                    personDto.Work!);

                return CreatedAtAction("FindPerson", new { id = newIndex }, personDto);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute][Required] int id)
        {
            try
            {
                await _personRepository.DeletePersonAsync(id);
                
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePerson([FromRoute][Required] int id, 
            [FromBody][Required] PersonDto personDto)
        {
            try
            {
                var person = await _personRepository.FindPersonAsync(id);

                if (person != null)
                {
                    var updatedPerson = await _personRepository.UpdatePersonAsync(id, 
                        personDto.Name,
                        personDto.Age,
                        personDto.Address,
                        personDto.Work);

                    return Ok(updatedPerson);
                }
                else
                    return NotFound(new JsonResult( new { message = "Person with this Id does not exist."}));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}