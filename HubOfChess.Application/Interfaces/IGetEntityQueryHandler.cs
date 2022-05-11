namespace HubOfChess.Application.Interfaces
{
    public interface IGetEntityQueryHandler<T> where T : class
    {
        Task<T> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
