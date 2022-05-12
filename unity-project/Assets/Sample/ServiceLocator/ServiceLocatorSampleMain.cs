using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorSampleMain : MonoBehaviour
{
    private void Awake()
    {
        var service = ServiceLocator.Instance.Get<ILocateServeSample>();
        service.Say();   
    }
}
