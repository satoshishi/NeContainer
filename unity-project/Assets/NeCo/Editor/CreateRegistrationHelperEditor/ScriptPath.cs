namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "ScriptPath", menuName = "NeCo/Editor/ScriptPath")]
    public class ScriptPath : ScriptableObject
    {
        [SerializeField]
        private string value;

        [SerializeField]
        private string labelName;

        public string Value => this.value;

        public void UpdateUI()
        {
            if (GUILayout.Button(this.labelName))
            {
                var filePath = EditorUtility.OpenFolderPanel("対象ディレクトリを選択", Application.dataPath, "");
                this.value = filePath;
            }

            EditorGUILayout.LabelField(this.Value);
        }

        public string Combine(ScriptName scriptName)
        {
            return this.Value + "/" + scriptName.Value + ".cs";
        }
    }
}