using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Generic_Repository;
using Server.Models;
using Shared.ViewModels;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.API
{
    public static class CRUD
    {
        private static readonly IConfiguration _configuration;
        static CRUD()
        {
            // Initialize configuration here
            _configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public static void MapMethodsUsingGenericRepositoryRoutes(this IEndpointRouteBuilder app)
        {
            //-----------------------------Login------------------------------------------

            app.MapPost("/login", async ([FromBody] LoginVM login, IGenericRepository repository) =>
            {
                var user = await Authenticate(login, repository);
                if (user != null)
                {
                    var token = GenerateToken(user, _configuration);
                    if(token != null)
                    {
                        return Results.Ok(new LoginResultVM { Token = token });
                    }
                    else
                    {
                        return Results.Unauthorized();
                    }
                }
                return Results.NotFound("Invalid Credentials");
            });


            //------------------------------Students----------------------------------------

            // Get All Students
            app.MapGet("/students", async (IGenericRepository repository) =>
            {
                var students = await repository.GetAll<Student>(x => x.User, x => x.Degree);

                if (students.Any())
                {
                    IEnumerable<GetStudentVM> allStudents = students
                    .Select(e => new GetStudentVM
                    {
                        Id = e.Id,
                        UserId = e.UserId,
                        RollNo = e.RollNo,
                        Degree = e.Degree.DegreeName,
                        Batch = e.Batch,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        Email = e.User.Email,
                        Address = e.User.Address,
                        MobileNo = e.User.MobileNo,
                        FatherName = e.User.FatherName,
                        MotherName = e.User.MotherName,
                        Dob = e.User.Dob
                    }).ToList();

                    return Results.Ok(allStudents);
                }

                return Results.NotFound("No students added");
            }).RequireAuthorization();

            // Get Student by Roll No.
            app.MapGet("/student/{rollNo}", async (int rollNo, IGenericRepository repository) =>
            {
                var studentData = await repository.Get<Student>(x => x.RollNo == rollNo, x => x.User, x => x.Degree);

                if (studentData != null)
                {
                    GetStudentVM data = new()
                    {
                        UserId = studentData.UserId,
                        RollNo = studentData.RollNo,
                        Degree = studentData.Degree.DegreeName,
                        Batch = studentData.Batch,
                        FirstName = studentData.User.FirstName,
                        LastName = studentData.User.LastName,
                        Email = studentData.User.Email,
                        Password = studentData.User.Password,
                        Address = studentData.User.Address,
                        MobileNo = studentData.User.MobileNo,
                        FatherName = studentData.User.FatherName,
                        MotherName = studentData.User.MotherName,
                        Dob = studentData.User.Dob,
                        DegreeId = studentData.DegreeId,
                        RoleId = studentData.User.RoleId,
                    };

                    return Results.Ok(data);
                }

                return Results.NotFound("No Student found with this Roll No.");
            }).RequireAuthorization();

            // Get All Students with Pagination
            app.MapGet("/pagination", async ([AsParameters] PagingParametersVM pagingParameters, IGenericRepository repository) =>
            {
                var students = await repository.GetFromMutlipleTableWithPagination<Student>(pagingParameters.PageIndex, pagingParameters.PageSize, x => x.User, x => x.Degree);

                if (students.Any())
                {
                    IEnumerable<GetStudentVM> allStudents = students
                        .Select(e => new GetStudentVM
                        {
                            UserId = e.UserId,
                            RollNo = e.RollNo,
                            Degree = e.Degree.DegreeName,
                            Batch = e.Batch,
                            FirstName = e.User.FirstName,
                            LastName = e.User.LastName,
                            Email = e.User.Email,
                            Address = e.User.Address,
                            MobileNo = e.User.MobileNo,
                            FatherName = e.User.FatherName,
                            MotherName = e.User.MotherName,
                            Dob = e.User.Dob
                        }).ToList();

                    return Results.Ok(allStudents);
                }

                return Results.NotFound("No students added");
            }).RequireAuthorization();

            //Add New Student
            app.MapPost("/student/Add", async ([FromBody] AddUpdateStudentVM newStudent, IGenericRepository repository) =>
            {
                if (newStudent == null)
                {
                    return Results.BadRequest("Invalid student data");
                }

                User user = new()
                {
                    RoleId = newStudent.RoleId,
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email,
                    Password = newStudent.Password,
                    Address = newStudent.Address,
                    MobileNo = newStudent.MobileNo,
                    Dob = newStudent.DOB,
                    FatherName = newStudent.FatherName,
                    MotherName = newStudent.MotherName,
                    CreatedBy = newStudent.CreatedBy,
                    CreatedOn = DateTime.Now,
                    UpdatedBy = newStudent.UpdatedBy,
                    UpdatedOn = DateTime.Now,

                };
                var userResult = await repository.Post<User>(user);

                if (userResult != null)
                {
                    Student student = new()
                    {
                        UserId = userResult.Id,
                        RollNo = newStudent.RollNo,
                        Batch = newStudent.Batch,
                        DegreeId = newStudent.DegreeId,
                        CreatedBy = newStudent.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = newStudent.UpdatedBy,
                        UpdatedOn = DateTime.Now,
                    };
                    var result = await repository.Post<Student>(student);
                    return Results.Ok("Student added succesfully");
                }

                return Results.BadRequest("Failed to create the student");
            }).RequireAuthorization();

            // Update the Student Data
            app.MapPut("/student/Update/{rollNo}", async (int rollNo, AddUpdateStudentVM student, IGenericRepository repository) =>
            {
                var studentToUpdate = await repository.Get<Student>(x => x.RollNo == rollNo);

                if (studentToUpdate != null)
                {
                    studentToUpdate.Batch = student.Batch;
                    studentToUpdate.DegreeId = student.DegreeId;
                    studentToUpdate.UpdatedBy = student.UpdatedBy;
                    studentToUpdate.UpdatedOn = DateTime.Now;

                    await repository.Put<Student>(studentToUpdate);

                    var user = await repository.Get<User>(x => x.Id == studentToUpdate.UserId);
                    if (user != null)
                    {
                        user.RoleId = student.RoleId;
                        user.FirstName = student.FirstName;
                        user.LastName = student.LastName;
                        user.Email = student.Email;
                        user.Password = student.Password;
                        user.Address = student.Address;
                        user.Dob = student.DOB;
                        user.MobileNo = student.MobileNo;
                        user.FatherName = student.FatherName;
                        user.MotherName = student.MotherName;
                        user.UpdatedBy = student.UpdatedBy;
                        user.UpdatedOn = DateTime.Now;

                        await repository.Put<User>(user);
                    }

                    return Results.Ok("Student Updated successfully");
                }



                return Results.NoContent();
            }).RequireAuthorization();

            // Delete Student by Roll No.
            app.MapDelete("/studentDelete/{rollNo}", async (int rollNo, IGenericRepository repository) =>
            {
                var student = await repository.Get<Student>(x => x.RollNo == rollNo);

                if (student != null)
                {
                    var userId = student.UserId;

                    // delete the Student
                    var studentDeletionResult = await repository.Delete<Student>(student);

                    // then delete the User
                    var user = await repository.Get<User>(x => x.Id == userId);
                    var userDeletionResult = user != null ? await repository.Delete<User>(user) : false;

                    if (studentDeletionResult && userDeletionResult)
                    {
                        return Results.Ok("Student deleted successfully");
                    }
                    else
                    {
                        return Results.BadRequest("Failed to delete student");
                    }
                }

                return Results.NotFound("No student found with this Roll No.");
            }).RequireAuthorization();



            //-------------------------------Degree-------------------------------------------

            //Get all Degree's
            app.MapGet("/allDegrees", async (IGenericRepository repository) =>
            {
                var degrees = await repository.GetAll<Degree>();

                if (degrees.Any())
                {
                    IEnumerable<DegreeVM> allRoles = degrees
                   .Select(e => new DegreeVM
                   {
                       Id = e.Id,
                       DepartmentId = e.DepartmentId,
                       DegreeName = e.DegreeName,
                       Duration = e.Duration,
                   }).ToList();
                    return Results.Ok(degrees);
                }

                return Results.NotFound("No degree added");
            });

            //------------------------------Role----------------------------------------------

            //Get all Roles
            app.MapGet("/allRoles", async (IGenericRepository repository) =>
            {
                var roles = await repository.GetAll<Role>();

                if (roles.Any())
                {
                    IEnumerable<RolesVM> allRoles = roles
                    .Select(e => new RolesVM
                    {
                        Id = e.Id,
                        RoleName = e.RoleName
                    }).ToList();
                    return Results.Ok(allRoles);
                }

                return Results.NotFound("No role added");
            });

        }


        //-----------------------------------Private Methods-------------------------------
        private static async Task<User> Authenticate(LoginVM login, IGenericRepository repository)
        {
            User user = null;
            var currentUser = await repository.Get<User>(x => x.Email == login.Email && x.Password == login.Password);
            if (currentUser != null)
            {

                user = new()
                {
                    Id = currentUser.Id,
                    RoleId = currentUser.RoleId,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email,
                };
                return user;
            }
            return null;
        }

        private static string GenerateToken(User user, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", user.RoleId.ToString()),
                new Claim("Name", user.FirstName),
                new Claim("Email", user.Email),
                new Claim("UserId", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: Claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

