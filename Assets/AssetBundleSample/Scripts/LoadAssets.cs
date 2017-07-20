using UnityEngine;
using System.Collections;
using AssetBundles;


public class LoadAssets : MonoBehaviour
{
    public const string AssetBundlesOutputPath = "/AssetBundles/";
    public string assetBundleName;
    public string assetName;

    // Use this for initialization
    IEnumerator Example()
    {
        yield return StartCoroutine(Initialize());


        yield return StartCoroutine(InstantiateGameObjectAsync(assetBundleName, assetName));
    }

    public void OnClickExample()
    {
        StartCoroutine(Example());
    }

    void InitializeSourceURL()
    {

        #if ENABLE_IOS_ON_DEMAND_RESOURCES
        if (UnityEngine.iOS.OnDemandResources.enabled)
        {
            AssetBundleManager.SetSourceAssetBundleURL("odr://");
            return;
        }
        #endif
        #if DEVELOPMENT_BUILD || UNITY_EDITOR

        AssetBundleManager.SetDevelopmentAssetBundleServer();
        return;
        #else

        AssetBundleManager.SetSourceAssetBundleURL(Application.dataPath + "/");

        return;
        #endif
    }

    // ��ʼ��url��AssetBundleManifest����.
    protected IEnumerator Initialize()
    {
        DontDestroyOnLoad(gameObject);

        InitializeSourceURL();

        var request = AssetBundleManager.Initialize();  // ����ƽ̨��ʼ��
        if (request != null)
            yield return StartCoroutine(request);
    }

    protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
    {
        float startTime = Time.realtimeSinceStartup;

        Debug.Log("add:******" + assetBundleName);

        // ����
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);

        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();

        if (prefab != null)
            GameObject.Instantiate(prefab);

        // ����ʱ��
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
    }
}
