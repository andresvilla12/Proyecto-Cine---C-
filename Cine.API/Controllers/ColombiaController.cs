using Cine.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cine.Shared.Entities;
using System.Diagnostics.Metrics;
using Cine.Shared.DTOs;
using Cine.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Cine.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/colombia")]
    public class ColombiaController : ControllerBase
    {
        private readonly DataContext _context;

        public ColombiaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Colombia
                .Include(x => x.Ciudad)
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
            var queryable = _context.Colombia.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }


            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var colombia = await _context.Colombia

              .Include(x => x.Ciudad!)

                .FirstOrDefaultAsync(x => x.Id == id);
            if (colombia is null)
            {
                return NotFound();
            }

            return Ok(colombia);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Colombia colombia)
        {
            _context.Add(colombia);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(colombia);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un departamento con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.Colombia
                .Include(x => x.Ciudad!)
                .ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult> Put(Colombia colombia)
        {
            _context.Update(colombia);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(colombia);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un departamento con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Colombia
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("Combo")]
        public async Task<ActionResult<List<string>>> GetCombo()
        {
            // return Ok(await _context.Colombia.ToListAsync()); // <-- COMENTAR ESTA LÍNEA

            // Devolver una lista fija (Mock data) para que la API funcione
            var listaFija = new List<string> { "Dato CI/CD 1", "Dato CI/CD 2" };
            return Ok(listaFija); // <-- USAR ESTA LÍNEA
        }


    }
}
