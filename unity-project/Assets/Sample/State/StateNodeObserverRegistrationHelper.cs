using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeCo;

namespace NeCo.Helper
{
    public class StateNodeObserverRegistrationHelper : HierarchyRegistrationHelper
    {
        protected override void OnRegistration(INeCoBuilder container)
        {
            container.Register<CurrentlyStateObserver>(InstanceType.Singleton);
        }
    }
}