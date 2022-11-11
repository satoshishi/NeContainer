using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{
    [DisallowMultipleComponent]
    public abstract class RegistrationHelperGameObject : MonoBehaviour, IRegistrationHelper
    {
        [SerializeField]
        private bool destoryOnBuild = false;
        public bool DestoryOnBuild => destoryOnBuild;

        public virtual INeCoResolver RegistrationAndBuild()
        {
            INeCoBuilder builder = _.Create();
            Registration(builder);

            return builder.Build();
        }

        public abstract INeCoBuilder Registration(INeCoBuilder container = default);
    }
}