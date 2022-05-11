using HubOfChess.Application.Common.Handlers;
using HubOfChess.Persistence;
using System;

namespace HubOfChess.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly AppDbContext DbContext;
        protected readonly AppGetEntityQueryHandler QueryHandler;


        public TestCommandBase()
        {
            DbContext = AppDbContextFactory.Create();
            QueryHandler = new(DbContext);
        }

        public void Dispose()
        {
            AppDbContextFactory.Destroy(DbContext);
        }
    }
}
