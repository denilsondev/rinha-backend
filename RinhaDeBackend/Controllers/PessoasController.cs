using Microsoft.AspNetCore.Mvc;
using RinhaDeBackend.Entities;
using RinhaDeBackend.Services;

namespace RinhaDeBackend.Controllers
{
    [ApiController]
    public class PessoasController  : ControllerBase
    {

        private readonly IPessoaService _service;

        public PessoasController(IPessoaService service)
        {
            _service = service;
            
        }

        [HttpGet] 
        [Route("pessoas/{id:guid}")]
        public async Task<IActionResult> GetPessoa(Guid id)
        {
            var pessoa = await _service.GetByIdAsync(id);
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

            var pessoas = await _service.SearchAsync(t);
            return Ok(pessoas);
        }

        [HttpPost]
        [Route("pessoas")]
        public async Task<IActionResult> AddPessoa([FromBody] Pessoa pessoa)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validação adicional para Stack
            if (pessoa.Stack == null || !pessoa.Stack.Any())
            {
                return BadRequest(new { error = "O campo 'Stack' é obrigatório e não pode ser vazio." });
            }


            if (pessoa.Stack != null && pessoa.Stack.Any(s => s.Length > 32))
            {
                return BadRequest(new { error = "Cada item do Stack deve ter no máximo 32 caracteres." });
            }


            try
            {
                var novaPessoa = await _service.AddAsync(pessoa);

                return CreatedAtAction(nameof(GetPessoa), new { id = novaPessoa.Id }, novaPessoa);

            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new { error = ex.Message });
            }
            
        }

        [HttpGet]
        [Route("contagem-pessoas")]
        public async Task<IActionResult> GetTotalCount()
        {
            var totalCount = await _service.GetTotalCountAsync();

            return Ok(totalCount.ToString());

        }



    }
}
