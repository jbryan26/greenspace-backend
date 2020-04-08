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
    public class ReservationController : ControllerBase
    {
        private readonly ReservationsDbContext _context;
        private readonly IMapper _mapper;

        public ReservationController(ReservationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationModels()
        {
            return await _context.ReservationModels.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationModel>> GetReservationModel(long id)
        {
           
           
            var reservationModel = await _context.ReservationModels.FindAsync(id);

            if (reservationModel == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReservationDto>(reservationModel);
        }

        // PUT: api/Reservation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationModel(long id, ReservationModel reservationModel)
        {
            if (id != reservationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationModelExists(id))
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

        // POST: api/Reservation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReservationModel>> PostReservationModel(ReservationModel reservationModel)
        {
            _context.ReservationModels.Add(reservationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationModel", new { id = reservationModel.Id }, reservationModel);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationModel>> DeleteReservationModel(long id)
        {
            var reservationModel = await _context.ReservationModels.FindAsync(id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            _context.ReservationModels.Remove(reservationModel);
            await _context.SaveChangesAsync();

            return reservationModel;
        }

        private bool ReservationModelExists(long id)
        {
            return _context.ReservationModels.Any(e => e.Id == id);
        }
    }
}
