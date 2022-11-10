using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiItemsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public WebApiItemsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/WebApiItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebApiItemDTO>>> GetWebApiItems()
        {
            return await _context.WebApiItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/WebApiItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebApiItemDTO>> GetWebApiItem(long id)
        {
            var WebApiItem = await _context.WebApiItems.FindAsync(id);

            if (WebApiItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(WebApiItem);
        }
        // PUT: api/WebApiItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWebApiItem(long id, WebApiItemDTO WebApiItemDTO)
        {
            if (id != WebApiItemDTO.Id)
            {
                return BadRequest();
            }

            var WebApiItem = await _context.WebApiItems.FindAsync(id);
            if (WebApiItem == null)
            {
                return NotFound();
            }

            WebApiItem.Name = WebApiItemDTO.Name;
            WebApiItem.IsComplete = WebApiItemDTO.IsComplete;

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