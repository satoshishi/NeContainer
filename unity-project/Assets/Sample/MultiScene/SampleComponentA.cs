using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

public class SampleComponentA : MonoBehaviour
{
    [Inject]
    public void Init(SampleComponentB sampleComponentB)
    {
        sampleComponentB.Say();
    }
}
