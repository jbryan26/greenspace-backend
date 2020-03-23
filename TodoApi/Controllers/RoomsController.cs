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
               var expressionTree = ConstructAndExpressionTree<Room>(filter.Fields);
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


        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<FieldCondition> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = ExpressionRetriever.GetExpression<T>(param, filters[0]);
            }
            else
            {
                exp = ExpressionRetriever.GetExpression<T>(param, filters[0]);
                for (int i = 1; i < filters.Count; i++)
                {
                    exp = Expression.And(exp, ExpressionRetriever.GetExpression<T>(param, filters[i]));
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static class ExpressionRetriever
        {
            private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
            private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

            public static Expression GetExpression<T>(ParameterExpression param, FieldCondition filter)
            {
                MemberExpression member = Expression.Property(param, filter.Name);
                ConstantExpression constant = Expression.Constant(filter.Value);
                switch (filter.Condition)
                {
                    case Comparison.Equal:
                        return Expression.Equal(member, constant);
                    case Comparison.GreaterThan:
                        return Expression.GreaterThan(member, constant);
                    case Comparison.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);
                    case Comparison.LessThan:
                        return Expression.LessThan(member, constant);
                    case Comparison.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);
                    case Comparison.NotEqual:
                        return Expression.NotEqual(member, constant);
                    case Comparison.Contains:
                        return Expression.Call(member, containsMethod, constant);
                    case Comparison.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);
                    case Comparison.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                    default:
                        return null;
                }
            }

            public enum Comparison
            {
                Equal,
                LessThan,
                LessThanOrEqual,
                GreaterThan,
                GreaterThanOrEqual,
                NotEqual,
                Contains, //for strings  
                StartsWith, //for strings  
                EndsWith //for strings  
            }

        }

    }

    

}
