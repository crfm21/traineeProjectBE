using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Infrastructure.Context;
using RecipeApp.Domain.Core;
using RecipeApp.Infrastructure.CoreRepository;
using RecipeApp.Domain.Entities.RepositoryInterfaces;
using RecipeApp.Infrastructure.EntityRepositories;
using RecipeApp.Services.ServicesInterfaces;
using RecipeApp.Services;

namespace RecipeApp.IoC
{
    public class DependencyContainer
    {
        public static void ServicesInjection(IServiceCollection services, IConfiguration Configuration)
        {
            RegisterDbContext(services, Configuration);
            RegisterRepositories(services);
            RegisterServices(services);
        }

        public static void RegisterDbContext(IServiceCollection services, IConfiguration Configuration)
        {
            var conn = Configuration.GetConnectionString("myconn");
            // Add ApplicationDbContext and SQL Server support
            services.AddDbContext<MainContext>(item => item.UseSqlServer(conn));
            //services.AddDbContext<MainContext>(x => x.UseSqlServer("Data Source=LocalDatabase.db"));
            //the above does not do any difference: only line of the startup = result: name databe OK but there is no script beeing done
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(ICoreRepository<>), typeof(CoreRepository<>));

            services.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IIngredientRepository), typeof(IngredientRepository));
            services.AddScoped(typeof(IIngredientCompoRepository), typeof(IngredientCompositionRepository));
            services.AddScoped(typeof(IRecipeRepository), typeof(RecipeRepository));
            services.AddScoped(typeof(IFavoriteRecipeRepository), typeof(FavoriteRecipeRepository));
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMemberServices, MemberServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IIngredientServices, IngredientServices>();
            services.AddTransient<IIngredientCompoServices, IngredientCompoServices>();
            services.AddTransient<IRecipeServices, RecipeServices>();
            services.AddTransient<IFavoriteRecipeServices, FavoriteRecipeServices>();
        }

    }
}
