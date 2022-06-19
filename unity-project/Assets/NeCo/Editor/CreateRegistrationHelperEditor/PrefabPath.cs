namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "PrefabPath", menuName = "NeCo/Editor/PrefabPath")]
    public class PrefabPath : ScriptableObject
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
                var filePath = EditorUtility.OpenFolderPanel(labelName, Application.dataPath, "");
                this.value = filePath;
            }


            EditorGUILayout.LabelField(this.Value);
        }
    }
}