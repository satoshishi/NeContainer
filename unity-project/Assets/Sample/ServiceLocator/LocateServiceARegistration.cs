using NeCo.Helper;
using NeCo;
using UnityEngine;

public class LocateServiceARegistration : RegistrationHelperGameObject
{
    public  GameObject target;
    public override INeCoBuilder Registration(INeCoBuilder container = null)
    {
        ILocateServeSample service = target.GetComponent<ILocateServeSample>();
        container.RegisterPrefab<ILocateServeSample>(service, null, true, false);
        return container;
    }
}
