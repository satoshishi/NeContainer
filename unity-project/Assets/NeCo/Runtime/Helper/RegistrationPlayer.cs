using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NeCo.Helper
{
    [DisallowMultipleComponent]
    public class RegistrationPlayer : MonoBehaviour
    {
        [SerializeField]
        private RegistrationHelperGameObject[] registerGameObjects;

        [SerializeField]
        private RegistrationHelperScriptableObject[] registerScriptables;

        [SerializeField]
        private bool playOnAwake;

        [SerializeField]
        private bool destoryOnBuild;

        [SerializeField]
        private UnityEvent<INeCoResolver> onCompleted;

        private void Awake()
        {
            if (playOnAwake)
            {
                this.Play();
            }
        }

        public void Play()
        {
            INeCoBuilder container = _.Create();

            foreach (RegistrationHelperGameObject registerGameObject in this.registerGameObjects)
            {
                registerGameObject.Registration(container);
            }

            foreach (RegistrationHelperScriptableObject registerScriptable in this.registerScriptables)
            {
                registerScriptable.Registration(container);
            }

            INeCoResolver resolver = container.Build();

            onCompleted?.Invoke(resolver);

            if (destoryOnBuild)
            {
                Destroy(this);
            }
        }
    }
}