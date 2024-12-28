using RinhaDeBackend.Entities;
using RinhaDeBackend.Repositories;

namespace RinhaDeBackend.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _repository;
        public PessoaService(IPessoaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Pessoa> AddAsync(Pessoa pessoa)
        {
            if (await _repository.ExistsByApelidoAsunc(pessoa.Apelido))
                throw new Exception("Apelido já está em uso.");

            return await _repository.AddAsync(pessoa);
        }

        public async Task<Pessoa> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }

        public async Task<IEnumerable<Pessoa>> SearchAsync(string termo)
        {
            return await _repository.SearchAsync(termo);

        }
    }
}
