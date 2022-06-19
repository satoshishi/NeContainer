namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class PrefabPath
    {
        public string Value { get; private set; } = string.Empty;

        private readonly string labelName;

        public PrefabPath(string value, string labelName)
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
    }
}