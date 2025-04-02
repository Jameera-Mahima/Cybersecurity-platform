using CSPlatform.Data;
using CSPlatform.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            List<Course> courses = databaseHelper.GetAllCourses();

            return Ok(new { message = "Successful", courses });
        }

        /* [HttpGet]
         public IActionResult GetCourseById()
         {
             DatabaseHelper databaseHelper = new DatabaseHelper();
             List<Course> courses = databaseHelper.GetCourseById();

             return Ok(new { message = "Successful", courses });
         } */

        [HttpGet("get-my-ID")]
        public IActionResult GetUserIdById(string user_generated_id)
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            int courseid = databaseHelper.GetCourseIdById(user_generated_id);

            return Ok(new { message = "Successful", courseid });
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course data is required.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.AddCourse(course);

            return Ok(new { message = "Course created successfully", course });


        }

        [HttpPut]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Invalid course data.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.UpdateCourse(course);

            return Ok(new { message = "Course updated successfully", course });


        }

        [HttpDelete]
        public IActionResult DeleteCourse(string user_generated_id)
        {
            if (user_generated_id == null)
            {
                return BadRequest("Invalid ID.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            int courseId = dbHelper.GetCourseIdById(user_generated_id);

            dbHelper.DeleteCourse(user_generated_id);

            return Ok(new { message = "Course deleted successfully" });

        }
    }
}
