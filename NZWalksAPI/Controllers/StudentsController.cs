using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    // http://localhost:portNumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // http://localhost:portNumber/api/Students
        [HttpGet]
        public IActionResult GetAllStudentDetails()
        {
            string[] studentNames = new string[] { "John", "Elon", "James", "Emma", "Alexa" };
            return Ok(studentNames);
        }
    }
}
