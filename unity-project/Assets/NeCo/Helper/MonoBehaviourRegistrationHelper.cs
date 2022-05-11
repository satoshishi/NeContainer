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

            public string id;
        }

        [SerializeField]
        RegistrationParameter[] m_parameters;

        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            foreach (var parameter in m_parameters)
            {
                container.RegisterMonoBehaviour(parameter.instance, parameter.id, parameter.entryPoint);
            }

            return container;
        }
    }
}