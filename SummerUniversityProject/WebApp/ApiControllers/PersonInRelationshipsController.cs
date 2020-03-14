using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using DAL.App.Repositories;
using Domain;
using DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInRelationshipsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PersonInRelationshipsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PersonInRelationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonInRelationship>>> GetPersonInRelationships()
        {
            return Ok(await _uow.PersonInRelationships.AllAsync());
        }

        // GET: api/PersonInRelationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonInRelationshipSingleDTO>> GetPersonInRelationship(int id)
        {
            var personInRelationship = await _uow.PersonInRelationships.FindAsyncSingle(id);

            if (personInRelationship == null)
            {
                return NotFound();
            }

            return Ok(personInRelationship);
        }
        
        [HttpGet("GetYoungestAunt")]
        public async Task<ActionResult<PersonDataDTO>> GetYoungestAunt()
        {
            var persons = await _uow.PersonInRelationships.FindYoungestAunt();
            if (persons == null)
            {
                return NotFound();
            }

            return Ok(persons);
        }
        
        [HttpGet("GetYoungestUncle")]
        public async Task<ActionResult<PersonDataDTO>> GetYoungestUncle()
        {
            var persons = await _uow.PersonInRelationships.FindYoungestUncle();
            if (persons == null)
            {
                return NotFound();
            }

            return Ok(persons);
        }

        // PUT: api/PersonInRelationships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonInRelationship(int id, PersonInRelationship personInRelationship)
        {
            if (id != personInRelationship.Id)
            {
                return BadRequest();
            }

            if ((await ValidateRelationship(personInRelationship)) == false)
            {
                return BadRequest();
            }
            
            await AddBackwardsRelationIfNeeded(personInRelationship);

            _uow.PersonInRelationships.Update(personInRelationship);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PersonInRelationships
        [HttpPost]
        public async Task<ActionResult<PersonInRelationship>> PostPersonInRelationship(PersonInRelationship personInRelationship)
        {
            if ((await ValidateRelationship(personInRelationship)) == false)
            {
                return BadRequest();
            }

            await AddBackwardsRelationIfNeeded(personInRelationship);
            
            await _uow.PersonInRelationships.AddAsync(personInRelationship);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPersonInRelationship", new { id = personInRelationship.Id }, personInRelationship);
        }

        // DELETE: api/PersonInRelationships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonInRelationship>> DeletePersonInRelationship(int id)
        {
            var personInRelationship = await _uow.PersonInRelationships.FindAsync(id);
            if (personInRelationship == null)
            {
                return NotFound();
            }

            _uow.PersonInRelationships.Remove(personInRelationship);
            await _uow.SaveChangesAsync();

            return personInRelationship;
        }

        private async Task<bool> ValidateRelationship(PersonInRelationship personInRelationship)
        {
            if (personInRelationship.Person1Id == personInRelationship.PersonId)
            {
                return false;
            }

            var relation = await _uow.Relationships.FindAsync(personInRelationship.RelationshipId);
            var person = await _uow.Persons.FindAsync(personInRelationship.PersonId);
            var person1 = await _uow.Persons.FindAsync(personInRelationship.Person1Id);
            
            // Mom/Dad older than child
            if ((relation.Relation == Relation.Ema || relation.Relation == Relation.Isa) && person1.Age > person.Age)
            {
                return false;
            }
            
            // Granddad/Grandmother older than grandchild
            if ((relation.Relation == Relation.Vanaema || relation.Relation == Relation.Vanaisa) && person1.Age > person.Age)
            {
                return false;
            }

            return true;
        }

        private async Task AddBackwardsRelationIfNeeded(PersonInRelationship personInRelationship)
        {
            var relation = (await _uow.Relationships.FindAsync(personInRelationship.RelationshipId)).Relation;
            if (relation == Relation.Abikaasa)
            {
                await _uow.PersonInRelationships.AddAsync(new PersonInRelationship()
                {
                    Person1Id = personInRelationship.PersonId,
                    PersonId = personInRelationship.Person1Id,
                    RelationshipId = personInRelationship.RelationshipId
                });
            } 
            else if (relation == Relation.Vanaema || relation == Relation.Vanaisa)
            {
                await _uow.PersonInRelationships.AddAsync(new PersonInRelationship()
                {
                    Person1Id = personInRelationship.PersonId,
                    PersonId = personInRelationship.Person1Id,
                    RelationshipId = await _uow.Relationships.FindIdByString(Relation.Lapselaps)
                });
            }
            await _uow.SaveChangesAsync();
        }
    }
}
