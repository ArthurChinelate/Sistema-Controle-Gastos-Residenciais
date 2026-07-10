// Controllers/TotalsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TotalsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TotalsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/totals
    [HttpGet]
    public async Task<IActionResult> GetTotals()
    {
        // LÓGICA DE TOTAIS: Agrupa os dados e calcula receita, despesa e saldo no banco.
        var peopleTotals = await _context.People
            .Select(p => new
            {
                p.Id,
                p.Name,
                TotalReceitas = p.Transactions.Where(t => t.Type == TransactionType.Receita).Sum(t => t.Value),
                TotalDespesas = p.Transactions.Where(t => t.Type == TransactionType.Despesa).Sum(t => t.Value),
                Saldo = p.Transactions.Where(t => t.Type == TransactionType.Receita).Sum(t => t.Value) - 
                        p.Transactions.Where(t => t.Type == TransactionType.Despesa).Sum(t => t.Value)
            })
            .ToListAsync();

        var generalTotalReceitas = peopleTotals.Sum(p => p.TotalReceitas);
        var generalTotalDespesas = peopleTotals.Sum(p => p.TotalDespesas);
        var generalSaldo = generalTotalReceitas - generalTotalDespesas;

        // Retorna o consolidado por pessoa e o total geral
        return Ok(new
        {
            People = peopleTotals,
            GeneralTotal = new
            {
                TotalReceitas = generalTotalReceitas,
                TotalDespesas = generalTotalDespesas,
                Saldo = generalSaldo
            }
        });
    }
}