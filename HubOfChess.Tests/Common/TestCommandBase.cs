using HubOfChess.Persistence;
using System;

namespace HubOfChess.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly AppDbContext DbContext;

        public TestCommandBase()
        {
            DbContext = AppDbContextFactory.Create();
        }

        public void Dispose()
        {
            AppDbContextFactory.Destroy(DbContext);
        }
    }
}
