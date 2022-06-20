namespace NeCo.Helper.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class CreateRegistrationHelperWindow : EditorWindow
    {
        private CreateRegistrationHelpEditors editors;

        [MenuItem("NeCo/ CreateRegistrationHelper")]
        private static void Create()
        {
            // 生成
            GetWindow<CreateRegistrationHelperWindow>("NeCo");
        }

        private void OnGUI()
        {
            if (this.editors == null)
            {
                this.editors = CreateRegistrationHelpEditors.Create();
            }

            this.editors.Update();
        }
    }
}
