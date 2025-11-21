using AutoMapper;
using CrudTesteTotvs.Data.Validators;
using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Domain.Entities;
using CrudTesteTotvs.Infra.Interface;
using CrudTesteTotvs.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace CrudTesteTotvs.Service.Services
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllProdutosAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto?> GetProdutoByIdAsync(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return produto == null ? null : _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<ProdutoDto> CreateProdutoAsync(CreateProdutoDto createProdutoDto)
        {
            
            var produto = _mapper.Map<Produto>(createProdutoDto);
            Validate(produto, Activator.CreateInstance<ProdutoValidator>());
            var createdProduto = await _produtoRepository.CreateAsync(produto);
            return _mapper.Map<ProdutoDto>(createdProduto);
        }

        public async Task<ProdutoDto?> UpdateProdutoAsync(Guid id, UpdateProdutoDto updateProdutoDto)
        {
            var existingProduto = await _produtoRepository.GetByIdAsync(id);
            if (existingProduto == null) return null;

            _mapper.Map(updateProdutoDto, existingProduto);
            Validate(existingProduto, Activator.CreateInstance<ProdutoValidator>());
            var updatedProduto = await _produtoRepository.UpdateAsync(existingProduto);
            return _mapper.Map<ProdutoDto>(updatedProduto);
        }

        public async Task<bool> DeleteProdutoAsync(Guid id)
        {
            return await _produtoRepository.DeleteAsync(id);
        }
    }
}
