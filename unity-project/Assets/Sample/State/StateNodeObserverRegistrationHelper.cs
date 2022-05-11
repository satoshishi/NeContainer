using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class StateNodeObserverRegistrationHelper : RegistrationHelperGameObject
    {
        public override INeCoBuilder Registration(INeCoBuilder container = null)
        {
            container.Register<CurrentlyStateObserver>(InstanceType.Singleton);

            return container;
        }
    }
}