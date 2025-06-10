using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace LivrariaService.Config;

public static class DependecyInjection
{
    public static IServiceCollection Injection(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<ILivroRepository, LivroRepository>();
        service.AddScoped<ILivroService, LivroService>();

        service.AddScoped<IClienteRepository, ClienteRepository>();
        service.AddScoped<IClienteService, ClienteService>();

        service.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
        service.AddScoped<IEmprestimoService, EmprestimoService>();

        service.AddScoped<INotificador, Notificador>();

        return service;
    }
}
