using UnityEngine;
using NeCo;
using NeCo.Helper;

public class Sample05Main : MonoBehaviour
{
    [SerializeField]
    private RegistrationHelperGameObject helper;

    // Start is called before the first frame update
    void Start()
    {
        helper.RegistrationAndBuild();
    }
}
