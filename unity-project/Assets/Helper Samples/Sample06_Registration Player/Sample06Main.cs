using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

public class Sample06Main : MonoBehaviour
{
    public void onCompleted(INeCoResolver resolver)
    {
        Debug.Log(resolver.Resolve<Sample06ComponentsA>().Message);
    }
}
