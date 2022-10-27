using UnityEngine;

namespace NeCo
{
    [System.Serializable]
    public class PrefabRegistrationOptions
    {
        public Transform Parent = null;

        public bool DontDestoryOnLoad = false;

        public bool IsThisEntryPoint = false;

        public string Id = string.Empty;
    }
}