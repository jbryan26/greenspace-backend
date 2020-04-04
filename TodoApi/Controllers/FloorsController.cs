using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTO;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;
        private readonly IMapper _mapper;

        public FloorsController(ReservationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FloorDto>>> GetFloor()
        {
            return await _context.Floor.Include(floor => floor.Rooms).ProjectTo<FloorDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Floor>> GetFloor(long id)
        {
            var floor = await _context.Floor.Include(floor => floor.Rooms).ProjectTo<FloorDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(floor1 => floor1.Id == id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }

        // PUT: api/Floors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutFloor(long id, Floor floor)
        {
            if (id != floor.Id)
            {
                return BadRequest();
            }

            _context.Entry(floor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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

        // POST: api/Floors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Floor>> PostFloor(Floor floor)
        {
            _context.Floor.Add(floor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloor", new { id = floor.Id }, floor);
        }

        // DELETE: api/Floors/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Floor>> DeleteFloor(long id)
        {
            var floor = await _context.Floor.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            _context.Floor.Remove(floor);
            await _context.SaveChangesAsync();

            return floor;
        }

        private bool FloorExists(long id)
        {
            return _context.Floor.Any(e => e.Id == id);
        }
    }
}
