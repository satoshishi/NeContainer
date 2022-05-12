using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;
using NeCo.Helper;

public class RegistrationInPlayMode
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameObjectをInterfaceをキーで登録するケース()
    {
        GameObject target = new GameObject();
        var component = target.AddComponent<PlayModeTestGameObject>();

        INeCoBuilder builder = NeCoUtilities.Create();
        builder.RegisterMonoBehaviour<IPlayModeInstance>(component);

        INeCoResolver resolver = builder.Build();
        var result = resolver.Resolve<IPlayModeInstance>();

        Assert.IsNotNull(result);

        yield return null;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PrefabをInterfaceをキーで登録するケース()
    {
        GameObject target = new GameObject();
        var component = target.AddComponent<PlayModeTestGameObject>();

        INeCoBuilder builder = NeCoUtilities.Create();
        builder.RegisterPrefab<IPlayModeInstance>(component, null, false, false);

        INeCoResolver resolver = builder.Build();
        var result = resolver.Resolve<IPlayModeInstance>();

        Assert.IsNotNull(result);

        yield return null;
    }
}
