namespace NeCo.Helper.Editor
{
    using System.Reflection;
    using UnityEngine;
    using UnityEditor;

    [InitializeOnLoad]
    public class CreatePrefab
    {
        private static readonly string createPrefabNameKey = "CreatingPrefabName";

        private static readonly string createPrefabPathKey = "CreatingPrefabPath";

        public static void RquestPrefabCreate(ScriptName prefabName, PrefabPath prefabPath, NameSpaceName nameSpaceName)
        {
            EditorPrefs.SetString(createPrefabNameKey, prefabName.Combine(nameSpaceName));
            EditorPrefs.SetString(createPrefabPathKey, prefabPath.Combine(prefabName));
        }

        private static string GetPrefabName()
        {
            return EditorPrefs.GetString(createPrefabNameKey);
        }

        private static string GetPrefabPath()
        {
            return EditorPrefs.GetString(createPrefabPathKey);
        }

        private static void ReleaseInfos()
        {
            EditorPrefs.DeleteKey(createPrefabNameKey);
            EditorPrefs.DeleteKey(createPrefabPathKey);
        }

        private static bool HasNeedPrefabCreatedInfos()
        {
            return EditorPrefs.HasKey(createPrefabNameKey) && EditorPrefs.HasKey(createPrefabPathKey);
        }

        static CreatePrefab()
        {
            try
            {
                if (!HasNeedPrefabCreatedInfos()) return;

                Handle();

                ReleaseInfos();
            }
            catch
            {
                ReleaseInfos();
            }
        }

        private static void Handle()
        {
            string prefabName = GetPrefabName();
            string prefabPath = GetPrefabPath();

            var gameObject = new GameObject(prefabName);
            var assembly = Assembly.Load("Assembly-CSharp");
            var classType = assembly.GetType(prefabName);
            gameObject.AddComponent(classType);

            PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
            GameObject.DestroyImmediate(gameObject);
        }
    }
}