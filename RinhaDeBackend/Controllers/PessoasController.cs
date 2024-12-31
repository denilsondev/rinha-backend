using Microsoft.AspNetCore.Mvc;
using RinhaDeBackend.API.Model;
using RinhaDeBackend.Application.Services;
using RinhaDeBackend.Domain.Entities;

namespace RinhaDeBackend.Controllers
{
    [ApiController]
    public class PessoasController  : ControllerBase
    {

        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService service)
        {
            _pessoaService = service;
            
        }

        [HttpGet] 
        [Route("pessoas/{id:guid}")]
        public async Task<IActionResult> GetPessoa(Guid id)
        {
            var pessoa = await _pessoaService.ObterPessoaPorId(id);
            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }


        [HttpGet]
        [Route("pessoas")]
        public async Task<IActionResult> SearchPessoas([FromQuery] string t)
        {

            if (string.IsNullOrEmpty(t))
                return BadRequest("O termo de busca (t) é obrigatório.");

            var pessoas = await _pessoaService.BuscarPessoas(t);
            return Ok(pessoas);
        }

        [HttpPost]
        [Route("pessoas")]
        public async Task<IActionResult> AddPessoa([FromBody] PessoaInputModel pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _pessoaService.CriarPessoaAsync(
                    pessoa.Apelido,
                    pessoa.Nome,
                    pessoa.Nascimento,
                    pessoa.Stack
                );

                return CreatedAtAction(nameof(GetPessoa), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("contagem-pessoas")]
        public async Task<IActionResult> GetTotalCount()
        {
            var totalCount = await _pessoaService.ContarPessoaAsync();

            return Ok(totalCount.ToString());

        }



    }
}
