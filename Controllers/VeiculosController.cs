using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MfApisWebServicesFuelManager.Models;

namespace MfApisWebServicesFuelManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VeiculosController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/veiculos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Veiculos.Include(v => v.Consumos).ToListAsync();
            return Ok(list);
        }

        // GET: api/veiculos/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var v = await _db.Veiculos.Include(x => x.Consumos).FirstOrDefaultAsync(x => x.Id == id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        // POST: api/veiculos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Veiculo model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Veiculos.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        // PUT: api/veiculos/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Veiculo model)
        {
            if (id != model.Id) return BadRequest("Id mismatch");
            var exists = await _db.Veiculos.AnyAsync(x => x.Id == id);
            if (!exists) return NotFound();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/veiculos/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var v = await _db.Veiculos.FindAsync(id);
            if (v == null) return NotFound();
            _db.Veiculos.Remove(v);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

