﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HubOfChess.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
