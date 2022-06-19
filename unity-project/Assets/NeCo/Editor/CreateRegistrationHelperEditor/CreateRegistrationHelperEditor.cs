namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class CreateRegistrationHelperEditor : EditorWindow
    {
        private ScriptName ScriptName = null;
        private NameSpaceName NameSpaceName = null;
        private ScriptPath ScriptPath = null;
        private PrefabPath PrefabPath = null;
        private CreateScript CreateScript = null;

        [MenuItem("NeCo/ CreateRegistrationHelper")]
        private static void Create()
        {
            // 生成
            GetWindow<CreateRegistrationHelperEditor>("NeCo");
        }

        private void OnGUI()
        {
            if (this.ScriptName == null)
                this.ScriptName = Load<ScriptName>("ScriptName.asset");
            this.ScriptName.UpdateUI();

            if (this.NameSpaceName == null)
                this.NameSpaceName = Load<NameSpaceName>("NameSpaceName.asset");
            this.NameSpaceName.UpdateUI();

            if (this.ScriptPath == null)
                this.ScriptPath = Load<ScriptPath>("ScriptPath.asset");
            this.ScriptPath.UpdateUI();

            if (this.PrefabPath == null)
                this.PrefabPath = Load<PrefabPath>("PrefabPath.asset");
            this.PrefabPath.UpdateUI();

            if (this.CreateScript == null)
                this.CreateScript = Load<CreateScript>("CreateScript.asset");
            this.CreateScript.UpdateUI(this.ScriptName, this.NameSpaceName, this.ScriptPath);
        }

        private T Load<T>(string name) where T : UnityEngine.Object
        {
            var allAsstPaths = AssetDatabase.GetAllAssetPaths();

            foreach(string path in allAsstPaths)
            {
                if(path.EndsWith(name))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    return asset as T;
                }
            }            

            return null;
        }
    }
}
