using UnityEngine;
using NeCo;
using NeCo.Helper;


public class Sample05CustomRegister : RegistrationHelperGameObject
{
    [SerializeField]
    private Sample05Components prefab;

    public override INeCoBuilder Registration(INeCoBuilder container = null)
    {
        container.RegistrationPrefab_AsSingleton(prefab, new PrefabRegistrationOptions()
        {
            IsThisEntryPoint = true,
            Parent = null,
            DontDestoryOnLoad = true,
            Id = "Sample05"
        });

        return container;
    }
}
