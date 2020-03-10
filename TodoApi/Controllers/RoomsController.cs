using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public RoomsController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetRoomModels()
        {
            return await _context.RoomModels.Include(location => location.FieldValues).ThenInclude(values => values.Field).ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomModel>> GetRoomModel(long id)
        {
            var roomModel = await _context.RoomModels.Include(location => location.FieldValues)
                .ThenInclude(values => values.Field).FirstOrDefaultAsync(location1 => location1.Id == id);

            if (roomModel == null)
            {
                return NotFound();
            }

            return roomModel;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
       // [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<IActionResult> PutRoomModel(long id, RoomModel roomModel)
        {
            if (id != roomModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomModelExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
      //  [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<RoomModel>> PostRoomModel(RoomModel roomModel)
        {
            _context.RoomModels.Add(roomModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomModel", new { id = roomModel.Id }, roomModel);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
      //  [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<RoomModel>> DeleteRoomModel(long id)
        {
            var roomModel = await _context.RoomModels.FindAsync(id);
            if (roomModel == null)
            {
                return NotFound();
            }

            _context.RoomModels.Remove(roomModel);
            await _context.SaveChangesAsync();

            return roomModel;
        }

        private bool RoomModelExists(long id)
        {
            return _context.RoomModels.Any(e => e.Id == id);
        }
    }
}
