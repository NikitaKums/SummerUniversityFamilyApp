using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain;

namespace WebApp.Controllers
{
    public class PersonInRelationshipsController : Controller
    {
        private readonly AppDbContext _context;

        public PersonInRelationshipsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonInRelationships
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonInRelationships.Include(p => p.Person).Include(p => p.Person1).Include(p => p.Relationship);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonInRelationships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRelationship = await _context.PersonInRelationships
                .Include(p => p.Person)
                .Include(p => p.Person1)
                .Include(p => p.Relationship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInRelationship == null)
            {
                return NotFound();
            }

            return View(personInRelationship);
        }

        // GET: PersonInRelationships/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["Person1Id"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Relation");
            return View();
        }

        // POST: PersonInRelationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Person1Id,RelationshipId,Id")] PersonInRelationship personInRelationship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personInRelationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.PersonId);
            ViewData["Person1Id"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.Person1Id);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", personInRelationship.RelationshipId);
            return View(personInRelationship);
        }

        // GET: PersonInRelationships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRelationship = await _context.PersonInRelationships.FindAsync(id);
            if (personInRelationship == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.PersonId);
            ViewData["Person1Id"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.Person1Id);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", personInRelationship.RelationshipId);
            return View(personInRelationship);
        }

        // POST: PersonInRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Person1Id,RelationshipId,Id")] PersonInRelationship personInRelationship)
        {
            if (id != personInRelationship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personInRelationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonInRelationshipExists(personInRelationship.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.PersonId);
            ViewData["Person1Id"] = new SelectList(_context.Persons, "Id", "Id", personInRelationship.Person1Id);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", personInRelationship.RelationshipId);
            return View(personInRelationship);
        }

        // GET: PersonInRelationships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personInRelationship = await _context.PersonInRelationships
                .Include(p => p.Person)
                .Include(p => p.Person1)
                .Include(p => p.Relationship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInRelationship == null)
            {
                return NotFound();
            }

            return View(personInRelationship);
        }

        // POST: PersonInRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personInRelationship = await _context.PersonInRelationships.FindAsync(id);
            _context.PersonInRelationships.Remove(personInRelationship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonInRelationshipExists(int id)
        {
            return _context.PersonInRelationships.Any(e => e.Id == id);
        }
    }
}
