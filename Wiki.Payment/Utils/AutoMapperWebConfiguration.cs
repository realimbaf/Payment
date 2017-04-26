using System.Globalization;
using AutoMapper;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;

namespace Wiki.Payment.Utils
{
    public static class AutoMapperWebConfiguration
    {
        public static MapperConfiguration MapperConfiguration;

        public static void Configure()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfile());
            });
        }

        public class ClientProfile : Profile
        {
            public ClientProfile()
            {
                CreateMap<Client, DTOClient>()
                    .ForMember(x => x.ClientCode, y => y.MapFrom(src => src.User.ClientCode))
                    .ForMember(x => x.Id, y => y.MapFrom(src => src.User.ClientId))
                    .ForMember(x => x.Name, y => y.MapFrom(src => src.User.Name));
                CreateMap<Manager, DTOManager>()
                    .ForMember(x => x.LastModified,
                        y => y.MapFrom( src =>
                                   !src.LastModified.HasValue || src.LastModified.Value.ToString(@"dd\.MM\.yyyy HH:mm") == "01.01.0001 00:00" 
                                        ? ""
                                        : src.LastModified.Value.ToString(@"dd\.MM\.yyyy HH:mm")));

                CreateMap<Common.POCO.Payment, DTOPayment>()
                    .ForMember(x => x.ClientCode, y => y.MapFrom(src => src.Client.ClientCode))
                    .ForMember(x => x.ClientId, y => y.MapFrom(src => src.Client.ClientId))
                    .ForMember(x => x.ClientName, y => y.MapFrom(src => src.Client.Name))
                    .ForMember(x => x.OperatorCode, y => y.MapFrom(src => src.Operator.ClientCode))
                    .ForMember(x => x.OperatorId, y => y.MapFrom(src => src.Operator.ClientId))
                    .ForMember(x => x.OperatorName, y => y.MapFrom(src => src.Operator.Name))
                    .ForMember(x => x.CreatedDate,
                        y => y.MapFrom(src => src.CreatedDate.ToString(@"dd\.MM\.yyyy HH:mm")))
                    .ForMember(x => x.LastModified,
                        y => y.MapFrom(src => src.LastModified.Value.ToString(@"dd\.MM\.yyyy HH:mm") == "01.01.0001 00:00" ? "" : src.LastModified.Value.ToString(@"dd\.MM\.yyyy HH:mm")));
            }
        }
    }
}
