using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Auth;
using TodoApi.DTO;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ReservationsDbContext _context;

        public AdminController(ReservationsDbContext context)
        {
            _context = context;
          
        }

        /// <summary>
        /// Nuke and restore all DB. Pass "drop" in command to confirm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
       // [Authorize(Policy = "OnlySuperAdmin")]
        //  [Authorize(Policy = "OnlySiteAdmin")]

       
        public async Task<ActionResult<int>> NukeDb(string command)
        {
            if (command != "drop") return BadRequest("You don't want to nuke db, don't you?");
            
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            return Ok();
        }
    }
}