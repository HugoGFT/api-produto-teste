using CrudTesteTotvs.Domain.Entities;
using FluentValidation;

namespace CrudTesteTotvs.Service.Interface
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        void Validate(TEntity obj, AbstractValidator<TEntity> validator);
    }
}
