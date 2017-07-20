using UnityEngine;
using System.Collections;
using AssetBundles;

public class AssetTest : MonoBehaviour {
    string sceneName = "TestScene";
    string sceneAssetBundle = "scene-bundle";
    
    public void LoadSceneTest()
    {
        StartCoroutine(InitializeLevelAsync(sceneName, true));
    }

    public void LoadObj()
    {
        StartCoroutine(InstantiateGameObjectAsync("cube-bundle", "MyCube"));

    }

    protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
    {
        float startTime = Time.realtimeSinceStartup;

        Debug.Log("add:******" + assetBundleName);

        // 加载
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);

        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();
        GameObject testObj = null;

        if (prefab != null)
        {
             testObj = GameObject.Instantiate(prefab);
        }

        // 计算时间
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");

        float time = 5f;
        Debug.Log(time+"秒后销毁");
        yield return new WaitForSeconds(time);
        if (testObj != null)
        {
            Destroy(testObj);
        }
    }



    protected IEnumerator InitializeLevelAsync(string levelName, bool isAdditive)
    {
        // This is simply to get the elapsed time for this phase of AssetLoading.
        float startTime = Time.realtimeSinceStartup;

        // Load level from assetBundle.
        AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
        if (request == null)
            yield break;
        yield return StartCoroutine(request);

        // Calculate and display the elapsed time.
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        Debug.Log("Finished loading scene " + levelName + " in " + elapsedTime + " seconds");
    }
}
