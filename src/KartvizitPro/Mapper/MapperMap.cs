using AutoMapper;
using Entites.Concrete;
using KartvizitPro.Model;

namespace KartvizitPro.Mapper
{
    public class MapperMap
    {
        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = false;
            cfg.AllowNullDestinationValues = false;
            cfg.CreateMap<Company, CompanyDto>().ReverseMap();
            cfg.CreateMap<Company, CompanyInsertDto>().ReverseMap();
        });

        public readonly IMapper _mapper;

        public MapperMap(IMapper mapper)
        {
            this._mapper = mapper;
            _mapper = configuration.CreateMapper();
        }
    }
}
