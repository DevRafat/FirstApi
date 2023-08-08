using FirstApi.Models;
using FirstApi.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
      private readonly AplicationDbContext _context;

        public MajorsController(AplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult>  GetMajors()
        {
            var list = await _context.Majors.OrderBy(c=>c.Name).ToListAsync();
            return Ok(list);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajorById(int id)
        {
            var obj = await _context.Majors.FirstOrDefaultAsync(c => c.Id == id);
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
                 TestColumn = "dxdsdsd"

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
    }
}
