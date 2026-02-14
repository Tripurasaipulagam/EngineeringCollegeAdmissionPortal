using EngineeringCollegeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace EngineeringCollegeWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public StudentController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IActionResult> Index(string? gender, int? courseId)
        {
            var client = _factory.CreateClient("api");

            var response = await client.GetAsync($"Students?gender={gender}&courseId={courseId}");
            List<Student> students = new();
            if (response.IsSuccessStatusCode)
                students = await response.Content.ReadFromJsonAsync<List<Student>>() ?? new();

            var courseResponse = await client.GetAsync("Students/courses");
            List<Course> courses = new();
            if (courseResponse.IsSuccessStatusCode)
                courses = await courseResponse.Content.ReadFromJsonAsync<List<Course>>() ?? new();

            ViewBag.Courses = courses;
            ViewBag.SelectedGender = gender;
            ViewBag.SelectedCourse = courseId;

            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCourses();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model, IFormFile? photo)
        {
            if (!ModelState.IsValid)
            {
                await LoadCourses();
                return View(model);
            }

            var client = _factory.CreateClient("api");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.FirstName ?? ""), "FirstName");
            content.Add(new StringContent(model.LastName ?? ""), "LastName");
            content.Add(new StringContent(model.Email ?? ""), "Email");
            content.Add(new StringContent(model.Address ?? ""), "Address");
            content.Add(new StringContent(model.Gender ?? ""), "Gender");
            content.Add(new StringContent(model.Year.ToString()), "Year");
            content.Add(new StringContent(model.CourseId.ToString()), "CourseId");

            if (photo != null)
                content.Add(new StreamContent(photo.OpenReadStream()), "photo", photo.FileName);

            var response = await client.PostAsync("Students", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("Email", error);
                await LoadCourses();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        private async Task LoadCourses()
        {
            var client = _factory.CreateClient("api");
            var response = await client.GetAsync("Students/courses");
            if (response.IsSuccessStatusCode)
                ViewBag.Courses = await response.Content.ReadFromJsonAsync<List<Course>>() ?? new List<Course>();
        }
    }
}
