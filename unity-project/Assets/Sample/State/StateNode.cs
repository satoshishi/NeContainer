using UnityEngine;
using System;
using NeCo;

public class CurrentlyStateObserver
{
    public StateNode Currently
    {
        get;
        private set;
    } = null;

    public Action<StateNode> OnUpdatedState
    {
        get;
        set;
    } = null;

    public void Update(StateNode state)
    {
        Currently = state;
        OnUpdatedState?.Invoke(Currently);
    }
}


public abstract class StateNode : MonoBehaviour
{
    protected INeCoResolver Container
    {
        get;
        private set;
    } = null;

    protected CurrentlyStateObserver Observer
    {
        get;
        private set;
    } = null;

    public string Name
    {
        get;
        private set;
    } = null;

    [Inject]
    public void Init(INeCoResolver container, CurrentlyStateObserver observer)
    {
        this.Container = container;
        this.Observer = observer;
        Name = this.GetType().Name;
    }

    public void ChangeState<TO>() where TO : StateNode
    {
        this.ExitState();        

        var to = Container.Resolve<TO>();
        to.EnterState();        
    }

    public void EnterState()
    {
        Debug.Log($"[State Enter] -> {Name}");
        Observer.Update(this);       

        OnEnter(Container);
    }

    public void ExitState()
    {
        Debug.Log($"[State Exit] -> {Name}");
        OnExit(Container);
    }    

    protected abstract void OnEnter(INeCoResolver container);

    protected abstract void OnExit(INeCoResolver container);
}
