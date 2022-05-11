using AutoMapper;
using HubOfChess.Application.Common.Handlers;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Application.Interfaces;
using HubOfChess.Persistence;
using System;

namespace HubOfChess.Tests.Common
{
    public abstract class TestQueryBase : IDisposable
    {
        protected readonly AppDbContext DbContext;
        protected readonly IMapper Mapper;
        protected readonly AppGetEntityQueryHandler QueryHandler;

        public TestQueryBase()
        {
            DbContext = AppDbContextFactory.Create();
            var confugurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IAppDbContext).Assembly));
            });
            Mapper = confugurationProvider.CreateMapper();
            QueryHandler = new(DbContext);
        }

        public void Dispose()
        {
            AppDbContextFactory.Destroy(DbContext);
        }
    }
}
