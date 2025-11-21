using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Application.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetProduto(Guid id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> CreateProduto(CreateProdutoDto createProdutoDto)
        {
            var produto = await _produtoService.CreateProdutoAsync(createProdutoDto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDto>> UpdateProduto(Guid id, UpdateProdutoDto updateProdutoDto)
        {
            var produto = await _produtoService.UpdateProdutoAsync(id, updateProdutoDto);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(Guid id)
        {
            var result = await _produtoService.DeleteProdutoAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
