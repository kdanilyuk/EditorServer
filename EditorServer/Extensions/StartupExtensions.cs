using AutoMapper;
using EditorServer.Contexts;
using EditorServer.Profiles;
using EditorServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Extensions
{
    public static class StartupExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DocumentProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddContexts(this IServiceCollection services, IConfiguration configuration, string documentDatabase)
        {
            services.AddDbContext<DocumentContext, DocumentContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString(documentDatabase)));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDocumentService, DocumentService>();
        }

        public static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IDbContextFactory, DbContextFactory>();
        }
    }
}
