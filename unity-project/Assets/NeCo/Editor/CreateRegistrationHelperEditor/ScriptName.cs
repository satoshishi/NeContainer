namespace NeCo.Helper.Edior
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
    }
}