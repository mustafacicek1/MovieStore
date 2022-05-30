using Microsoft.Extensions.DependencyInjection;
using MovieStore.Business.Abstract;
using MovieStore.Business.Concrete;
using MovieStore.Core.Utilities.Security.JWT;
using MovieStore.DataAccess.Abstract;
using MovieStore.DataAccess.Concrete;
using MovieStore.DataAccess.Concrete.EntityFramework;

namespace MovieStore.Business.Extensions
{
    public static class ServisCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MovieStoreDbContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IMovieService, MovieManager>();
            serviceCollection.AddScoped<ICustomerService, CustomerManager>();
            serviceCollection.AddScoped<IAuthService, AuthManager>();
            serviceCollection.AddSingleton<ITokenHelper, JwtHelper>();

            return serviceCollection;
        }
    }
}
