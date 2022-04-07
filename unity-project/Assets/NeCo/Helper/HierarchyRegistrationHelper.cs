using UnityEngine;

namespace NeCo.Helper
{
    public abstract class HierarchyRegistrationHelper : MonoBehaviour, IRegistrationHelper
    {
        [SerializeField]
        private bool m_isDestoryOnBuild = false;
        public bool IsDestoryOnBuild => m_isDestoryOnBuild;

        public INeCoResolver RegistrationAndBuild()
        {
            var container = NeCoUtilities.Create();

            Registration(container);

            return container.Build();
        }        

        public INeCoBuilder Registration(INeCoBuilder container)
        {
            OnRegistration(container);

            return container;
        }

        protected abstract void OnRegistration(INeCoBuilder container);
    }
}