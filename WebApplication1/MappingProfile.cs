using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataModel;

namespace WebApplication1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataModel.Entity, ApiModel.Entity>()
                .ForMember(apiModel => apiModel.Document, o => o.MapFrom(dataModel => EF.Functions.AsJsonDictionary(dataModel.JsonDocument)));
        }
    }
}
