using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RelationshipsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Relationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        {
            return Ok(await _uow.Relationships.AllAsyncApi());
        }

        // GET: api/Relationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> GetRelationship(int id)
        {
            var relationship = await _uow.Relationships.FindAsync(id);

            if (relationship == null)
            {
                return NotFound();
            }

            return relationship;
        }

        // PUT: api/Relationships/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutRelationship(int id, Relationship relationship)
        {
            if (id != relationship.Id)
            {
                return BadRequest();
            }

            _uow.Relationships.Update(relationship);
            await _uow.SaveChangesAsync();

            return NoContent();
        }*/

        // POST: api/Relationships
        /*[HttpPost]
        public async Task<ActionResult<Relationship>> PostRelationship(Relationship relationship)
        {
            await _uow.Relationships.AddAsync(relationship);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRelationship", new { id = relationship.Id }, relationship);
        }*/

        // DELETE: api/Relationships/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<Relationship>> DeleteRelationship(int id)
        {
            var relationship = await _uow.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }

            _uow.Relationships.Remove(relationship);
            await _uow.SaveChangesAsync();

            return relationship;
        }*/
    }
}
