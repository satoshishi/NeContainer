using UnityEngine;

public class Sample01Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    public void Say()
    {
        Debug.Log(this.message);
    }
}
