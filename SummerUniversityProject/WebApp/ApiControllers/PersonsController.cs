using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain;
using DTO;
using Newtonsoft.Json;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PersonsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons(string search, int? pageIndex, int? pageSize)
        {
            return Ok(await _uow.Persons.AllAsyncApi(search, pageIndex, pageSize));
        }
        
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var category = await _uow.Persons.CountDataAmount(search);
            return category;
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _uow.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpGet("GetPersonRelationships/{id}")]
        public async Task<ActionResult<PersonInRelationshipDTO>> GetPersonRelationships(int id)
        {
            var persons = await _uow.Persons.FindAllRelationshipsAsync(id);
            if (persons == null)
            {
                return NotFound();
            }
            
            return Ok(persons);
        }
        
        [HttpGet("GetDaughtersAndSons")]
        public async Task<ActionResult<IEnumerable<PersonDataDTO>>> GetDaughtersAndSons()
        {
            var persons = await _uow.Persons.GetDaughtersAndSons();
            if (persons == null)
            {
                return NotFound();
            }
            
            return Ok(persons);
        }
        
        [HttpGet("GetPersonsRelatedFamilyMembers/{id}")]
        public async Task<ActionResult<FamilyTreeDTO>> GetPersonsRelatedFamilyMembers(int id)
        {
            var persons = await _uow.Persons.GetFamilyTreeForPerson(id);
            if (persons == null)
            {
                return NotFound();
            }

            return Ok(persons);
        }

        [HttpGet("GetPersonWithMostPredecessors")]
        public async Task<ActionResult<PersonWithMostPredecessorsDTO>> GetPersonWithMostPredecessors()
        {
            var person = await _uow.Persons.GetPersonWithMostPredecessors();

            if (person == null)
            {
                return NotFound();
            }
            
            return Ok(person);
        }
        
        [HttpGet("GetNthChildInFamily/{id}")]
        public async Task<ActionResult<NthChildInFamilyDTO>> GetNthChildInFamily(int id)
        {
            var person = await _uow.Persons.GetNthChildInFamily(id);

            if (person == null)
            {
                return NotFound();
            }
            
            return Ok(person);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (person == null || id != person.Id)
            {
                return BadRequest();
            }
            
            if (person.FirstName.Length < 1 || person.FirstName.Length > 128 ||
                person.LastName.Length < 1 || person.LastName.Length > 128 ||
                person.Age < 0 || person.Age > 122)
            {
                return BadRequest();
            }

            _uow.Persons.Update(person);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            
            if (person.FirstName.Length < 1 || person.FirstName.Length > 128 ||
                person.LastName.Length < 1 || person.LastName.Length > 128 ||
                person.Age < 0 || person.Age > 122)
            {
                return BadRequest();
            }
            await _uow.Persons.AddAsync(person);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _uow.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _uow.PersonInRelationships.DeleteAllRelationsForPerson(id);

            _uow.Persons.Remove(person);
            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}
