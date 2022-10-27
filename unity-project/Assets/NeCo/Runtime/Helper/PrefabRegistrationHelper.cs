using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class PrefabRegistrationHelper : RegistrationHelperGameObject
    {
        [System.Serializable]
        public class RegistrationParameter
        {
            public MonoBehaviour instance;

            public bool isTransient;

            public PrefabRegistrationOptions options;

        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            foreach (var parameter in m_parameters)
            {
                if (parameter.isTransient)
                {
                    container.RegistrationPrefab_AsTransient(parameter.instance, parameter.options);
                }
                else
                {
                    container.RegistrationPrefab_AsSingleton(parameter.instance, parameter.options);
                }
            }

            return container;
        }
    }
}