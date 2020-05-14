using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendingsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public AttendingsController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attending>>> GetAttendings()
        {
            return await _context.Attendings.ToListAsync();
        }

        // GET: api/Attendings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attending>> GetAttending(long id)
        {
            var attending = await _context.Attendings.FindAsync(id);

            if (attending == null)
            {
                return NotFound();
            }

            return attending;
        }

        // PUT: api/Attendings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttending(long id, Attending attending)
        {
            if (id != attending.Id)
            {
                return BadRequest();
            }

            _context.Entry(attending).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Attending>> PostAttending(Attending attending)
        {
            _context.Attendings.Add(attending);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttending", new { id = attending.Id }, attending);
        }

        // DELETE: api/Attendings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attending>> DeleteAttending(long id)
        {
            var attending = await _context.Attendings.FindAsync(id);
            if (attending == null)
            {
                return NotFound();
            }

            _context.Attendings.Remove(attending);
            await _context.SaveChangesAsync();

            return attending;
        }

        private bool AttendingExists(long id)
        {
            return _context.Attendings.Any(e => e.Id == id);
        }
    }
}
