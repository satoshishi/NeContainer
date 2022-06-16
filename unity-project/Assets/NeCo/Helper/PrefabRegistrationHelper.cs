using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class PrefabRegistrationHelper : RegistrationHelperGameObject
    {
        [System.Serializable]
        public class RegistrationParameter
        {
            public bool entryPoint;

            public MonoBehaviour instance;

            public Transform parent;

            public string id;

            public bool isTransient;

            public bool dontDestoryOnLoad;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            foreach (var parameter in m_parameters)
            {
                container.RegisterPrefab(parameter.instance, parameter.parent, parameter.dontDestoryOnLoad, parameter.isTransient, parameter.entryPoint, parameter.id);
            }

            return container;
        }
    }
}