using System;
using RinhaDeBackend.Entities;

namespace RinhaDeBackend.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> SearchAsync(string termo);
        Task<Pessoa> GetByIdAsync(Guid id);
        Task<Pessoa> AddAsync(Pessoa pessoa);
        Task<int> GetTotalCountAsync();
    }
}
