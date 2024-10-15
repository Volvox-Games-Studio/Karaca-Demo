using System.IO;
using UnityEngine;

namespace Publish
{
    public static class BuildPostProcess
    {
#if UNITY_EDITOR
        
        [UnityEditor.Callbacks.PostProcessBuild(1)]
        public static void OnPostprocessBuild(UnityEditor.BuildTarget target, string pathToBuiltProject) 
        {
            if (target is not UnityEditor.BuildTarget.WebGL) return;

            var imagePath = Path.Join(Application.dataPath, "Publish/logo.png");
            var imageTargetPath = Path.Join(Directory.GetParent(Application.dataPath)?.FullName, "VGBuilds/WebGL/TemplateData/unity-logo-dark.png");
            
            File.Copy(imagePath, imageTargetPath, true);
            
            var styleSheetPath = Path.Join(Application.dataPath, "Publish/style.css");
            var styleSheetTargetPath = Path.Join(Directory.GetParent(Application.dataPath)?.FullName, "VGBuilds/WebGL/TemplateData/style.css");
            
            File.Copy(styleSheetPath, styleSheetTargetPath, true);
        }
        
#endif
    }
}
