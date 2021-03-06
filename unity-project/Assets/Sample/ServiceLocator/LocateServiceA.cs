using UnityEngine;

public interface ILocateServeSample
{
    void Say();
}

public class LocateServiceA : MonoBehaviour, ILocateServeSample
{
    public void Say()
    {
        UnityEngine.Debug.Log(this.name);
    }
}

public class LocateServiceB : ILocateServeSample
{
    public void Say()
    {
        UnityEngine.Debug.Log(this.GetHashCode());
    }    
}
