using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableReports : ControllerBase
    {
        public AvailableReports()
        {
        }

        //GET: api/AvailableReports
        [HttpGet]
        public ActionResult<String[]> GetAvailableReports()
        {
            return new string[] {"Room Report", "Occupancy Report", "Food and Beverage Report"};
        }
    }
}
