using UnityEngine;

public class Sample05Components : MonoBehaviour
{
    [SerializeField]
    private string message;

    public void Start()
    {
        Debug.Log(this.message);
    }
}
