﻿using System;
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
    public class FieldsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public FieldsController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/Fields
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Field>>> GetFields(ParentType parentType = ParentType.NotSet)
        {


            if (parentType != ParentType.NotSet)
            {
                //todo: seems like ef core can convert comparasion of enum to sql (sic!) so doing it on client for now
                // return  _context.Fields.Where((field, i) => field.ParentType == parentType).ToListAsync();
                return _context.Fields.AsEnumerable().Where((field, i) => field.ParentType == parentType).ToList();
            }
            else return await _context.Fields.ToListAsync();
        }

        // GET: api/Fields/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Field>> GetField(long id)
        {
            var @field = await _context.Fields.FindAsync(id);

            if (@field == null)
            {
                return NotFound();
            }

            return @field;
        }

        // PUT: api/Fields/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<IActionResult> PutField(long id, Field @field)
        {
            if (id != @field.Id)
            {
                return BadRequest();
            }

            _context.Entry(@field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
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

        // POST: api/Fields
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(200)]
        //  [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<Field>> PostField(Field @field)
        {
            _context.Fields.Add(@field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetField", new { id = @field.Id }, @field);
        }

        // DELETE: api/Fields/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        //  [Authorize(Policy = "OnlyCompanyAdmin")]
        public async Task<ActionResult<Field>> DeleteField(long id)
        {
            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();

            return @field;
        }

        private bool FieldExists(long id)
        {
            return _context.Fields.Any(e => e.Id == id);
        }
    }
}
