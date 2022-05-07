using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{

    [CreateAssetMenu(fileName = "Write Name", menuName = "NeCo/ScriptableObjectRegistrationHelper")]
    public class ScriptableObjectRegistrationHelper : ScriptableObject
    {
        [SerializeField]
        private RegistrationHelperBase[] m_helpers;

        public INeCoResolver RegistrationAndBuild()
        {
            var container = NeCoUtilities.Create();

            foreach (var helper in m_helpers)
            {
                container = helper.Registration(container);
            }

            return container.Build();
        }
    }
}