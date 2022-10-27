using UnityEngine;

public class Sample04Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    public void Say()
    {
        Debug.Log(this.message);
    }
}
