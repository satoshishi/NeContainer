using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{

    [CreateAssetMenu(fileName = "Write Name", menuName = "NeCo/RegistrationHelperScriptableObject")]
    public class RegistrationHelperScriptableObject : ScriptableObject, IRegistrationHelper
    {
        [SerializeField]
        private RegistrationHelperGameObject[] m_helpers;

        public INeCoResolver RegistrationAndBuild()
        {
            var container = _.Create();

            foreach (var helper in m_helpers)
            {
                container = helper.Registration(container);
            }

            return container.Build();
        }

        public INeCoBuilder Registration(INeCoBuilder container = default)
        {
            foreach (var helper in m_helpers)
            {
                container = helper.Registration(container);
            }

            return container;
        }
    }
}