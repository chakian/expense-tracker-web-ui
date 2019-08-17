using AutoMapper;
using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Identity;

namespace ExpenseTracker.Business.AutoMapper
{
    public class AutoMapperBootstrap
    {
        public static IMapper Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserEntity>().IgnoreAllPropertiesWithAnInaccessibleSetter().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<UserEntity, User>().IgnoreAllPropertiesWithAnInaccessibleSetter().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            });

            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();

            // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            var mapper = configuration.CreateMapper();

            return mapper;
        }
    }
}
