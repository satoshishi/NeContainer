namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class ScriptPath
    {
        public string Value { get; private set; } = string.Empty;

        private readonly string labelName;

        public ScriptPath(string value, string labelName)
        {
            this.Value = value;
            this.labelName = labelName;
        }

        public void Update()
        {
            if (GUILayout.Button(this.labelName))
            {
                var filePath = EditorUtility.OpenFolderPanel("対象ディレクトリを選択", Application.dataPath, "");
                this.Value = filePath;
            }

            EditorGUILayout.LabelField(this.Value);
        }

        public string Combine(ScriptName scriptName)
        {
            return this.Value + "/" + scriptName.Value + ".cs";
        }
    }
}