using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{
    [DisallowMultipleComponent]
    public abstract class RegistrationHelperGameObject : MonoBehaviour, IRegistrationHelper
    {
        [SerializeField]
        private bool m_isDestoryOnBuild = false;
        public bool IsDestoryOnBuild => m_isDestoryOnBuild;

        public virtual INeCoResolver RegistrationAndBuild()
        {
            INeCoBuilder builder = _.Create();
            Registration(builder);

            return builder.Build();
        }

        public abstract INeCoBuilder Registration(INeCoBuilder container = default);
    }
}