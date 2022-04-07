using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class PrefabRegistrationHelper : HierarchyRegistrationHelper
    {
        [System.Serializable]
        public class RegistrationParameter
        {
            public bool entryPoint;

            public MonoBehaviour instance;

            public Transform parent;

            public string id;

            public bool isTransient;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        protected override void OnRegistration(INeCoBuilder container)
        {
            foreach (var parameter in m_parameters)
            {
                container.RegisterPrefab(parameter.instance, parameter.parent, parameter.isTransient, parameter.id, parameter.entryPoint);
            }
        }
    }
}