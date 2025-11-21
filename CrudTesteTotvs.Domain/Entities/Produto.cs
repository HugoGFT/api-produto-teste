using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Produto: BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
