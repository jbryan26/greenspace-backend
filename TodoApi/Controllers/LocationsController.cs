using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
   
    public class LocationsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public LocationsController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations.Include(location => location.FieldValues).ThenInclude(values => values.Field).ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(long id)
        {
            var location = await _context.Locations.Include(location => location.FieldValues)
                            .ThenInclude(values => values.Field).FirstOrDefaultAsync(location1 => location1.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        [HttpPost]
        [Route("FilterLocations")]
        public async Task<ActionResult<List<Location>>> FilterLocations(Filter filter)
        {
            //todo: MATERILIZING FOR NOW!

            var exp = await _context.Locations.Include(location => location.FieldValues)
                .ThenInclude(values => values.Field).ToListAsync();

            /*var exp = _context.Locations.Include(location => location.FieldValues)
                .ThenInclude(values => values.Field).ToList().Where(((location1, i) =>
                    location1.FieldValues.Any(value => value.Value == "true" && value.Field.Name == "HaveProjector")));*/

            //IQueryable<Location> exp = null;
            /*foreach (var fld in filter.Fields)
            {
                if (fld.Value is string)
                {
                    if (fld.Condition == Condition.Eq)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueString.Contains(fld.Value.ToString()) && value.Field.Name == fld.Name))
                            .ToList(); /*.Include(location => location.FieldValues)
                    .ThenInclude(values => values.Field)#1#
                        ;
                    }
                    else return BadRequest("Can't do this operation with string field");
                }

                if (fld.Value is long)
                {
                    if (fld.Condition == Condition.Eq)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueInt == (long)fld.Value && value.Field.Name == fld.Name))
                            .ToList(); /*.Include(location => location.FieldValues)
                    .ThenInclude(values => values.Field)#1#
                        ;
                    }
                    if (fld.Condition == Condition.Less)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueInt < (long)fld.Value && value.Field.Name == fld.Name))
                            .ToList(); 
                        ;
                    }
                    if (fld.Condition == Condition.More)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueInt > (long)fld.Value && value.Field.Name == fld.Name))
                            .ToList(); 
                        ;
                    }
                }
                if (fld.Value is bool)
                {
                    if (fld.Condition == Condition.Eq)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueBool == (bool)fld.Value && value.Field.Name == fld.Name))
                            .ToList(); /*.Include(location => location.FieldValues)
                    .ThenInclude(values => values.Field)#1#
                        ;
                    }
                    else
                    {
                        return BadRequest("Can't do this operation with bool field");
                    }
                }
                if (fld.Value is DateTime)
                {
                    if (fld.Condition == Condition.Eq)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueDate == (DateTime)fld.Value && value.Field.Name == fld.Name))
                            .ToList(); /*.Include(location => location.FieldValues)
                    .ThenInclude(values => values.Field)#1#
                        ;
                    }
                    if (fld.Condition == Condition.Less)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueDate < (DateTime)fld.Value && value.Field.Name == fld.Name))
                            .ToList();
                        ;
                    }
                    if (fld.Condition == Condition.More)
                    {
                        exp = exp.Where((location1, i) =>
                                location1.FieldValues.Any(value =>
                                    value.ValueDate > (DateTime)fld.Value && value.Field.Name == fld.Name))
                            .ToList();
                        ;
                    }
                }


                

                //numeric

        

            }*/

            /*Expression<Func<Location, bool>> expr = new Expression<Func<Location, bool>>();
            var location = await _context.Locations.Include(location => location.FieldValues)
                .ThenInclude(values => values.Field).Where(expr).ToListAsync();*/
            var res =  exp.ToList();

            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        //  [Authorize(Policy = "OnlyCompanyAdmin")]
        // PUT: api/Locations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(long id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            /*_context.Entry(location).State = EntityState.Modified;

            _context.Entry(location.FieldValues).State = EntityState.Added;*/
            _context.Update(location);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
     //   [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
       // [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<Location>> DeleteLocation(long id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return location;
        }

        private bool LocationExists(long id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
