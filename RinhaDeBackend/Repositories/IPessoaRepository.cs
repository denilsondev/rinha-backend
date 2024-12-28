﻿using System.Collections;
using RinhaDeBackend.Entities;

namespace RinhaDeBackend.Repositories
{
    public interface IPessoaRepository
    {
        Task<Pessoa> GetByIdAsync(Guid id);
        Task<Pessoa> AddAsync(Pessoa pessoa);
        Task<IEnumerable<Pessoa>> SearchAsync(string termo);
        Task<bool> ExistsByApelidoAsunc(string apelido);
        Task<int> GetTotalCountAsync();


    }
}
