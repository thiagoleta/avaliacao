using Business.Interfaces;
using Business.Notificacoes;
using Business.Services;
using Data.Context;
using Data.Repository;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
   
            services.AddScoped<INotificador, Notificador>();       
            services.AddScoped<ITarefaService, TarefaService>();

            return services;
        }
    }
}