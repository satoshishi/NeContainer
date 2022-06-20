namespace NeCo.Helper.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CreateAssetMenu(fileName = "CreateScript", menuName = "NeCo/Editor/CreateScript")]
    public class CreateScript : ScriptableObject
    {
        [SerializeField]
        private string labelName;

        public void UpdateUI(ScriptName scriptName, NameSpaceName nameSpaceName, ScriptPath scriptPath, PrefabPath prefabPath)
        {
            if (GUILayout.Button(this.labelName))
            {
                if (scriptName.Invalid()) return;
                if (nameSpaceName.Invalid()) return;
                if (scriptPath.Invalid()) return;
                if (prefabPath.Invalid()) return;

                Script createdScirpt = Script.CreateRaw();
                createdScirpt = createdScirpt.SetName(scriptName);
                createdScirpt = createdScirpt.SetNameSpace(nameSpaceName);
                createdScirpt.OutPut(scriptName, scriptPath);

                CreatePrefab.RquestPrefabCreate(scriptName, prefabPath, nameSpaceName);
            }
        }
    }
}
