using EngineeringCollegeAPI.Data;
using EngineeringCollegeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EngineeringCollegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Students?gender=Male&courseId=1
        [HttpGet]
        public async Task<IActionResult> Get(string? gender, int? courseId)
        {
            var query = _context.Students.Include(s => s.Course).AsQueryable();

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(s => s.Gender == gender);

            if (courseId.HasValue && courseId > 0)
                query = query.Where(s => s.CourseId == courseId);

            return Ok(await query.ToListAsync());
        }

        // GET: api/Students/courses
        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Student student, IFormFile? photo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Students.AnyAsync(s => s.Email == student.Email))
                return BadRequest("Email already exists");

            if (photo != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await photo.CopyToAsync(stream);

                student.ProfilePhoto = "/uploads/" + fileName;
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }
    }
}
