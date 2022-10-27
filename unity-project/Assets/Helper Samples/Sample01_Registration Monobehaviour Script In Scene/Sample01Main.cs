using UnityEngine;
using NeCo;
using NeCo.Helper;

public class Sample01Main : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviourRegistrationHelper helper;

    // Start is called before the first frame update
    void Start()
    {
        INeCoResolver resolver = this.helper.RegistrationAndBuild();

        resolver.Resolve<Sample01Components>("A").Say();
        resolver.Resolve<Sample01Components>("B").Say();
    }
}
