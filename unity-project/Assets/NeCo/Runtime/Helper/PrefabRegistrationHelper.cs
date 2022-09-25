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

            public bool isTransient;

            public bool dontDestoryOnLoad;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            foreach (var parameter in m_parameters)
            {
                if (parameter.isTransient)
                {
                    container.RegistrationPrefab_AsTransient(parameter.instance, parameter.parent, parameter.entryPoint, parameter.dontDestoryOnLoad);
                }
                else
                {
                    container.RegistrationPrefab_AsSingleton(parameter.instance, parameter.parent, parameter.entryPoint, parameter.dontDestoryOnLoad);
                }
            }

            return container;
        }
    }
}