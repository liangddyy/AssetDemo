#if ENABLE_IOS_ON_DEMAND_RESOURCES || ENABLE_IOS_APP_SLICING
using UnityEngine;
using UnityEditor;
using UnityEditor.iOS;
using System.Collections;
using System.IO;

namespace AssetsDemo
{
    /// <summary>
    /// 对资源进行标识赋值的编辑器脚本
    /// </summary>
    public class BuildResources
    {
        [InitializeOnLoadMethod]
        static void SetupResourcesBuild()
        {
            // 注册回调接口 获取文件列表
            UnityEditor.iOS.BuildPipeline.collectResources += CollectResources;
        }

        static string GetPath(string relativePath)
        {
            string root = Path.Combine(AssetBundles.Utility.AssetBundlesOutputPath,
                AssetBundles.Utility.GetPlatformName());
            return Path.Combine(root, relativePath);
        }

        static UnityEditor.iOS.Resource[] CollectResources()
        {
            string manifest = AssetBundles.Utility.GetPlatformName();
            return new Resource[]
            {
                new Resource(manifest, GetPath(manifest)).AddOnDemandResourceTags(manifest),
                new Resource("material-bundle", GetPath("material-bundle")).AddOnDemandResourceTags("material-bundle"),
                new Resource("cube-bundle", GetPath("cube-bundle")).AddOnDemandResourceTags("cube-bundle"),
                new Resource("scene-bundle", GetPath("scene-bundle")).AddOnDemandResourceTags("scene-bundle"),
                new Resource("img-bundle", GetPath("img-bundle")).AddOnDemandResourceTags("img-bundle"),
                new Resource("audio-bundle", GetPath("audio-bundle")).AddOnDemandResourceTags("audio-bundle"),
                new Resource("txt-bundle", GetPath("txt-bundle")).AddOnDemandResourceTags("txt-bundle"),
                new Resource("scriptable-bundle", GetPath("scriptable-bundle")).AddOnDemandResourceTags("scriptable-bundle")

//                // 资源分割
//                new Resource("variants/myassets").BindVariant(GetPath("variants/myassets.hd"), "hd")
//                    .BindVariant(GetPath("variants/myassets.sd"), "sd")
//                    .AddOnDemandResourceTags("variants>myassets"),
//                new Resource("variants/logo").BindVariant(GetPath("variants/logo.hd"), "hd")
//                    .BindVariant(GetPath("variants/logo.sd"), "sd"),
            };
        }
    }
}

#endif