using Microsoft.AspNetCore.Mvc;
using ekaterina_anisimova_kt_42_22_April_25.Services;
using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ekaterina_anisimova_kt_42_22_April_25.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadsController : ControllerBase
    {
        private readonly ILoadService _loadService;

        public LoadsController(ILoadService loadService)
        {
            _loadService = loadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoadDto>>> GetLoads()
        {
            var loads = await _loadService.GetAllLoadsAsync();
            return Ok(loads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoadDto>> GetLoad(int id)
        {
            var load = await _loadService.GetLoadByIdAsync(id);
            if (load == null)
            {
                return NotFound();
            }
            return Ok(load);
        }

        [HttpPost]
        public async Task<ActionResult<LoadDto>> PostLoad([FromBody] Load load)
        {
            try
            {
                var addedLoad = await _loadService.AddLoadAsync(load);
                return CreatedAtAction(nameof(GetLoad), new { id = addedLoad.LoadId }, addedLoad);
            }
            catch (ArgumentException ex) // Перехватываем ошибку, если TeacherId или DisciplineId не найдены
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoad(int id, [FromBody] Load load)
        {
            if (id != load.LoadId)
            {
                return BadRequest();
            }

            try
            {
                var updatedLoad = await _loadService.UpdateLoadAsync(load);
                if (updatedLoad == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex) // Перехватываем ошибку, если TeacherId или DisciplineId не найдены
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoad(int id)
        {
            var result = await _loadService.DeleteLoadAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("filtered")] 
        public async Task<ActionResult<IEnumerable<LoadDto>>> GetFilteredLoads([FromQuery] LoadFilterDto filter)
        {
            var loads = await _loadService.GetFilteredLoadsAsync(filter);
            return Ok(loads);
        }
    }
}