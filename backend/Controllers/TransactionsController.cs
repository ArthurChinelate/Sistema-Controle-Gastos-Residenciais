// Controllers/TransactionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/transactions
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        // Include traz os dados da pessoa relacionada para exibição na listagem
        var transactions = await _context.Transactions.Include(t => t.Person).ToListAsync();
        return Ok(transactions);
    }

    // POST: api/transactions
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(Transaction transaction)
    {
        // REGRA DE NEGÓCIO: Validar se a pessoa existe
        var person = await _context.People.FindAsync(transaction.PersonId);
        if (person == null)
            return BadRequest("Pessoa não encontrada.");

        // REGRA DE NEGÓCIO: Menores de 18 anos só podem cadastrar despesas
        if (person.Age < 18 && transaction.Type == TransactionType.Receita)
        {
            return BadRequest("Menores de 18 anos só podem registrar despesas.");
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return Ok(transaction);
    }
}