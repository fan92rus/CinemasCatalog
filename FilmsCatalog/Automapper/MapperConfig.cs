using AutoMapper;
using FilmsCatalog.Models;

namespace FilmsCatalog.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cinema, CinemaViewModel>();
            CreateMap<CinemaViewModel, CinemaEditModel>().ForMember(x => ((CinemaAddModel) x).PosterFile, x => x.Ignore());
        }
    }

    public class MapperEx
    {
        public static IMapper CreateMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}