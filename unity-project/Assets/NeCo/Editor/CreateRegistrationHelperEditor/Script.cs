namespace NeCo.Helper.Edior
{
    using UnityEngine;
    using UnityEditor;

    public class Script
    {
        public string Value { get; private set; } = string.Empty;

        public static Script CreateRaw()
        {
            LoadRegistrtionHelperTemplateSources loadTemplateSource = new LoadRegistrtionHelperTemplateSources();
            string sourceTemp = loadTemplateSource.Handle();

            return new Script(sourceTemp);
        }

        private Script(string value)
        {
            this.Value = value;
        }

        public Script SetName(ScriptName scriptName)
        {
            string newBody = this.Value.Replace(@"#SCRIPT_NAME#", scriptName.Value);
            return new Script(newBody);
        }

        public Script SetNameSpace(NameSpaceName nameSpaceName)
        {
            string newBody = this.Value.Replace(@"#SCRIPT_NAMESPACE#", nameSpaceName.Value);
            return new Script(newBody);
        }

        public void OutPut(ScriptName scriptName, ScriptPath scriptPath)
        {
            OutPutCreatedRegistrationHelperSources outputer = new OutPutCreatedRegistrationHelperSources();
            outputer.Handle(scriptName, scriptPath, this);
        }
    }
}