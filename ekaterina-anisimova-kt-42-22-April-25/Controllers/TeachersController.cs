using Microsoft.AspNetCore.Mvc;
using ekaterina_anisimova_kt_42_22_April_25.Services;
using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Filters;
using ekaterina_anisimova_kt_42_22_April_25.Models; 

namespace ekaterina_anisimova_kt_42_22_April_25.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService) 
        {
            _teacherService = teacherService;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> PostFilteredTeachers([FromBody] TeacherFilter filter)
        {
            var teachers = await _teacherService.GetFilteredTeachersAsync(filter);
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> PostTeacher([FromBody] Teacher teacher)
        {
            var addedTeacher = await _teacherService.AddTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = addedTeacher.TeacherId }, addedTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacher);
            if (updatedTeacher == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}