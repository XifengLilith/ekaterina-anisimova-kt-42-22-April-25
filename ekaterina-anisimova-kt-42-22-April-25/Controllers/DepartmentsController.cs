using Microsoft.AspNetCore.Mvc;
using ekaterina_anisimova_kt_42_22_April_25.Services;
using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using ekaterina_anisimova_kt_42_22_April_25.Filters;

namespace ekaterina_anisimova_kt_42_22_April_25.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> PostFilteredDepartments([FromBody] DepartmentFilter filter)
        {
            var departments = await _departmentService.GetFilteredDepartmentsAsync(filter);
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment([FromBody] Department department)
        {
            var addedDepartment = await _departmentService.AddDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = addedDepartment.DepartmentId }, addedDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, [FromBody] Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}