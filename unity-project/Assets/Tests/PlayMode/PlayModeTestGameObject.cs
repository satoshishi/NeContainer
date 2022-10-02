using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

public interface IPlayModeInstance
{
    void Say();
}

public class PlayModeTestGameObject : MonoBehaviour, IPlayModeInstance
{
    public void Say()
    {
        Debug.Log(this.name);
    }
}

public class RequestMonobehaviourClass
{
    [Inject]
    public void Injected(IPlayModeInstance target)
    {
       target.Say();
    }

    
}
