using UnityEngine;

public class Sample03Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    public void Say()
    {
        Debug.Log(this.message);
    }
}
