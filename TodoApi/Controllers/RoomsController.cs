using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TodoApi.DTO;
using TodoApi.Helpers;
using TodoApi.Models;
using Image = TodoApi.Models.Image;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;


        public RoomsController(ReservationsDbContext context, IHostingEnvironment hostingEnvironment , IMapper mapper)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            this._environment = _environment;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoom()
        {
            return await _context.Rooms.Include(location => location.ResourceType).Include(location => location.FieldValues).ThenInclude(values => values.Field).ProjectTo<RoomDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        // GET: api/Rooms
        [HttpPost]
        [Route("FilterRooms")]
        [ProducesResponseType(typeof(IEnumerable<Room>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<IEnumerable<RoomDto>>> FilterRooms(Filter filter)
        {
            //validate filter
            if (filter == null) return BadRequest("You should provide filter");
            if (filter.Fields != null)
            {
                if (filter.Fields.Count == 0) return BadRequest("You should provide some fields");
            }

            try
            {
                var res = FilterRoomsBll(filter);
                return Ok(res);
            }
            catch (IncorrectFieldFilterException e)
            {
                return BadRequest(e.Message);
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
           
        }

        //todo: refactor to BLL
        private IEnumerable<RoomDto> FilterRoomsBll(Filter filter)
        {
            var roomsfromRegions = _context.Regions
                .Include(region => region.Sites)
                    .ThenInclude(site => site.Buildings)
                        .ThenInclude(building => building.Floors)
                            .ThenInclude(floor => floor.Rooms)
                                .ThenInclude(room => room.Images)
                .Include(region => region.Sites)
                    .ThenInclude(site => site.Buildings)
                        .ThenInclude(building => building.Floors)
                            .ThenInclude(floor => floor.Rooms)
                                .ThenInclude(room => room.ResourceType)

                .ToList()
                .Where((region, i) => filter.RegionIds?.Contains(region.Id) ?? true).SelectMany(region =>
                    region.Sites.SelectMany(site =>
                        site.Buildings.SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms)))).ToList();

            var roomsfromSites = _context.Sites
                .Include(site => site.Buildings)
                    .ThenInclude(building => building.Floors)
                        .ThenInclude(floor => floor.Rooms)
                            .ThenInclude(room => room.Images)
                .Include(site => site.Buildings)
                    .ThenInclude(building => building.Floors)
                        .ThenInclude(floor => floor.Rooms)
                            .ThenInclude(room => room.ResourceType)
                .ToList()
                .Where((site, i) => filter.SiteIds?.Contains(site.Id) ?? true).SelectMany(site =>
                    site.Buildings.SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms))).ToList();

            var roomsfromBuildings = _context.Building
                .Include(building => building.Floors)
                    .ThenInclude(floor => floor.Rooms)
                        .ThenInclude(room => room.Images)
                .Include(building => building.Floors)
                    .ThenInclude(floor => floor.Rooms)
                        .ThenInclude(room => room.ResourceType)
                .ToList()
                .Where((site, i) => filter.BuildingIds?.Contains(site.Id) ?? true)
                .SelectMany(building => building.Floors.SelectMany(floor => floor.Rooms)).ToList();

            var roomsfromFloors = _context.Floor
                .Include(floor => floor.Rooms)
                .ThenInclude(room => room.Images)
                .Include(floor => floor.Rooms)
                .ThenInclude(room => room.ResourceType)
                .ToList()
                .Where((site, i) => filter.FloorIds?.Contains(site.Id) ?? true).SelectMany(floor => floor.Rooms).ToList();



               var res = roomsfromRegions.Union(roomsfromSites).Union(roomsfromBuildings).Union(roomsfromFloors).AsQueryable().ProjectTo<RoomDto>(_mapper.ConfigurationProvider).ToList();
              
          //  var res = roomsfromRegions.Union(roomsfromSites).Union(roomsfromBuildings).Union(roomsfromFloors).AsEnumerable();


            if (filter.Fields != null)
            {
                try
                {
                    //res.Where((dto, i) => dto.HasDockingStation == true);

                    var expressionTree = ExpressionHelper.ExpressionHelper.ConstructAndExpressionTree<RoomDto>(filter.Fields);
                    var anonymousFunc = expressionTree.Compile();
                    res = res.AsEnumerable().Where(anonymousFunc).ToList();
                }

                catch (ArgumentException e)
                {
                    throw new IncorrectFieldFilterException("Seems like Fields contain incorrect fieldname or value");
                 //  return BadRequest("Seems like Fields contain incorrect fieldname or value");
               }
            }

            return res;
        }

        [HttpPost]
        [Route("GetReservations")]
        
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations(Filter filter, DateTime startDate, DateTime endDate)
        {
            //validate filter
            if (filter == null) return BadRequest("You should provide filter");
            if (filter.Fields != null)
            {
                if (filter.Fields.Count == 0) return BadRequest("You should provide some fields");
            }

            try
            {
                var res = FilterRoomsBll(filter);
                var roomIds = res.Select(room => room.Id);
                var reservations = await _context.ReservationModels.Include(model => model.FoodDetailItems)
                    .Where(model => model.StartTime > startDate && model.EndTime < endDate)
                    .Where(model => roomIds.Contains(model.RoomId)).ProjectTo<ReservationDto>(_mapper.ConfigurationProvider).ToListAsync();
                return reservations;
            }
            catch (IncorrectFieldFilterException e)
            {
                return BadRequest(e.Message);
            }


            /*var mthd =  FilterRooms(filter);



            if (res == null) return Ok();
            var roomIds = res.Value.ToList().Select(room => room.Id);
            var reservations = await _context.ReservationModels
                .Where(model => model.StartTime > startDate && model.EndTime < endDate)
                .Where(model => roomIds.Contains(model.Id)).ProjectTo<ReservationDto>(_mapper.ConfigurationProvider).ToListAsync();
            return reservations;

              */
            return Ok();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Room), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoomDto>> GetRoom(long id)
        {
            var roomModel = await _context.Rooms.Include(room => room.Images)
                .Include(location => location.ResourceType)
                .Include(location => location.FieldValues)
                .ThenInclude(values => values.Field)
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(location1 => location1.Id == id);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(200)]
        //  [Authorize(Policy = "OnlyCompanyAdmin")]
        //    public async Task<ActionResult<RoomDto>> PostRoom([FromForm(Name = "file")][AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]IFormFile file, Room roomModel)
          public async Task<ActionResult<RoomDto>> PostRoom([FromForm]RoomWithImage roomWithImage)

        //  public async Task<ActionResult<RoomDto>> PostRoom(IFormFile file, Room roomModel)
      //    public async Task<ActionResult<RoomDto>> PostRoom([FromForm(Name = "file")][AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]IFormFile file, [FromForm]string roomModelStr)

        {
          /*var  roomModel = roomWithImage.Room;
          var file = roomWithImage.File;*/

          var roomModel = JsonConvert.DeserializeObject<Room>(roomWithImage.RoomStr);
          var files = roomWithImage.Files;

            _context.Rooms.Add(roomModel);
            await _context.SaveChangesAsync();


            if (files?.Count != 0)
            {
                foreach (var file in files)
                {
                    await UploadFile(file, roomModel);
                }
            }

            /* if there would be files
             if (files != null)
            {
                foreach (var formFile in files)
                {
                   await PostImage(formFile, roomModel.Id);
                }
            }*/

            return CreatedAtAction("GetRoom", new { id = roomModel.Id }, _mapper.Map<RoomDto>(roomModel));
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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


      public class EmailForm
      {
          [Display(Name = "Add a picture")]
          [DataType(DataType.Upload)]
          [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
          public IFormFile SubmitterPicture { get; set; }
      }

        [HttpPost]
        [Route("PostImage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        // public async Task<IActionResult> PostImage([FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]IFormFile file, long id)
        public async Task<IActionResult> PostImage([AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]IFormFile file, long id)
        {
            var room = _context.Rooms.Include(room1 => room1.Images).SingleOrDefault(room2 => room2.Id == id);
            if (room == null) return BadRequest("Room not found");

            await UploadFile(file, room);

            return Ok(_mapper.Map<RoomDto>(room));
          //  return Ok(new { count = files.Count });
      }

        private async Task UploadFile(IFormFile file, Room room)
        {
            //var fileName = Path.GetFileName(file.FileName);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, fileName);

            var fileNameRes = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePathRes = Path.Combine(_hostingEnvironment.ContentRootPath, fileNameRes);

            var fileNameTn = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePathTn = Path.Combine(_hostingEnvironment.ContentRootPath, fileNameTn);

            string filePathFull;
            string filePathThumbnail;

            // Bitmap bmp;
            await using (var fileSteam = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
                //  fileSteam.Position = 0;
                //   bmp = new Bitmap(fileSteam);
            }


            //resize if needed
            filePathFull = ImageHelper.ResizeImage(filePath, filePathRes, false);
            filePathThumbnail = ImageHelper.ResizeImage(filePath, filePathTn, true);
            //make thumbnail


            room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"{fileName}",
            });
            room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"{fileNameTn}",
                IsThumbnail = true,
                PathToFullImage = fileName
            });

            /*room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"tn.jpg",
                IsThumbnail = true,
                PathToFullImage = fileName
            });*/
            _context.SaveChanges();

            // var fls = HttpContext.Request.Form.Files;

            // var files = HttpContext.Request.Form.TryGetValue("");


            /*long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }*/

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }

        /*private async Task UploadFile(byte[] file, string filename, Room room)
        {
            //var fileName = Path.GetFileName(file.FileName);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, fileName);

            var fileNameRes = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePathRes = Path.Combine(_hostingEnvironment.ContentRootPath, fileNameRes);

            var fileNameTn = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePathTn = Path.Combine(_hostingEnvironment.ContentRootPath, fileNameTn);

            string filePathFull;
            string filePathThumbnail;

            // Bitmap bmp;
            await using (var fileSteam = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
                //  fileSteam.Position = 0;
                //   bmp = new Bitmap(fileSteam);
            }


            //resize if needed
            filePathFull = ImageHelper.ResizeImage(filePath, filePathRes, false);
            filePathThumbnail = ImageHelper.ResizeImage(filePath, filePathTn, true);
            //make thumbnail


            room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"{fileName}",
            });
            room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"{fileNameTn}",
                IsThumbnail = true,
                PathToFullImage = fileName
            });

            /*room.Images.Add(new Image()
            {
                Name = file.FileName,
                Path = $"tn.jpg",
                IsThumbnail = true,
                PathToFullImage = fileName
            });#1#
            _context.SaveChanges();

            // var fls = HttpContext.Request.Form.Files;

            // var files = HttpContext.Request.Form.TryGetValue("");


            /*long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }#1#

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }*/


        private bool RoomExists(long id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }

        public class RoomWithImage
        {
            /*[FromForm]
            public Room Room { get; set; }*/

            [FromForm] public string RoomStr { get; set; }

            [FromForm]
            [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
            public ICollection<IFormFile> Files { get; set; }
        }
    }

    internal class IncorrectFieldFilterException : Exception
    {
        public IncorrectFieldFilterException(string seemsLikeFieldsContainIncorrectFieldnameOrValue) : base(seemsLikeFieldsContainIncorrectFieldnameOrValue)
        {
          
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _Extensions;
        public AllowedExtensionsAttribute(string[] Extensions)
        {
            _Extensions = Extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);
            if (!(file == null))
            {
                if (!_Extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

      

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
