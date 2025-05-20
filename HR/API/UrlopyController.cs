using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemHR.Models;

namespace SystemHR.ApiControllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UrlopyController : ControllerBase
{
    private readonly SystemHRContext _context;
    public UrlopyController(SystemHRContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListaUrlopow>>> GetAll() =>
        await _context.Urlop.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ListaUrlopow>> Get(int id)
    {
        var entity = await _context.Urlop.AsNoTracking()
                                         .FirstOrDefaultAsync(e => e.Id == id);
        return entity is null ? NotFound() : entity;
    }

    [HttpPost]
    public async Task<ActionResult<Urlop>> Create([FromBody] Urlop model)
    {
        _context.NowyUrlop.Add(model);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            return Conflict();
        }
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Urlop model)
    {
        if (id != model.Id) return BadRequest();
        var entity = await _context.NowyUrlop.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Nr_identyfikacyjny = model.Nr_identyfikacyjny;
        entity.nazwa_wolnego = model.nazwa_wolnego;
        entity.dane_wolnego = model.dane_wolnego;
        entity.Identyfikator = model.Identyfikator;
        entity.Kategoria = model.Kategoria;
        entity.Dostepne = model.Dostepne;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.NowyUrlop.FindAsync(id);
        if (entity is null) return NotFound();

        _context.NowyUrlop.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("return/{wolneId:int}")]
    public async Task<IActionResult> ConfirmReturn(int wolneId)
    {
        var usage = await _context.Wolne
            .FirstOrDefaultAsync(w => w.Id == wolneId && w.Koniec_Wolnego == null);
        if (usage is null) return NotFound();

        usage.Koniec_Wolnego = DateTime.UtcNow;

        var leave = await _context.NowyUrlop.FindAsync(usage.Id_Urlop);
        if (leave is not null) leave.Dostepne = true;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
