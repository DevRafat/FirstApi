using AutoMapper;
using FirstApi.Filters;
using FirstApi.Models;
using FirstApi.Service.Major;
using FirstApi.Service.User;
using FirstApi.Tables;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[ServiceFilter(typeof(CustomFilter))]
    [Authorize(Roles ="Admin,User")]
    public class MajorsController : ControllerBase
    {
      private readonly AplicationDbContext _context;
      private readonly IMajorService _majorService;
      private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public MajorsController(AplicationDbContext context, IMajorService majorService, IMapper mapper, IUserService userService)
        {
            _context = context;
            _majorService = majorService;
            _mapper = mapper;
            _userService = userService;
        }

     



       
        [HttpGet("ListMajor")]

      
        public async Task<IActionResult>  GetMajors()
        {
            var userName =  _userService.GetCurrentLoggedIn();
          
            var list = await _majorService.GetAll();
          
            return Ok(list);

        }
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var list = await _context.Students.Include(c => c.Major).ToListAsync();
            return Ok(list);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajorById(int id)
        {
            var obj = await _context.Majors.FirstAsync(c => c.Id == id);
            if (obj == null)
            {
                return NotFound("the Major is not found");
            }

            return Ok(obj);

        }

        [HttpPost]
        public async Task<IActionResult> CreateMajor(MajorModel model)
        {
            var obj = new Major()
            {
                 Name = model.Name,
                 Description = model.Description,

            };
            await _context.Majors.AddAsync(obj);
            await _context.SaveChangesAsync();
            return Ok(obj);   

        }

        [HttpPut]
        public async Task<IActionResult> UpdateMajor(int id,MajorModel model)
        {

            var obj = await _context.Majors.FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                return NotFound("the Major is not found");
            }
            obj.Name = model.Name;
            obj.Description = model.Description;
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMajor(int id)
        {

            var obj = await _context.Majors.FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                return NotFound("the Major is not found");
            }

            _context.Majors.Remove(obj);
            await _context.SaveChangesAsync();

            return Ok();

        }


        [HttpGet("GetStudentsList")]
        public async Task<IActionResult> GetStudentsList()
        {
            var list = await _context.Students.Include(c=>c.Major).ToListAsync();
            var result=_mapper.Map<List<StudentModel>>(list);
            //var data = list.Select(v => new StudentModel()
            //{
            //    Id = v.Id,
            //    Name = v.Name,
            //    Description = v.Description,
            //    MajorName = v.Major?.Name

            //});
            return Ok(result);

        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var obj = await _context.Students.Include(c => c.Major).FirstOrDefaultAsync(c => c.Id == id);
            var result = _mapper.Map<StudentModel>(obj);
            
            return Ok(result);

        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> addStudent(StudentModel m)
        {

            var obj=_mapper.Map<Student>(m);
            obj.Major = null;
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
            return Ok(obj);

        }

        [HttpGet("SendMessage")]
        public IActionResult SendMessage(string email)
        {
            //BackgroundJob.Enqueue(() => _majorService.SendMessage(email));
            //BackgroundJob.Schedule(() => _majorService.SendMessage(email), TimeSpan.FromMinutes(1));
            RecurringJob.AddOrUpdate(() => _majorService.SendMessage(email), Cron.Monthly);

            return Ok();
        }

    }
}
