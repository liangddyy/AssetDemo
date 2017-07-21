#if ENABLE_IOS_ON_DEMAND_RESOURCES || ENABLE_IOS_APP_SLICING
using UnityEngine;
using UnityEditor;
using UnityEditor.iOS; 
using System.Collections;
using System.IO;

/// <summary>
/// ����Դ���б�ʶ��ֵ�ı༭���ű�
/// </summary>
public class BuildResources
{
    [InitializeOnLoadMethod]
    static void SetupResourcesBuild()
    {
        // ע��ص��ӿ� ��ȡ�ļ��б�
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
            // ����
            new Resource(manifest, GetPath(manifest)).AddOnDemandResourceTags(manifest),

            new Resource("material-bundle", GetPath("material-bundle")).AddOnDemandResourceTags("material-bundle"),
            new Resource("cube-bundle", GetPath("cube-bundle")).AddOnDemandResourceTags("cube-bundle"),
            new Resource("scene-bundle", GetPath("scene-bundle")).AddOnDemandResourceTags("scene-bundle"),
                        
            
            new Resource("variants/variant-scene", GetPath("variants/variant-scene")).AddOnDemandResourceTags("variants>variant-scene"),
            // ��Դ�ָ�
            new Resource("variants/myassets").BindVariant(GetPath("variants/myassets.hd"), "hd")
                                             .BindVariant(GetPath("variants/myassets.sd"), "sd")
                                             .AddOnDemandResourceTags("variants>myassets"),

            new Resource("variants/logo").BindVariant(GetPath("variants/logo.hd"), "hd")
                                         .BindVariant(GetPath("variants/logo.sd"), "sd"),


            new Resource("banner", GetPath("banner.english")).AddOnDemandResourceTags("banner"),
            new Resource("tanks-scene-bundle", GetPath("tanks-scene-bundle")).AddOnDemandResourceTags("tanks-scene-bundle"),
            new Resource("tanks-albedo", GetPath("tanks-albedo.normal-sd")).AddOnDemandResourceTags("tanks-albedo")
            
        };
    }
}
#endif