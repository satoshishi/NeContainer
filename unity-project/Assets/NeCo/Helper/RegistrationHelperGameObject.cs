using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeCo.Helper
{
    [DisallowMultipleComponent]
    public abstract class RegistrationHelperGameObject : MonoBehaviour, IRegistrationHelper
    {
        public abstract INeCoResolver RegistrationAndBuild();

        public abstract INeCoBuilder Registration(INeCoBuilder container = default);
    }
}