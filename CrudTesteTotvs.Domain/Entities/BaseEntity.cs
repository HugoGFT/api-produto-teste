using System.Diagnostics.CodeAnalysis;

namespace CrudTesteTotvs.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
