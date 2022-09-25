using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class MonoBehaviourRegistrationHelper : RegistrationHelperGameObject
    {
        [System.Serializable]
        public class RegistrationParameter
        {
            public bool entryPoint;

            public MonoBehaviour instance;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            foreach (var parameter in m_parameters)
            {
                container.RegistrationMonoBehaviour_AsConstant(parameter.instance, parameter.entryPoint);
            }

            return container;
        }
    }
}