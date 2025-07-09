using Microsoft.AspNetCore.Mvc;
using ekaterina_anisimova_kt_42_22_April_25.Services;
using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ekaterina_anisimova_kt_42_22_April_25.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplineDto>>> GetDisciplines()
        {
            var disciplines = await _disciplineService.GetAllDisciplinesAsync();
            return Ok(disciplines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplineDto>> GetDiscipline(int id)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }
            return Ok(discipline);
        }

        [HttpPost]
        public async Task<ActionResult<DisciplineDto>> PostDiscipline([FromBody] Discipline discipline)
        {
            var addedDiscipline = await _disciplineService.AddDisciplineAsync(discipline);
            return CreatedAtAction(nameof(GetDiscipline), new { id = addedDiscipline.DisciplineId }, addedDiscipline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscipline(int id, [FromBody] Discipline discipline)
        {
            if (id != discipline.DisciplineId)
            {
                return BadRequest();
            }

            var updatedDiscipline = await _disciplineService.UpdateDisciplineAsync(discipline);
            if (updatedDiscipline == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id)
        {
            var result = await _disciplineService.DeleteDisciplineAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<DisciplineDto>>> GetFilteredDisciplines([FromQuery] DisciplineFilterDto filter)
        {
            var disciplines = await _disciplineService.GetFilteredDisciplinesAsync(filter);
            return Ok(disciplines);
        }
    }
}