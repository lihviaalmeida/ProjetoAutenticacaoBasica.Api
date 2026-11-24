using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoBasica.Api.Data;
using ProjetoAutenticacaoBasica.Api.Dto;
using ProjetoAutenticacaoBasica.Api.Models;


namespace ProjetoAutenticacaoBasica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageiroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassageiroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPassageiro([FromBody] CriarPassageiroDtos passageiro)
        {
            var novoPassageiro = new Passageiro
            {
                Nome = passageiro.Nome,
                CPF = passageiro.CPF
            };
            _context.Passageiros.Add(novoPassageiro);
            await _context.SaveChangesAsync();
            return Ok(new { mensagem = "Passageiro criado com sucesso." });

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassageiroInfoDto>>> GetPassageiros([FromQuery] string? nome)
        {
            var passageiros = await _context.Passageiros
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(nome) || p.Nome.Contains(nome) || p.Nome.Contains(nome))
                .OrderBy(p => p.Nome)
                .Select(p => new PassageiroInfoDto
                {
                   Id = p.Id,
                   Nome = p.Nome
                })
                .ToListAsync();
            return Ok(passageiros);

        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PassageiroInfoDto>> GetPassageiroPorCpf(string cpf)
        {
            var passageiro = await _context.Passageiros
                .AsNoTracking()
                .Where(p => p.CPF == cpf)
                .Select(p => new PassageiroInfoDto
                {
                    Id = p.Id,
                    Nome = p.Nome
                })
                .FirstOrDefaultAsync();
            if (passageiro is null)
            {
                return NotFound(new { imagem = "passageiro não encontrado" });
            }
            return Ok(passageiro);
        }
    }
}
