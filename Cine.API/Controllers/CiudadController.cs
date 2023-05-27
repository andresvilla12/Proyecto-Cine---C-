using Cine.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cine.Shared.Entities;
using Cine.Shared.DTOs;
using Cine.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Cine.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/ciudad")]
    public class CiudadController : ControllerBase
    {
        private readonly DataContext _context;

        public CiudadController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ciudad
                .Where(x => x.Colombia!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }


            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ciudad
                .Where(x => x.Colombia!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }


            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ciudad = await _context.Ciudad
                
                .FirstOrDefaultAsync(x => x.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return Ok(ciudad);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Ciudad ciudad)
        {
            try
            {
                _context.Add(ciudad);
                await _context.SaveChangesAsync();
                return Ok(ciudad);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Ciudad ciudad)
        {
            try
            {
                _context.Update(ciudad);
                await _context.SaveChangesAsync();
                return Ok(ciudad);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var ciudad = await _context.Ciudad.FirstOrDefaultAsync(x => x.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.Remove(ciudad);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("combo/{colombiaId:int}")]
        public async Task<ActionResult> GetCombo(int colombiaId)
        {
            return Ok(await _context.Ciudad
                .Where(x => x.ColombiaId == colombiaId)
                .ToListAsync());
        }

    }
}

