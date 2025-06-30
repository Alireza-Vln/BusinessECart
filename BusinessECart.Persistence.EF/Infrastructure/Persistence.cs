using Autofac;
using Microsoft.Extensions.Configuration;

namespace BusinessECart.Persistence.EF.Infrastructure;

public static class Persistence
{
    public static IPersistenceConfig BuildPersistenceConfig(
        IConfiguration appSetting,
        string configKey = "persistenceConfig")
    {
        var config = new PersistenceConfig();
        appSetting.Bind(configKey, config);

        return config;
    }

    public static IPersistenceRegister Setup(
        IPersistenceConfig config, ContainerBuilder container)
    {
        return new PersistenceRegister(config, container);
    }
}