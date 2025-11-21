using AutoMapper;
using CrudTesteTotvs.Domain.Dtos;
using CrudTesteTotvs.Domain.Entities;
using System.Diagnostics.CodeAnalysis;


namespace CrudTesteTotvs.Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<CreateProdutoDto, Produto>().ForMember(dest => dest.DataCriacao, src => src.MapFrom(o =>  DateTime.Now));
            CreateMap<UpdateProdutoDto, Produto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
