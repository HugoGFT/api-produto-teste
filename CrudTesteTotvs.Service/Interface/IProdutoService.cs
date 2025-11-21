using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Domain.Entities;

namespace CrudTesteTotvs.Service.Interface
{
    public interface IProdutoService : IBaseService<Produto>
    {
        Task<IEnumerable<ProdutoDto>> GetAllProdutosAsync();
        Task<ProdutoDto?> GetProdutoByIdAsync(Guid id);
        Task<ProdutoDto> CreateProdutoAsync(CreateProdutoDto createProdutoDto);
        Task<ProdutoDto?> UpdateProdutoAsync(Guid id, UpdateProdutoDto updateProdutoDto);
        Task<bool> DeleteProdutoAsync(Guid id);
    }
}
