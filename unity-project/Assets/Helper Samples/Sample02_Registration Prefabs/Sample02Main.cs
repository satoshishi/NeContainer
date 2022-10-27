using UnityEngine;
using NeCo;
using NeCo.Helper;

public class Sample02Main : MonoBehaviour
{
    [SerializeField]
    private PrefabRegistrationHelper helper;

    // Start is called before the first frame update
    void Start()
    {
        INeCoResolver resolver = this.helper.RegistrationAndBuild();

        resolver.Resolve<Sample02Components>("A").Say();
        resolver.Resolve<Sample02Components>("A").Say();

        resolver.Resolve<Sample02Components>("B").Say();
        resolver.Resolve<Sample02Components>("B").Say();
    }
}
