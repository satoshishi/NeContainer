namespace NeCo.Helper
{
    using UnityEngine;
    using UnityEditor;

    public class CreateRegistrationHelperEditor : EditorWindow
    {
        [MenuItem("NeCo/ CreateRegistrationHelper")]
        private static void Create()
        {
            // 生成
            GetWindow<CreateRegistrationHelperEditor>("NeCo");
        }

        private void OnGUI()
        {
            var allAsstPaths = AssetDatabase.GetAllAssetPaths();
            string fileName = "RegistrationHelper_TemplateSources.bytes";

            foreach(string path in allAsstPaths)
            {
                if(path.EndsWith(fileName))
                {
                    var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    Debug.Log(textAsset.text);
                }
            }
        }
    }
}
