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
        INeCoBuilder builder = _.Create();

        builder.RegistrationAsConstant("hello ");
        this.helper.Registration(builder);
        INeCoResolver resolver = builder.Build();

        resolver.Resolve<Sample01Components>("A").Say();
        resolver.Resolve<Sample01Components>("B").Say();
    }
}
