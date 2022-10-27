using UnityEngine;

namespace NeCo.Helper
{
    public class SceneRegistrationHelper : MonoBehaviour, IRegistrationHelper
    {

        [SerializeField]
        private bool m_isDestoryOnBuild = false;
        public bool IsDestoryOnBuild => m_isDestoryOnBuild;

        public INeCoResolver RegistrationAndBuild()
        {
            INeCoBuilder builder = _.Create();
            Registration(builder);

            return builder.Build();
        }

        public INeCoBuilder Registration(INeCoBuilder container = default)
        {
            var hierarchyRegistrations = GameObject.FindObjectsOfType<RegistrationHelperGameObject>();

            foreach (var helper in hierarchyRegistrations)
            {
                container = helper.Registration(container);

                if (helper.IsDestoryOnBuild)
                    Destroy(helper.gameObject);
            }
            if (IsDestoryOnBuild)
                Destroy(this.gameObject, 1f);

            return container;
        }
    }
}
