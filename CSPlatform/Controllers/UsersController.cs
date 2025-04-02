using CSPlatform.Data;
using CSPlatform.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            List<User> users = databaseHelper.GetAllUsers();
            
            return Ok(new { message = "Successful", users });
        }

        [HttpGet("get-my-email")]
        public IActionResult GetUserIdByEmail(string email)
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            int userid = databaseHelper.GetUserIdByEmail(email);

            return Ok(new { message = "Successful", userid });
        }



        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();          
            dbHelper.AddUser(user);

            return Ok(new { message = "User created successfully", user });


        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.UpdateUser(user);

            return Ok(new { message = "User updated successfully", user });


        }

        [HttpDelete]
        public IActionResult DeleteUser(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid email.");
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            int userId = dbHelper.GetUserIdByEmail(email);

            dbHelper.DeleteUser(email);

            return Ok(new { message = "User deleted successfully" });

        }

        /*[HttpGet]
        public IActionResult GetUserByUserId()
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            User user = databaseHelper.GetUserByUserId();

            return Ok(new { message = "Successful", user });
        }*/
    }
}
