using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoBasica.Api.Data;
using ProjetoAutenticacaoBasica.Api.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoBasica.Api.Dto;

namespace ProjetoAutenticacaoBasica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VooController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarVoo([FromBody] Voo voo)
        {

           var novoVoo = new Voo
           {
               Codigo = voo.Codigo,
               Origem = voo.Origem,
               Destino = voo.Destino
           };
            _context.Voos.Add(novoVoo);
            await _context.SaveChangesAsync();
            return Ok("voo criado com sucesso.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumoVooDto>>>GetVoos()
        {
            var voos = await _context.Voos.AsNoTracking()
                .Select(v => new ResumoVooDto
                {
                    Id = v.Id,
                    Codigo = v.Codigo,
                    Trajeto = v.Origem + " - " + v.Destino

                })
                .ToListAsync();
            return Ok(voos);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<VooManifestoDto>> GetVooPorCodigo(int codigo)
        {
            var voo = await _context.Voos
                .AsNoTracking()
                .Where(v => v.Codigo == codigo)
                .Select(v => new VooManifestoDto
                {
                    Id = v.Id,
                    Codigo = v.Codigo,
                    Trajeto = v.Origem + " ->" + v.Destino,
                    Passageiro = v.Passagens.Select(p => new PassageiroNoVooDto
                    {
                        Nome = p.Passageiro.Nome,
                        Assento = p.Assento
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (voo is null)
            {
                return NotFound("Voo não encontrado");
            }
            return Ok(voo);
        }

    }
}
