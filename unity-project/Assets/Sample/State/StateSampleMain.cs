using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;
using NeCo.Helper;

public class StateSampleMain : MonoBehaviour
{
    [SerializeField]
    private SceneRegistrationHelper helper;

    private INeCoResolver resolver;

    private void Start()
    {
        resolver = helper.RegistrationAndBuild();
        resolver.Resolve<StateA>().EnterState();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            resolver.Resolve<CurrentlyStateObserver>().Currently.ChangeState<StateA>();
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            resolver.Resolve<CurrentlyStateObserver>().Currently.ChangeState<StateB>();
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            resolver.Resolve<CurrentlyStateObserver>().Currently.ChangeState<StateC>();
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            resolver.Resolve<CurrentlyStateObserver>().Currently.ChangeState<StateD>();
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            resolver.Resolve<CurrentlyStateObserver>().Currently.ChangeState<StateE>();
        }
    }
}
