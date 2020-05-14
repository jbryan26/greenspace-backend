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
    public class ResourceTypesController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public ResourceTypesController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/ResourceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceType>>> GetResourceTypes()
        {
            return await _context.ResourceTypes.ToListAsync();
        }

        // GET: api/ResourceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceType>> GetResourceType(long id)
        {
            var resourceType = await _context.ResourceTypes.FindAsync(id);

            if (resourceType == null)
            {
                return NotFound();
            }

            return resourceType;
        }

        // PUT: api/ResourceTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResourceType(long id, ResourceType resourceType)
        {
            if (id != resourceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(resourceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceTypeExists(id))
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

        // POST: api/ResourceTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ResourceType>> PostResourceType(ResourceType resourceType)
        {
            _context.ResourceTypes.Add(resourceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResourceType", new { id = resourceType.Id }, resourceType);
        }

        // DELETE: api/ResourceTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResourceType>> DeleteResourceType(long id)
        {
            var resourceType = await _context.ResourceTypes.FindAsync(id);
            if (resourceType == null)
            {
                return NotFound();
            }

            _context.ResourceTypes.Remove(resourceType);
            await _context.SaveChangesAsync();

            return resourceType;
        }

        private bool ResourceTypeExists(long id)
        {
            return _context.ResourceTypes.Any(e => e.Id == id);
        }
    }
}
