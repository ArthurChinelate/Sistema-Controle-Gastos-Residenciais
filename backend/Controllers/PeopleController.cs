// Controllers/PeopleController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeopleController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/people - Listagem de pessoas
    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        var people = await _context.People.ToListAsync();
        return Ok(people);
    }

    // POST: api/people - Criação de pessoa
    [HttpPost]
    public async Task<IActionResult> CreatePerson(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
    }

    // DELETE: api/people/{id} - Deleção (as transações serão deletadas em cascata pelo EF Core)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound();

        _context.People.Remove(person);
        await _context.SaveChangesAsync(); // Deleta a pessoa e, por consequência, suas transações.
        
        return NoContent();
    }
}