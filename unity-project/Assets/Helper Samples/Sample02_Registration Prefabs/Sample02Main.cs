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
        INeCoBuilder builder = _.Create();
        this.helper.Registration(builder);
        builder.RegistrationAsConstant("hello");
        
        INeCoResolver resolver = builder.Build();

        resolver.Resolve<Sample02Components>("A").Say();
        resolver.Resolve<Sample02Components>("A").Say();

        resolver.Resolve<Sample02Components>("B").Say();
        resolver.Resolve<Sample02Components>("B").Say();
    }
}
