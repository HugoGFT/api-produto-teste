using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Data.Validators
{
    [ExcludeFromCodeCoverage]
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.Preco)
                .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

            RuleFor(x => x.Estoque)
                .GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo");

            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("Categoria é obrigatória")
                .MaximumLength(50).WithMessage("Categoria deve ter no máximo 50 caracteres");
        }
    }
}
