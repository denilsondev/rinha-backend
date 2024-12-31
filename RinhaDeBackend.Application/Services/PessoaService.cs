using RinhaDeBackend.Domain.Entities;
using RinhaDeBackend.Domain.Repositories;
using RinhaDeBackend.Domain.ValueObjects;

namespace RinhaDeBackend.Application.Services
{
    public class PessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
            
        }

        public async Task<Guid> CriarPessoaAsync(string apelido, string nome, DateTime nascimento, List<string> stack)
        {
            var stackObjects = stack.Select(s => new Stack(s)).ToList();

            var pessoa = new Pessoa(apelido, nome, nascimento, stackObjects);

            await _pessoaRepository.AddAsync(pessoa);

            return pessoa.Id;

        }


        public async Task<IEnumerable<Pessoa>> BuscarPessoas(string termo)
        {
            return await _pessoaRepository.SearchAsync(termo);
        }

        public async Task<Pessoa?> ObterPessoaPorId(Guid id)
        {
            return await _pessoaRepository.GetByIdAsync(id);
        }

        public async Task<int> ContarPessoaAsync()
        {
            return await _pessoaRepository.GetTotalCountAsync();
        }








    }
}
