using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemHR.Models;

[ApiController]
[Route("api/[controller]")]
public sealed class UrlopyRestController : ControllerBase
{
    private readonly SystemHRContext _context;
    public UrlopyRestController(SystemHRContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Urlop>>> GetAll() =>
        await _context.NowyUrlop.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Urlop>> Get(int id)
    {
        var entity = await _context.NowyUrlop.FindAsync(id);
        return entity is null ? NotFound() : entity;
    }

    [HttpPost]
    public async Task<ActionResult<Urlop>> Create([FromBody] Urlop dto)
    {
        _context.NowyUrlop.Add(dto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Urlop dto)
    {
        if (id != dto.Id) return BadRequest();
        _context.Entry(dto).State = EntityState.Modified;
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
}
