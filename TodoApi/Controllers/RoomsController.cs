using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTO;
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
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            return await _context.Rooms.Include(location => location.FieldValues).ThenInclude(values => values.Field).ToListAsync();
        }

        // GET: api/Rooms
        [HttpPost]
        [Route("FilterRooms")]
        public async Task<ActionResult<IEnumerable<Room>>> FilterRooms(Filter filter)
        {
            var roomsfromRegions = _context.Regions
                .Include(region => region.Sites)
                .ThenInclude(site => site.Buildings)
                .ThenInclude(building => building.Floors)
                .ThenInclude(floor => floor.Rooms)
                .ToList()
                .Where((region, i) => filter.RegionIds.Contains(region.Id)).SelectMany(region => region.Sites.SelectMany(site => site.Buildings.SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms)))).ToList();

            var roomsfromSites = _context.Sites
                .Include(site => site.Buildings)
                .ThenInclude(building => building.Floors)
                .ThenInclude(floor => floor.Rooms)
                .ToList()
                .Where((site, i) => filter.SiteIds.Contains(site.Id)).SelectMany(site => site.Buildings.SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms))).ToList();

            var roomsfromBuildings = _context.Building
                .Include(building => building.Floors)
                .ThenInclude(floor => floor.Rooms)
                .ToList()
                .Where((site, i) => filter.BuildingIds.Contains(site.Id)).SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms)).ToList();

            var roomsfromFloors = _context.Floor
                .Include(floor => floor.Rooms)
                .ToList()
                .Where((site, i) => filter.FloorIds.Contains(site.Id)).SelectMany(floor => floor.Rooms).ToList();

           var res = roomsfromRegions.Union(roomsfromSites).Union(roomsfromBuildings).Union(roomsfromFloors);


           if (filter.Fields != null)
           {
               var expressionTree = ExpressionHelper.ExpressionHelper.ConstructAndExpressionTree<Room>(filter.Fields);
               var anonymousFunc = expressionTree.Compile();
               res = res.Where(anonymousFunc);
            }
          


            //field filtration
            /*foreach (var keyValue in filter.Fields)
            {
                var prop = keyValue.Name;
                var val = keyValue.Value;
                res.Where(c => c.GetType().GetProperty(prop).Name == prop && c.GetType().GetProperty(prop).GetValue());
            }*/



            /*var all = await _context.Rooms.Include(location => location.FieldValues).ThenInclude(values => values.Field).ToListAsync();
            return all.Where((model, i) => filter.FloorIds.Contains(model.FloorId)).ToList();*/
            return Ok(res);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(long id)
        {
            var roomModel = await _context.Rooms.Include(location => location.FieldValues)
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
        public async Task<IActionResult> PutRoom(long id, Room roomModel)
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
                if (!RoomExists(id))
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
        public async Task<ActionResult<Room>> PostRoom(Room roomModel)
        {
            _context.Rooms.Add(roomModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = roomModel.Id }, roomModel);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
      //  [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<Room>> DeleteRoom(long id)
        {
            var roomModel = await _context.Rooms.FindAsync(id);
            if (roomModel == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(roomModel);
            await _context.SaveChangesAsync();

            return roomModel;
        }

        private bool RoomExists(long id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }

    

}
