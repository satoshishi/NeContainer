namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class CreateRegistrationHelperEditor : EditorWindow
    {
        private ScriptName ScriptName = new ScriptName("", "スクリプト名");
        private NameSpaceName NameSpaceName = new NameSpaceName("", "スクリプトのNameSpace");
        private ScriptPath ScriptPath = new ScriptPath("", "スクリプトを生成するPath");
        private PrefabPath PrefabPath = new PrefabPath("", "Prefabを生成するPath");
        private CreateScript CreateScript = new CreateScript("スクリプトを生成");

        [MenuItem("NeCo/ CreateRegistrationHelper")]
        private static void Create()
        {
            // 生成
            GetWindow<CreateRegistrationHelperEditor>("NeCo");
        }

        private void OnGUI()
        {
            this.ScriptName.Update();
            this.NameSpaceName.Update();
            this.ScriptPath.Update();
            this.PrefabPath.Update();

            this.CreateScript.Update(this.ScriptName, this.NameSpaceName, this.ScriptPath);
        }
    }
}
