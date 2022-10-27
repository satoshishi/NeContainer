using UnityEngine;
using NeCo;
using NeCo.Helper;
public class Sample03Main : MonoBehaviour
{
    [SerializeField]
    private SceneRegistrationHelper helper;

    // Start is called before the first frame update
    void Start()
    {
        INeCoResolver resolver = this.helper.RegistrationAndBuild();

        resolver.Resolve<Sample03Components>("prefab").Say();
        resolver.Resolve<Sample03Components>("mono").Say();
    }
}
