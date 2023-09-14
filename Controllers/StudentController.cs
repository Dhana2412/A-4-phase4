using A_4.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Xml;

namespace A_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly List<Student> _students = new List<Student>
        {
            new Student { Id = 1, FirstName = "John", LastName = "Doe", Grade = 95.5 },
            new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Grade = 88.0 }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _students.Find(s => s.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Invalid data");

            student.Id = GenerateNewStudentId(); // Generate a new unique student ID
            _students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || id != updatedStudent.Id)
                return BadRequest("Invalid data");

            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.Grade = updatedStudent.Grade;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentToRemove = _students.FirstOrDefault(s => s.Id == id);
            if (studentToRemove == null)
                return NotFound();

            _students.Remove(studentToRemove);
            return NoContent();
        }

        private int GenerateNewStudentId()
        {
            // Generate a new unique student ID. You can implement your own logic here.
            // For simplicity, we are just incrementing the maximum current ID by 1.
            return _students.Max(s => s.Id) + 1;
        }
    }
}



    

