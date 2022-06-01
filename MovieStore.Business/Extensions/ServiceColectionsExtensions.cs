using Microsoft.Extensions.DependencyInjection;
using MovieStore.Business.Abstract;
using MovieStore.Business.Concrete;
using MovieStore.Core.Utilities.Security.JWT;
using MovieStore.DataAccess.Abstract;
using MovieStore.DataAccess.Concrete;
using MovieStore.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business.Extensions
{
    public static class ServiceColectionsExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddDbContext<MovieStoreDbContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IMovieService, MovieManager>();
            serviceCollection.AddScoped<ICustomerService, CustomerManager>();
            serviceCollection.AddScoped<IAuthService, AuthManager>();
            serviceCollection.AddScoped<IActorService, ActorManager>();
            serviceCollection.AddScoped<IDirectorService, DirectorManager>();
            serviceCollection.AddScoped<IOrderService, OrderManager>();
            serviceCollection.AddSingleton<ITokenHelper, JwtHelper>();

            return serviceCollection;
        }
    }
}
