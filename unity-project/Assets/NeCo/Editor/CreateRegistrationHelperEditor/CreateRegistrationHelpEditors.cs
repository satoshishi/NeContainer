namespace NeCo.Helper.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class CreateRegistrationHelpEditors
    {
        public static readonly string ScriptNameSource = "ScriptName.asset";

        private ScriptName scriptName;

        public static readonly string NameSpaceNameSource = "NameSpaceName.asset";

        private NameSpaceName nameSpaceName;

        public static readonly string ScriptPathSource = "ScriptPath.asset";

        private ScriptPath scriptPath;

        public static readonly string PrefabPathSource = "PrefabPath.asset";

        private PrefabPath prefabPath;

        public static readonly string createScriptSource = "CreateScript.asset";

        private CreateScript createScript;

        private CreateRegistrationHelpEditors(ScriptName scriptName, NameSpaceName nameSpaceName, ScriptPath scriptPath, PrefabPath prefabPath, CreateScript createScript)
        {
            this.scriptName = scriptName;
            this.nameSpaceName = nameSpaceName;
            this.scriptPath = scriptPath;
            this.prefabPath = prefabPath;
            this.createScript = createScript;
        }

        public void Update()
        {
            this.scriptName.UpdateUI();
            this.nameSpaceName.UpdateUI();
            this.scriptPath.UpdateUI();
            this.prefabPath.UpdateUI();
            this.createScript.UpdateUI(this.scriptName, this.nameSpaceName, this.scriptPath, prefabPath);
        }

        public static CreateRegistrationHelpEditors Create()
        {
            ScriptName scriptName = Load<ScriptName>(ScriptNameSource);
            NameSpaceName nameSpaceName = Load<NameSpaceName>(NameSpaceNameSource);
            ScriptPath scriptPath = Load<ScriptPath>(ScriptPathSource);
            PrefabPath prefabPath = Load<PrefabPath>(PrefabPathSource);
            CreateScript createScript = Load<CreateScript>(createScriptSource);

            return new CreateRegistrationHelpEditors(scriptName, nameSpaceName, scriptPath, prefabPath, createScript);
        }

        private static T Load<T>(string name) where T : UnityEngine.Object
        {
            var allAsstPaths = AssetDatabase.GetAllAssetPaths();

            foreach (string path in allAsstPaths)
            {
                if (path.EndsWith(name))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    return asset as T;
                }
            }

            return null;
        }
    }
}