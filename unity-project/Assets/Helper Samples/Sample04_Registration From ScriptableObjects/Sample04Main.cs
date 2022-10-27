using UnityEngine;
using NeCo;
using NeCo.Helper;

public class Sample04Main : MonoBehaviour
{
    [SerializeField]
    private RegistrationHelperScriptableObject helper;

    // Start is called before the first frame update
    void Start()
    {
        INeCoResolver resolver = this.helper.RegistrationAndBuild();

        resolver.Resolve<Sample04Components>().Say();
    }
}
