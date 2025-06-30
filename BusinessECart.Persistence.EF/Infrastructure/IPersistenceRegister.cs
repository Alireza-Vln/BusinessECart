using System.Reflection;

namespace BusinessECart.Persistence.EF.Infrastructure;

public interface IPersistenceRegister
{
    IPersistenceRegister WithEntityMapsAssembly(Assembly assembly);
    void Register();
}
