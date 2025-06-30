using Autofac;
using BusinessECart.App;
using BusinessECart.Contracts.Interfaces;
using BusinessECart.Infrastructure.Configs.Dates;
using BusinessECart.Persistence.EF;
using BusinessECart.Persistence.EF.Infrastructure;
using BusinessECart.Service;
using BusinessECart.Service.Authentications.Jwt;
using Microsoft.Extensions.Configuration;

namespace BusinessECart.Infrastructure.Configs.Autofac;

public class AutofacBusinessModule(IConfiguration configuration) : Module
{
    private readonly IPersistenceConfig _persistenceConfig = Persistence.EF.Infrastructure.Persistence.BuildPersistenceConfig(configuration);
    private const string ConnectionStringKey = "connectionString";

    protected override void Load(ContainerBuilder container)
    {
        var persistentAssembly = typeof(PersistentAssembly).Assembly;
        var serviceAssembly = typeof(ServiceAssembly).Assembly;
        var applicationAssembly =
            typeof(ApplicationAssembly).Assembly;
        var securityAssembly = typeof(JwtTokenGenerator).Assembly;

        
        container.RegisterType<EFUnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();
        
        // container.RegisterAssemblyTypes(persistentAssembly)
        //     .AsImplementedInterfaces()
        //     .InstancePerLifetimeScope();
        
        container
            .RegisterAssemblyTypes(
                serviceAssembly,
                applicationAssembly,persistentAssembly)
            .AssignableTo<IScope>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        
        container.RegisterType<DateTimeService>()
            .As<IDateTimeService>()
            .InstancePerLifetimeScope();
        


        base.Load(container);
    }
    
    
}