using UnityEngine;

namespace NeCo
{
    [System.Serializable]
    public class MonoBehaviourRegistrationOptions
    {
        public bool IsThisEntryPoint = false;

        public string ComponentTypeName;

        public string Id = string.Empty;        
    }
}