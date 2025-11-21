using AutoMapper;
using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Domain.Entities;
using CrudTesteTotvs.Infra.Interface;
using CrudTesteTotvs.Service.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTesteTotvs.Test.Service
{
    public class ProdutoServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly ProdutoService _service;

        public ProdutoServiceTests()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<ProdutoService>();
        }

        [Fact]
        public async Task GetAllProdutosAsync_DeveRetornarListaDeProdutosDto()
        {
            // Arrange
            var produtos = new List<Produto>
                {
                    new Produto { Id = Guid.NewGuid(), Nome = "Produto 1", Descricao = "Desc 1", Preco = 10, Estoque = 5, Categoria = "Cat 1", DataCriacao = DateTime.Now, DataAtualizacao = DateTime.Now, IsActive = true },
                    new Produto { Id = Guid.NewGuid(), Nome = "Produto 2", Descricao = "Desc 2", Preco = 20, Estoque = 10, Categoria = "Cat 2", DataCriacao = DateTime.Now, DataAtualizacao = DateTime.Now, IsActive = true }
                };
            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(produtos);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<IEnumerable<ProdutoDto>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns(new List<ProdutoDto>
                {
                        new ProdutoDto { Id = produtos[0].Id, Nome = produtos[0].Nome },
                        new ProdutoDto { Id = produtos[1].Id, Nome = produtos[1].Nome }
                });

            // Act
            var result = await _service.GetAllProdutosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<ProdutoDto>)result).Count);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_DeveRetornarProdutoDto_QuandoEncontrado()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var produto = new Produto { Id = produtoId, Nome = "Produto", Descricao = "Desc", Preco = 10, Estoque = 5, Categoria = "Cat", DataCriacao = DateTime.Now, DataAtualizacao = DateTime.Now, IsActive = true };
            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.GetByIdAsync(produtoId))
                .ReturnsAsync(produto);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<ProdutoDto>(produto))
                .Returns(new ProdutoDto { Id = produtoId, Nome = produto.Nome });

            // Act
            var result = await _service.GetProdutoByIdAsync(produtoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoId, result.Id);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.GetByIdAsync(produtoId))
                .ReturnsAsync((Produto)null);

            // Act
            var result = await _service.GetProdutoByIdAsync(produtoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateProdutoAsync_DeveRetornarProdutoDtoCriado()
        {
            // Arrange
            var createDto = new CreateProdutoDto { Nome = "Novo", Descricao = "Desc", Preco = 15, Estoque = 3, Categoria = "Cat" };
            var produto = new Produto { Id = Guid.NewGuid(), Nome = createDto.Nome, Descricao = createDto.Descricao, Preco = createDto.Preco, Estoque = createDto.Estoque, Categoria = createDto.Categoria, DataCriacao = DateTime.Now, DataAtualizacao = DateTime.Now, IsActive = true };
            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Produto>(createDto))
                .Returns(produto);

            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.CreateAsync(produto))
                .ReturnsAsync(produto);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<ProdutoDto>(produto))
                .Returns(new ProdutoDto { Id = produto.Id, Nome = produto.Nome });

            // Act
            var result = await _service.CreateProdutoAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Nome, result.Nome);
        }

        [Fact]
        public async Task UpdateProdutoAsync_DeveRetornarProdutoDtoAtualizado_QuandoEncontrado()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var updateDto = new UpdateProdutoDto { Nome = "Atualizado", Descricao = "Desc", Preco = 20, Estoque = 7, Categoria = "Cat" };
            var produto = new Produto { Id = produtoId, Nome = "Antigo", Descricao = "Desc", Preco = 10, Estoque = 5, Categoria = "Cat", DataCriacao = DateTime.Now, DataAtualizacao = DateTime.Now, IsActive = true };

            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.GetByIdAsync(produtoId))
                .ReturnsAsync(produto);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map(updateDto, produto))
                .Callback(() => produto.Nome = updateDto.Nome);

            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.UpdateAsync(produto))
                .ReturnsAsync(produto);

            _mocker.GetMock<IMapper>()
                .Setup(m => m.Map<ProdutoDto>(produto))
                .Returns(new ProdutoDto { Id = produtoId, Nome = updateDto.Nome });

            // Act
            var result = await _service.UpdateProdutoAsync(produtoId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Nome, result.Nome);
        }

        [Fact]
        public async Task UpdateProdutoAsync_DeveRetornarNull_QuandoProdutoNaoEncontrado()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var updateDto = new UpdateProdutoDto { Nome = "Atualizado", Descricao = "Desc", Preco = 20, Estoque = 7, Categoria = "Cat" };

            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.GetByIdAsync(produtoId))
                .ReturnsAsync((Produto)null);

            // Act
            var result = await _service.UpdateProdutoAsync(produtoId, updateDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProdutoAsync_DeveRetornarTrue_QuandoExcluido()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.DeleteAsync(produtoId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteProdutoAsync(produtoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProdutoAsync_DeveRetornarFalse_QuandoNaoExcluido()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _mocker.GetMock<IProdutoRepository>()
                .Setup(r => r.DeleteAsync(produtoId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteProdutoAsync(produtoId);

            // Assert
            Assert.False(result);
        }
    }
}
