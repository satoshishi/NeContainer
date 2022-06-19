namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class CreateScript
    {
        private readonly string labelName;

        public CreateScript(string labelName)
        {
            this.labelName = labelName;
        }

        public void Update(ScriptName scriptName, NameSpaceName nameSpaceName, ScriptPath scriptPath)
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
