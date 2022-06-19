namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "NameSpaceName", menuName = "NeCo/Editor/NameSpaceName")]
    public class NameSpaceName : ScriptableObject
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