using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Services
{
    public interface IDbContextFactory
    {
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
    }
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return GetScopedContext<TDbContext>();
        }

        private TDbContext GetScopedContext<TDbContext>() where TDbContext : DbContext
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetService<TDbContext>();
        }
    }
}
