using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace HubOfChess.Application.Common.Handlers
{
    internal static class AppGetEntityQueryHandlerDI
    {
        public static IServiceCollection AddAppGetEntityQueryHandler(this IServiceCollection services)
        {
            services.AddScoped<IGetEntityQueryHandler<User>, AppGetEntityQueryHandler>();
            services.AddScoped<IGetEntityQueryHandler<Chat>, AppGetEntityQueryHandler>();
            services.AddScoped<IGetEntityQueryHandler<Message>, AppGetEntityQueryHandler>();
            services.AddScoped<IGetEntityQueryHandler<Post>, AppGetEntityQueryHandler>();
            services.AddScoped<IGetEntityQueryHandler<PostComment>, AppGetEntityQueryHandler>();

            return services;
        }
    }
}
