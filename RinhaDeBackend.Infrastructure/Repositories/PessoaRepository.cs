using Microsoft.EntityFrameworkCore;
using RinhaDeBackend.Domain.Entities;
using RinhaDeBackend.Domain.Repositories;
using RinhaDeBackend.Infrastructure.Data;

namespace RinhaDeBackend.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;
        public PessoaRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task AddAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByApelidoAsunc(string apelido)
        {
            return await _context.Pessoas.AnyAsync(p => p.Apelido == apelido);
        }

        public async Task<Pessoa?> GetByIdAsync(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Pessoas.CountAsync();
        }

        public async Task<IEnumerable<Pessoa>> SearchAsync(string termo)
        {
            return await _context.Pessoas
                                 .Where(p => p.Apelido.Contains(termo) || p.Nome.Contains(termo) || p.Stack.Any(p => p.Nome.Contains(termo)))
                                 .Include(p => p.Stack)
                                 .ToListAsync();
        }


    }
}
