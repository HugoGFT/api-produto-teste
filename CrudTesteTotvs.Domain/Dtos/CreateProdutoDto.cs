using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
