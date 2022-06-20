namespace NeCo.Helper.Editor
{
    using System;
    using UnityEngine;
    using UnityEditor;

    public class LoadRegistrtionHelperTemplateSources
    {
        private readonly string fileName = "RegistrationHelper_TemplateSources.bytes";

        public string Handle()
        {
            var allAsstPaths = AssetDatabase.GetAllAssetPaths();

            foreach(string path in allAsstPaths)
            {
                if(path.EndsWith(this.fileName))
                {
                    var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    return textAsset.text;
                }
            }

            throw new NullReferenceException("スクリプトを生成するためのテンプレートAssetが見つかりませんでした");
        }
    }
}