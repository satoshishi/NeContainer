using NeCo.Helper;
using NeCo;

public class LocateServiceARegistration : HierarchyRegistrationHelper
{
    protected override void OnRegistration(INeCoBuilder container)
    {
        container.Register<LocateServiceA>(InstanceType.Singleton);
    }
}
