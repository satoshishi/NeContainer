using UnityEngine;

namespace NeCo.Helper
{
    public class SceneRegistrationHelper : MonoBehaviour, IRegistrationHelper
    {
        [SerializeField]
        private bool m_isDestoryOnBuild = false;

        public INeCoResolver RegistrationAndBuild()
        {
            var container = NeCoUtilities.Create();

            Registration(container);

            return container.Build();
        }        

        public INeCoBuilder Registration(INeCoBuilder container = default)
        {
            var hierarchyRegistrations = GameObject.FindObjectsOfType<HierarchyRegistrationHelper>();

            foreach (var helper in hierarchyRegistrations)
            {
                container = helper.Registration(container);

                if (helper.IsDestoryOnBuild)
                    Destroy(helper.gameObject);
            }
            if (m_isDestoryOnBuild)
                Destroy(this.gameObject, 1f);

            return container;
        }
    }
}
