using Cine.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cine.Shared.Entities;
using Cine.Shared.DTOs;
using Cine.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Stores.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/funciones")]
    public class FuncionesController : ControllerBase
    {
        private readonly DataContext _context;

        public FuncionesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Funcion
                .Where(x => x.Pelicula!.Id == pagination.Id)
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
            var queryable = _context.Funcion
                .Where(x => x.Pelicula!.Id == pagination.Id)
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
            var funcion = await _context.Funcion

                .FirstOrDefaultAsync(x => x.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return Ok(funcion);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Funcion funcion)
        {
            try
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return Ok(funcion);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una función con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Funcion funcion)
        {
            try
            {
                _context.Update(funcion);
                await _context.SaveChangesAsync();
                return Ok(funcion);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una función con el mismo nombre.");
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
            var funcion = await _context.Funcion.FirstOrDefaultAsync(x => x.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            _context.Remove(funcion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}