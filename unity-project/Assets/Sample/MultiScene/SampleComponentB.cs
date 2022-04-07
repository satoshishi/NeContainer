using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleComponentB : MonoBehaviour
{
    public void Say() => Debug.Log("Hello " + gameObject.name);
}
