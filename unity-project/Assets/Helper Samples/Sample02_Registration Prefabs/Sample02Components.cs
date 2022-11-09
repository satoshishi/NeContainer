using UnityEngine;
using NeCo;

public class Sample02Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    private string globalMessage;

    [Inject]
    private void Injection(string globalMessage)
    {
        this.globalMessage = globalMessage;
    }

    public void Say()
    {
        Debug.Log(this.globalMessage + " " + this.message);
    }
}
