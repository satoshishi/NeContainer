namespace NeCo.Helper.Editor
{
    using System.IO;
    using UnityEditor;

    public class OutPutCreatedRegistrationHelperSources
    {
        public void Handle(ScriptName scriptName, ScriptPath scriptPath, Script body)
        {
            string containsFileNamePath = scriptPath.Combine(scriptName);

            File.WriteAllText(containsFileNamePath, body.Value);           
            AssetDatabase.Refresh();
        }        
    }
}