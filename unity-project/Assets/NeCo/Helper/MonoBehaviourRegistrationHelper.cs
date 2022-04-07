using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class MonoBehaviourRegistrationHelper : HierarchyRegistrationHelper
    {
        [System.Serializable]
        public class RegistrationParameter
        {
            public bool entryPoint;

            public MonoBehaviour instance;

            public string id;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        protected override void OnRegistration(INeCoBuilder container)
        {
            foreach (var parameter in m_parameters)
            {
                container.RegisterMonoBehaviour(parameter.instance, parameter.id, parameter.entryPoint);
            }
        }
    }
}