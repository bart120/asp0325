using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocaSub.Data;
using DocaSub.Models;

namespace DocaSub.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubRequestsController : ControllerBase
    {
        private readonly DocaDbContext _context;

        public SubRequestsController(DocaDbContext context)
        {
            _context = context;
        }

        // GET: api/SubRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubRequest>>> GetSubRequests()
        {
            return await _context.SubRequests.ToListAsync();
        }

        // GET: api/SubRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubRequest>> GetSubRequest(int id)
        {
            var subRequest = await _context.SubRequests.Include("Subvention").SingleOrDefaultAsync(x => x.Id == id);
            //subRequest.Subvention = await _context.Subventions.FindAsync(subRequest.SubventionId);

            if (subRequest == null)
            {
                return NotFound();
            }

            return subRequest;
        }

        // PUT: api/SubRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubRequest(int id, SubRequest subRequest)
        {
            if (id != subRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(subRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubRequestExists(id))
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

        // POST: api/SubRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubRequest>> PostSubRequest(SubRequest subRequest)
        {
            _context.SubRequests.Add(subRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubRequest", new { id = subRequest.Id }, subRequest);
        }

        // DELETE: api/SubRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubRequest(int id)
        {
            var subRequest = await _context.SubRequests.FindAsync(id);
            if (subRequest == null)
            {
                return NotFound();
            }

            _context.SubRequests.Remove(subRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubRequestExists(int id)
        {
            return _context.SubRequests.Any(e => e.Id == id);
        }
    }
}
