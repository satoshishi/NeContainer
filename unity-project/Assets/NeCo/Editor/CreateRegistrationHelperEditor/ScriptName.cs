namespace NeCo.Helper.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "ScriptName", menuName = "NeCo/Editor/ScriptName")]
    public class ScriptName : ScriptableObject
    {
        [SerializeField]
        private string value;

        [SerializeField]
        private string labelName;

        public string Value => this.value;

        public void UpdateUI()
        {
            this.value = EditorGUILayout.TextField(this.labelName, this.Value);            
        }

        public bool Invalid()
        {
            return string.IsNullOrEmpty(this.value);
        }

        public string Combine(NameSpaceName nameSpaceName)
        {
            string _namespace = string.IsNullOrEmpty(nameSpaceName.Value) ? "" : nameSpaceName.Value + ".";
            return _namespace + this.value;
        }
    }
}