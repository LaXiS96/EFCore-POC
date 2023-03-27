using AutoMapper;

namespace WebApplication1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataModel.Entity, ApiModel.Entity>()
                //.ForMember(apiModel => apiModel.Document, o => o.MapFrom(dataModel => EF.Functions.AsJsonDictionary(dataModel.JsonDocument)));

                // For some reason, MapFrom seems to work with a call to JsonDictionary's constructors or JsonDictionary.FromJson, but only when not filtering
                // When filtering, we must replace dictionary accesses with calls to JSON_VALUE
                .ForMember(apiModel => apiModel.Document, o => o.MapFrom(dataModel => JsonDictionary.FromJson(dataModel.JsonDocument)));
        }
    }
}
