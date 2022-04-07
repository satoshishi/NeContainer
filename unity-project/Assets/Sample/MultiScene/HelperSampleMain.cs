using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo.Helper;
using NeCo;

public class HelperSampleMain : MonoBehaviour
{
    [SerializeField]
    private SceneRegistrationHelper helper;

    // Start is called before the first frame update
    void Start()
    {
        var resolver = helper.RegistrationAndBuild();
    }
}
