namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "CreateScript", menuName = "NeCo/Editor/CreateScript")]
    public class CreateScript : ScriptableObject
    {
        [SerializeField]
        private string labelName;

        public void UpdateUI(ScriptName scriptName, NameSpaceName nameSpaceName, ScriptPath scriptPath)
        {
            if (GUILayout.Button(this.labelName))
            {
                Script createdScirpt = Script.CreateRaw();
                createdScirpt = createdScirpt.SetName(scriptName);
                createdScirpt = createdScirpt.SetNameSpace(nameSpaceName);
                createdScirpt.OutPut(scriptName, scriptPath);
            }
        }
    }
}
