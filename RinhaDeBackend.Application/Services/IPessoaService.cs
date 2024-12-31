using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RinhaDeBackend.Domain.Entities;
using RinhaDeBackend.Domain.Repositories;

namespace RinhaDeBackend.Application.Services
{
    public interface IPessoaService
    {
        Task<Guid> CriarPessoaAsync(string apelido, string nome, DateTime nascimento, List<string> stack);
        Task<IEnumerable<Pessoa>> BuscarPessoas(string termo);
        Task<Pessoa?> ObterPessoaPorId(Guid id);
        Task<int> ContarPessoaAsync();
    }
}
