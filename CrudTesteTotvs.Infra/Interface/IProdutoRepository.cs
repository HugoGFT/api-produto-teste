using CrudTesteTotvs.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Infra.Interface
{
    public interface IProdutoRepository
    {
        [ExcludeFromCodeCoverage] 
        Task<IEnumerable<Produto>> GetAllAsync();
        
        [ExcludeFromCodeCoverage] 
        Task<Produto?> GetByIdAsync(Guid id);
        
        [ExcludeFromCodeCoverage] 
        Task<Produto> CreateAsync(Produto produto);

        [ExcludeFromCodeCoverage] 
        Task<Produto> UpdateAsync(Produto produto);

        [ExcludeFromCodeCoverage] 
        Task<bool> DeleteAsync(Guid id);
        
        [ExcludeFromCodeCoverage] 
        Task<bool> ExistsAsync(Guid id);
    }
}
