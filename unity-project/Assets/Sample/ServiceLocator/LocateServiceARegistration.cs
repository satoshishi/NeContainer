using NeCo.Helper;
using NeCo;

public class LocateServiceARegistration : RegistrationHelperGameObject
{
    public override INeCoBuilder Registration(INeCoBuilder container = null)
    {
        container.Register<LocateServiceA>(InstanceType.Singleton);
        return container;
    }
}
