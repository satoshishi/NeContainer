using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NeCo;

public class Dispose
{
    [UnityTest]
    public IEnumerator インスタンスとMonoBehaviourインスタンスとPrefabを生成した後にDispose()
    {
        GameObject monoBehaviour = new GameObject();
        monoBehaviour.name = "mono behaviour";
        var monoBehaviourComponents = monoBehaviour.AddComponent<PlayModeTestGameObject>();      
        INeCoBuilder builder = _.Create();
        builder.RegistrationMonoBehaviour_AsConstant<IPlayModeInstance>(monoBehaviourComponents, true);

        GameObject prefab = new GameObject();
        prefab.name = "prefab";
        var prefabComponents = prefab.AddComponent<PlayModeTestGameObject>();      
        builder.RegistrationPrefab_AsSingleton<PlayModeTestGameObject>(prefabComponents, null, true);        

        builder.RegistrationAsSingleton<RequestMonobehaviourClass>();

        INeCoResolver resolver = builder.Build();

        yield return new WaitForSeconds(10f);

        resolver.Dispose();
        builder.Dispose();

        Debug.Log("disposed");        

        yield return new WaitForSeconds(5f);

        Debug.Log("end");
    }
}
