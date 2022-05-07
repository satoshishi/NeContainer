using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo.Helper;
using NeCo;

public class RuntimeBuildingServiceLocator_OnBeforeSceneLoad
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void BuildingServiceLocatorOnBeforeSceneLoad()
    {
        ScriptableObjectRegistrationHelper helper = Resources.Load("RegistrationHelpers") as ScriptableObjectRegistrationHelper;
        INeCoResolver resolver = helper.RegistrationAndBuild();

        GameObject serviceLocatorObject = new GameObject("ServiceLocator");
        UnityEngine.Object.DontDestroyOnLoad(serviceLocatorObject);
        ServiceLocator serviceLocator = serviceLocatorObject.AddComponent<ServiceLocator>();
        serviceLocator.Init(resolver);
    }
}
