using NeCo.Helper;
using NeCo;

public class LocateServiceARegistration : RegistrationHelperGameObject
{
    public LocateServiceA service;
    public override INeCoBuilder Registration(INeCoBuilder container = null)
    {
        container.RegisterPrefab<ILocateServeSample>(service, null, true, false);
        return container;
    }
}
