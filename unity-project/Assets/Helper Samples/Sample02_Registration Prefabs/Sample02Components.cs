using UnityEngine;

public class Sample02Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    public void Say()
    {
        Debug.Log(this.message);
    }
}
