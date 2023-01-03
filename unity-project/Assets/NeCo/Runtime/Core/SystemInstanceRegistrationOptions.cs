using UnityEngine;

namespace NeCo
{
    [System.Serializable]
    public class SystemInstanceRegistrationOptions
    {
        public bool IsThisEntryPoint = false;

        public string Id = string.Empty;        

        public InjectionType injectionType = InjectionType.Self;
    }
}