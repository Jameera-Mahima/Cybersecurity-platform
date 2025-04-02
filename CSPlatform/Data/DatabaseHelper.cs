using CSPlatform.Models.Domain;
using Microsoft.Data.SqlClient;

namespace CSPlatform.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString = "";

        public DatabaseHelper()
        {
            _connectionString = "Server=mahimasPc\\ROOT;Database=CSPlatform;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (username, email, password, user_type, phone_number, date_of_birth, country_region) VALUES (@name, @email, @password, @usertype, @phonenumber, @dateofbirth, @countryregion)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@usertype", user.UserType);
                    cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@dateofbirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("@countryregion", user.CountryRegion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = [];

            using (SqlConnection conn = new (_connectionString))
            {
                conn.Open();
                string query = "SELECT user_id, username, email, password, user_type, phone_number, date_of_birth, country_region FROM Users";
                using (SqlCommand cmd = new (query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string userid = ((int)reader["user_id"]).ToString();
                            string username = (string)reader[1];
                            string email = (string)reader[2];
                            string password = (string)reader[3];
                            string usertype = (string)reader[4];
                            string? phonenumber = reader.IsDBNull(5) ? null : (string)reader["phone_number"];
                            DateTime? dateofbirth = reader["date_of_birth"] as DateTime?;
                            string? countryregion = reader.IsDBNull(7) ? null : (string)reader["country_region"];
                            users.Add(new User
                            {
                                UserId = userid,
                                UserName = username,
                                Email = email,
                                Password = password,
                                UserType = usertype,
                                PhoneNumber = phonenumber,
                                DateOfBirth = dateofbirth,
                                CountryRegion = countryregion
                            }) ;
                        }
                    }
                }
            }
            return users;
        }

        public int GetUserIdByEmail(string email)
        {
            int userid = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT user_id FROM Users WHERE email = @email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userid = (int)reader["user_id"];
                        };
                    }
                }
            }
            return userid;
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET username = @name, email = @email, password = @password, user_type = @usertype, phone_number = @phonenumber, date_of_birth = @dateofbirth, country_region = @countryregion WHERE user_id = @userid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userid", user.UserId);
                    cmd.Parameters.AddWithValue("@name", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@usertype", user.UserType);
                    cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@dateofbirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("@countryregion", user.CountryRegion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string userIdQuery = "SELECT user_id FROM Users WHERE email = @email";
                int userId = 0;
                using (SqlCommand userIdCmd = new SqlCommand(userIdQuery, conn))
                {
                    userIdCmd.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader = userIdCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = (int)reader["user_id"];
                        }
                    }
                    userIdCmd.ExecuteNonQuery();
                }

                string delenrollquery = "DELETE FROM Enrollments WHERE user_id = @userid";
                using (SqlCommand delenrollCmd = new SqlCommand(delenrollquery, conn))
                {
                    delenrollCmd.Parameters.AddWithValue("@userid", userId);
                    delenrollCmd.ExecuteNonQuery();
                }

                string deluserquery = "DELETE FROM Users WHERE email = @email";
                using (SqlCommand deluserCmd = new SqlCommand(deluserquery, conn))
                {
                    deluserCmd.Parameters.AddWithValue("@email", email);
                    deluserCmd.ExecuteNonQuery() ;
                }
            }
        }

        public void AddCourse(Course course)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Courses (user_generated_id, title, description, Instructor, CourseDuration) VALUES (@user_generated_id, @title, @description, @instructor, @courseDuration)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_generated_id", course.UserGeneratedId);
                    cmd.Parameters.AddWithValue("@title", course.Title);
                    cmd.Parameters.AddWithValue("@description", course.Description);
                    cmd.Parameters.AddWithValue("@instructor", course.Instructor);
                    cmd.Parameters.AddWithValue("@courseDuration", course.CourseDuration);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT user_generated_id, course_id, title, description, Instructor, CourseDuration FROM Courses";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string usergeneratedid = (string)reader[0];
                            string courseid = ((int)reader["course_id"]).ToString();
                            string title = (string)reader[2];
                            string description = (string)reader[3];
                            string? instructor = reader.IsDBNull(4) ? null : (string)reader["Instructor"];
                            string? courseDuration = reader.IsDBNull(5) ? null : (string)reader["CourseDuration"];
                            courses.Add(new Course
                            {
                                UserGeneratedId = usergeneratedid,
                                CourseId = courseid,
                                Title = title,
                                Description = description,
                                Instructor = instructor,
                                CourseDuration = courseDuration
                            });
                        }
                    }
                }
            }
            return courses;
        }

        public int GetCourseIdById(string user_generated_id)
        {
            int courseid = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT course_id FROM Courses WHERE user_generated_id = @user_generated_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@user_generated_id", user_generated_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            courseid = (int)reader["course_id"];
                        };
                    }
                }
            }
            return courseid;
        }

        public void UpdateCourse(Course course)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Courses SET user_generated_id = @usergeneratedid, title = @title, description = @description, instructor = @instructor, courseDuration = @courseduration WHERE course_id = @courseid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@courseid", course.CourseId);
                    cmd.Parameters.AddWithValue("@usergeneratedid", course.UserGeneratedId);
                    cmd.Parameters.AddWithValue("@title", course.Title);
                    cmd.Parameters.AddWithValue("@description", course.Description);
                    cmd.Parameters.AddWithValue("@instructor", course.Instructor);
                    cmd.Parameters.AddWithValue("@courseduration", course.CourseDuration);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCourse(string user_generated_id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string courseIdQuery = "SELECT course_id FROM Courses WHERE user_generated_id = @usergeneratedid";
                int courseId = 0;
                using (SqlCommand courseIdCmd = new SqlCommand(courseIdQuery, conn))
                {
                    courseIdCmd.Parameters.AddWithValue("@usergeneratedid", user_generated_id);
                    using (SqlDataReader reader = courseIdCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            courseId = (int)reader["course_id"];
                        }
                    }
                    courseIdCmd.ExecuteNonQuery();
                }

                string delenrollquery = "DELETE FROM Enrollments WHERE course_id = @courseid";
                using (SqlCommand delenrollCmd = new SqlCommand(delenrollquery, conn))
                {
                    delenrollCmd.Parameters.AddWithValue("@courseid", courseId);
                    delenrollCmd.ExecuteNonQuery();
                }

                string delcoursequery = "DELETE FROM Courses WHERE user_generated_id = @usergeneratedid";
                using (SqlCommand delcourseCmd = new SqlCommand(delcoursequery, conn))
                {
                    delcourseCmd.Parameters.AddWithValue("@usergeneratedid", user_generated_id);
                    delcourseCmd.ExecuteNonQuery();
                }
            }
        }

        public void EnrollUser(int userid, int courseid, DateTime enrollmentdate, string enrollmentstatus)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Enrollments (user_id, course_id, EnrollmentDate, EnrollmentStatus) VALUES (@userId, @courseId, @enrollmentDate, @enrollmentStatus)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userid);
                    cmd.Parameters.AddWithValue("@courseId", courseid);
                    cmd.Parameters.AddWithValue("@enrollmentDate", enrollmentdate);
                    cmd.Parameters.AddWithValue("@enrollmentStatus", enrollmentstatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> enrollments = [];

            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                string query = "SELECT enrollment_id, user_id, course_id, EnrollmentDate, EnrollmentStatus FROM Enrollments";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string enrollmentid = ((int)reader["enrollment_id"]).ToString();
                            string userid = ((int)reader["user_id"]).ToString();
                            string courseid = ((int)reader["course_id"]).ToString();
                            DateTime? enrollmentdate = reader.IsDBNull(3) ? null : (DateTime)reader["EnrollmentDate"];
                            string? enrollmentstatus = reader.IsDBNull(4) ? null : (string)reader["EnrollmentStatus"];
                            enrollments.Add(new Enrollment
                            {
                                EnrollmentId = enrollmentid,
                                UserId = userid,
                                CourseId = courseid,
                                EnrollmentDate = enrollmentdate,
                                EnrollmentStatus = enrollmentstatus
                            });
                        }
                    }
                }
            }
            return enrollments;
        }

        public void UpdateEnrollment(int enrollmentid, DateTime enrollmentdate, string enrollmentstatus)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Enrollments SET EnrollmentDate = @enrollmentdate, EnrollmentStatus = @enrollmentstatus WHERE enrollment_id = @enrollmentid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@enrollmentid", enrollmentid);
                    cmd.Parameters.AddWithValue("@enrollmentdate", enrollmentdate);
                    cmd.Parameters.AddWithValue("@enrollmentstatus", enrollmentstatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Enrollments WHERE enrollment_id = @enrollmentid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@enrollmentid", enrollmentId);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}


