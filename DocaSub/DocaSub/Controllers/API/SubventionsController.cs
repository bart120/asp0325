using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocaSub.Data;
using DocaSub.Models;
using System.Linq.Expressions;
using Asp.Versioning;

namespace DocaSub.Controllers.API
{
   
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubventionsController : ControllerBase
    {
        private readonly DocaDbContext _context;

        public SubventionsController(DocaDbContext context)
        {
            _context = context;
        }

        // GET: api/Subventions
        [HttpGet]
        [Route("", Name = "GetSubventionsV1")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Subvention>>> GetSubventionsV1()
        {
            return await _context.Subventions.ToListAsync();
        }

        

        // GET: api/Subventions
        [HttpGet]
        [Route("", Name = "GetSubventionsV2")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<Subvention>>> GetSubventionsV2(/*bool ordered = false*/)
        {
            /*if (ordered)
            {*/
                return await _context.Subventions.OrderBy(x => x.Name).ToListAsync();
            /*}
            return await _context.Subventions.ToListAsync();*/
        }

        // GET: api/Subventions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Subvention>> GetSubvention(int id)
        {
            var subvention = await _context.Subventions.Include("SubRequests").SingleOrDefaultAsync(x => x.Id == id);

            if (subvention == null)
            {
                return NotFound();
            }

            return Ok(subvention);
        }

        [HttpGet("subrequests")]
        public async Task<ActionResult<IEnumerable<Subvention>>> GetSubRequests()
        {
            return await _context.Subventions.Include("SubRequests").ToListAsync();
        }

        // GET: api/Subventions
        [HttpGet("orderby/{field}")]
        public async Task<ActionResult<IEnumerable<Subvention>>> GetOrderedSubventions(string field = "Name")
        {
            if(typeof(Subvention).GetProperty(field) == null)
            {
                return BadRequest("Invalid field name");
            }

            var parameter = Expression.Parameter(typeof(Subvention), "x");
            var property = Expression.Property(parameter, field);
            var type = Expression.Convert(property, typeof(object));

            var lambda = Expression.Lambda<Func<Subvention, object>>(type, parameter);


            //return await _context.Subventions.OrderBy(x => x.Name).ToListAsync();
            return await _context.Subventions.OrderBy(lambda).ToListAsync();
        }

        // PUT: api/Subventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubvention([FromQuery]int id, [FromBody] Subvention subvention)
        {
            if (id != subvention.Id)
            {
                return BadRequest();
            }

            _context.Entry(subvention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subventions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subvention>> PostSubvention([FromBody]Subvention subvention)
        {
            if(subvention.End != null && subvention.Start > subvention.End)
            {
                return BadRequest("End date must be greater than or equal to start date");
            }

            _context.Subventions.Add(subvention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubvention", new { id = subvention.Id }, subvention);
        }

        // DELETE: api/Subventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubvention(int id)
        {
            //var subvention = await _context.Subventions.SingleOrDefaultAsync(x => x.Id == id);
            var subvention = await _context.Subventions.FindAsync(id);
            //subvention.Name = "bob";
            //_context.Update(subvention);//_context.Entry(subvention).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            if (subvention == null)
            {
                return NotFound();
            }
            //_context.Entry(subvention).State = EntityState.Deleted;
            _context.Subventions.Remove(subvention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubventionExists(int id)
        {
            return _context.Subventions.Any(e => e.Id == id);
        }
    }
}
