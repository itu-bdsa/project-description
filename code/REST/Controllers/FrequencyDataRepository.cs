namespace REST.Controllers;

using Models;

[ApiController]
[Route("api/FrequencyDataRepository")]
public class FrequencyDataRepository : ControllerBase
{

    private readonly CommitTreeContext _context;

    public FrequencyDataRepository(CommitTreeContext context)
    {
        _context = context;
    }


    // GET: api/allFrequencyData
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FrequencyData>>> GetAllFrequencyData()
    {
        if (_context.allFrequencyData == null)
        {
            return NotFound();
        }
        return await _context.allFrequencyData.ToListAsync();
    }

    // GET: api/allFrequencyData/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FrequencyData>> GetFrequencyData(int id)
    {
        if (_context.allFrequencyData == null)
        {
            return NotFound();
        }
        var frequencyData = await _context.allFrequencyData.FindAsync(id);

        if (frequencyData == null)
        {
            return NotFound();
        }

        return frequencyData;
    }

    // PUT: api/allFrequencyData/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFrequencyData(int id, FrequencyData frequencyData)
    {
        if (id != frequencyData.Id)
        {
            return BadRequest();
        }

        _context.Entry(frequencyData).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FrequencyDataExists(id))
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

    // POST: api/allFrequencyData
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FrequencyData>> PostFrequencyData(FrequencyData frequencyData)
    {
        if (_context.allFrequencyData == null)
        {
            return Problem("Entity set 'CommitTreeContext.allFrequencyData'  is null.");
        }
        _context.allFrequencyData.Add(frequencyData);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFrequencyData), new { id = frequencyData.Id }, frequencyData);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFrequencyData(int id)
    {
        if (_context.allFrequencyData == null)
        {
            return NotFound();
        }
        var frequencyData = await _context.allFrequencyData.FindAsync(id);
        if (frequencyData == null)
        {
            return NotFound();
        }

        _context.allFrequencyData.Remove(frequencyData);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool FrequencyDataExists(int id)
    {
        return (_context.allFrequencyData?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // [Route("api/[controller]")]
    // [HttpPut]
    // public void Create (FrequencyDataCreateDTO frequencyData) {

    //     var entry = new FrequencyData(frequencyData.Date);
    //     context.allFrequencyData.Add(entry);
    //     context.SaveChanges();

    //     Console.WriteLine(frequencyData.Date + " has been created");
    // }

    // [Route("api/[controller]")]
    // [HttpGet]
    // public IReadOnlyCollection<FrequencyDataDTO> ReadAll(){
    //     return context.allFrequencyData.Select ( e => new FrequencyDataDTO(e.Id)).ToList().AsReadOnly();
    // }

    // [Route("api/[controller]")]
    // [HttpPatch]
    // public void Update(){
    //     // not implemented yet
    //     // context.Frequenices.delete
    // }

}