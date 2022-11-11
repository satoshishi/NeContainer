using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

public class Sample06ComponentsB : MonoBehaviour
{
    [Inject]
    private void Injection(Sample06ComponentsA a)
    {
        Debug.Log(a.Message);
    }
}
