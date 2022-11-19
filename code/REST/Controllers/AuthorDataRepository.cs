namespace REST.Controllers;

using Models;

[ApiController]
[Route("api/AuthorDataRepository")]
public class AuthorDataRepository : ControllerBase

// OBS: SHOULD IMPLEMENT IAuthorDataRepository

{

    private readonly CommitTreeContext _context;

    public AuthorDataRepository(CommitTreeContext context)
    {
        _context = context;
    }

    [HttpPut (Name = "AD_Create")]
    public void Create(AuthorDataCreateDTO author)
    {

        var entry = new AuthorData(author.Name, DateTime.ParseExact("01/01/0001", "dd/mm/yyyy", null));
        _context.allAuthorData.Add(entry);
        _context.SaveChanges();

        Console.WriteLine(author.Name + " has been created");
    }

    [HttpGet (Name = "AD_ReadAll")]
    public IReadOnlyCollection<AuthorDataDTO> ReadAll()
    {
        var authors = _context.allAuthorData.Select(e => new AuthorDataDTO(e.Id, e.Name!));

        return authors.ToList().AsReadOnly();

    }

    [HttpPatch (Name = "AD_Update")]
    public void Update()
    {
        // not implemented yet
        // context.Authors.delete
    }

    [HttpDelete ("{Date}", Name = "AD_DeleteByDate")]
    public void DeleteSpecificAuth(DateTime Date)
    {
        var authorToRemove = (
            from a in _context.allAuthorData
            where a.Date == Date
            select a
        ).FirstOrDefault();
        if (authorToRemove != null) {
            _context.allAuthorData.Remove(authorToRemove);
            _context.SaveChanges();
            Console.WriteLine(authorToRemove + " has been deleted");
        }
    }

    [HttpDelete (Name = "AD_DeleteAll")]
    public void DeleteAll() {
        foreach (var auth in _context.allAuthorData)
        {
            _context.allAuthorData.Remove(auth);
            _context.SaveChanges();
        }
    }




    /*


        ALT HERUNDER ER AUTO GENERERET


    */

    // GET: api/allAuthorData
    // [HttpGet(Name = "getallauthordata")]
    // public async Task<ActionResult<IEnumerable<AuthorData>>> GetAllAuthorData()
    // {
    //     if (_context.allAuthorData == null)
    //     {
    //         return NotFound();
    //     }
    //     return await _context.allAuthorData.ToListAsync();
    // }

    // // GET: api/allAuthorData/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<AuthorData>> GetAuthorData(int id)
    // {
    //     if (_context.allAuthorData == null)
    //     {
    //         return NotFound();
    //     }
    //     var authorData = await _context.allAuthorData.FindAsync(id);

    //     if (authorData == null)
    //     {
    //         return NotFound();
    //     }

    //     return authorData;
    // }

    // // PUT: api/allAuthorData/5
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutAuthorData(int id, AuthorData authorData)
    // {
    //     if (id != authorData.Id)
    //     {
    //         return BadRequest();
    //     }

    //     _context.Entry(authorData).State = EntityState.Modified;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException)
    //     {
    //         if (!AuthorDataExists(id))
    //         {
    //             return NotFound();
    //         }
    //         else
    //         {
    //             throw;
    //         }
    //     }

    //     return NoContent();
    // }

    // // POST: api/allAuthorData
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPost (Name = "postauthordata")]
    // public async Task<ActionResult<AuthorData>> PostAuthorData(AuthorData authorData)
    // {
    //     if (_context.allAuthorData == null)
    //     {
    //         return Problem("Entity set 'CommitTreeContext.allAuthorData'  is null.");
    //     }

    //     _context.allAuthorData.Add(authorData);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction(nameof(GetAuthorData), new { id = authorData.Id }, authorData);
    // }

    // // DELETE: api/TodoItems/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteAuthorData(int id)
    // {
    //     if (_context.allAuthorData == null)
    //     {
    //         return NotFound();
    //     }
    //     var authorData = await _context.allAuthorData.FindAsync(id);
    //     if (authorData == null)
    //     {
    //         return NotFound();
    //     }

    //     _context.allAuthorData.Remove(authorData);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }


    // private bool AuthorDataExists(int id)
    // {
    //     return (_context.allAuthorData?.Any(e => e.Id == id)).GetValueOrDefault();
    // }






    // // [Route("api/[controller]")]
    // // [HttpPut]
    // // public void Create (AuthorDataCreateDTO authorData) {

    // //     var entry = new AuthorData(authorData.Name);
    // //     _context.allAuthorData.Add(entry);
    // //     _context.SaveChanges();

    // //     Console.WriteLine(authorData.Name + " has been created");
    // // }

    // // [Route("api/[controller]")]
    // // [HttpGet]
    // // public IReadOnlyCollection<AuthorDataDTO> ReadAll(){
    // //     return _context.allAuthorData.Select ( e => new AuthorDataDTO(e.Id, e.Name )).ToList().AsReadOnly();
    // // }

    // // [Route("api/[controller]")]
    // // [HttpPatch]
    // // public void Update(){
    // //     // not implemented yet
    // //     // context.Authors.delete
    // // }

}