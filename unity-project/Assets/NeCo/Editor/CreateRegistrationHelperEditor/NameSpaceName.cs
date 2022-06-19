namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class NameSpaceName
    {
        public string Value {get; private set;} = string.Empty;

        private readonly string labelName;

        public NameSpaceName(string value, string labelName)
        {
            this.Value = value;
            this.labelName = labelName;
        }

        public void Update()
        {
            this.Value = EditorGUILayout.TextField(this.labelName, this.Value);            
        }      
    }
}