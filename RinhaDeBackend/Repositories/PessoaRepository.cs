using Microsoft.EntityFrameworkCore;
using RinhaDeBackend.Data;
using RinhaDeBackend.Entities;

namespace RinhaDeBackend.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;
        public PessoaRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<Pessoa> AddAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
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
            return await _context.Pessoas.
                Where(p =>
                (!string.IsNullOrEmpty(p.Apelido) && EF.Functions.ILike(p.Apelido, $"%{termo}%")) ||
                (!string.IsNullOrEmpty(p.Nome) && EF.Functions.ILike(p.Nome, $"%{termo}%")) ||
                (p.Stack != null && p.Stack.Any(stack => EF.Functions.ILike(stack, $"%{termo}%")))
                )
                .Take(50)
                .ToListAsync();
        }

        
    }
}
