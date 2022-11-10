using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GitInsight.Core;
using GitInsight.Entities;

namespace GitInsight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly GitInsightContext _context;

        public ContributionController(GitInsightContext context)
        {
            _context = context;
        }

        // GET: api/WebApiItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionDTO>>> GetContribution()
        {
            return await _context.Contribution
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/WebApiItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributionDTO>> GetContribution(long id)
        {
            var contribution = await _context.Contribution.FindAsync(id);

            if (contribution == null)
            {
                return NotFound();
            }

            return ItemToDTO(contribution);
        }
        // PUT: api/WebApiItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContribution(long id, ContributionDTO contributionDTO)
        {
            if (id != contributionDTO.Id)
            {
                return BadRequest();
            }

            var contribution = await _context.Contribution.FindAsync(id);
            if (contribution == null)
            {
                return NotFound();
            }

            contribution.Name = contributionDTO.Author;
            contribution.IsComplete = contributionDTO.Date;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!WebApiItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/WebApiItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WebApiItemDTO>> CreateWebApiItem(WebApiItemDTO WebApiItemDTO)
        {
            var WebApiItem = new WebApiItem
            {
                IsComplete = WebApiItemDTO.IsComplete,
                Name = WebApiItemDTO.Name
            };

            _context.WebApiItems.Add(WebApiItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetWebApiItem),
                new { id = WebApiItem.Id },
                ItemToDTO(WebApiItem));
        }

        // DELETE: api/WebApiItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebApiItem(long id)
        {
            var WebApiItem = await _context.WebApiItems.FindAsync(id);

            if (WebApiItem == null)
            {
                return NotFound();
            }

            _context.WebApiItems.Remove(WebApiItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebApiItemExists(long id)
        {
            return _context.WebApiItems.Any(e => e.Id == id);
        }

        private static WebApiItemDTO ItemToDTO(WebApiItem WebApiItem) =>
            new WebApiItemDTO
            {
                Id = WebApiItem.Id,
                Name = WebApiItem.Name,
                IsComplete = WebApiItem.IsComplete
            };
    }
}