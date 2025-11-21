using CrudTesteTotvs.Data;
using CrudTesteTotvs.Domain.Entities;
using CrudTesteTotvs.Infra.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Infra.Repository
{
    [ExcludeFromCodeCoverage]
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos
                .Where(p => p.IsActive)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(Guid id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            produto.DataAtualizacao = DateTime.UtcNow;
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var produto = await GetByIdAsync(id);
            if (produto == null) return false;

            produto.IsActive = false;
            produto.DataAtualizacao = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Id == id && p.IsActive);
        }
    }
}


