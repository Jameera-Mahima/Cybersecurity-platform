using CSPlatform.Data;
using CSPlatform.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost("enroll")]
        public IActionResult EnrollUser(int userId, int courseId, DateTime EnrollmentDate, string EnrollmentStatus)
        {
            if (userId <= 0 || courseId <= 0)
            {
                return BadRequest(new { message = "Invalid userId or courseId." });
            }

            DatabaseHelper databaseHelper = new DatabaseHelper();
            databaseHelper.EnrollUser(userId, courseId, EnrollmentDate, EnrollmentStatus);
            return Ok(new { message = "Enrollment successful", userId, courseId });
        }

        [HttpGet]
        public IActionResult GetEnrollmentsByUserId()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            List<Enrollment> enrollments = databaseHelper.GetAllEnrollments();

            if (enrollments == null || enrollments.Count == 0)
            {
                return NotFound(new { message = "No enrollments found" });
            }

            return Ok(new { message = "Successful", enrollments });
        }

        [HttpPut]
        public IActionResult UpdateEnrollment(int enrollmentid, DateTime EnrollmentDate, string EnrollmentStatus)
        {
            if (enrollmentid <= 0)
            {
                return BadRequest("Invalid user data.");
            }

            DatabaseHelper databaseHelper = new DatabaseHelper();
            databaseHelper.UpdateEnrollment(enrollmentid, EnrollmentDate, EnrollmentStatus);
            return Ok(new { message = "Enrollment is updated successfully", enrollmentid, EnrollmentDate, EnrollmentStatus});
        }

        [HttpDelete]
        public IActionResult DeleteEnrollment(int enrollmentid)
        {

            if (enrollmentid <= 0)
            {
                return NotFound(new { message = "Enrollment not found." });
            }

            DatabaseHelper databaseHelper = new DatabaseHelper();
            databaseHelper.DeleteEnrollment(enrollmentid);

            return Ok(new { message = "Enrollment deleted successfully. " });
        }
    }
}
