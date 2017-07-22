using UnityEngine;
using System.Collections;
using AssetBundles;
using UnityEditor.iOS;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

namespace AssetBundles.Demo
{



    public class AssetTest : MonoBehaviour
    {
        string sceneName = "TestScene";
        string sceneAssetBundle = "scene-bundle";

        public void LoadSceneTest()
        {
//        BuildPipeline.OnDemandTagsCollectorDelegate.
            StartCoroutine(InitializeLevelAsync(sceneName, true));
        }

        /// <summary>
        /// ---------------
        /// </summary>
        public void LoadObj()
        {
            StartCoroutine(InstantiateGameObjectAsync("cube-bundle", "MyCube"));

        }

        public void ToTank()
        {
            LoadScene("TanksLoader");
        }

        /// <summary>
        /// ------------
        /// </summary>
        public void ToAsset()
        {
            LoadScene("VariantLoader");
        }

        private void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetBundleName">bundle对象</param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
        {
            float startTime = Time.realtimeSinceStartup;

            Debug.Log("add:******" + assetBundleName);

            // 加载
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName,
                typeof(GameObject));
            if (request == null)
                yield break;
            yield return StartCoroutine(request);


            //        request.
            GameObject prefab = request.GetAsset<GameObject>();
            GameObject testObj = null;
            if (prefab != null)
            {
                testObj = GameObject.Instantiate(prefab);
            }

            // 计算时间
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");

            float time = 5f;
            Debug.Log(time + "秒后销毁");
            yield return new WaitForSeconds(time);
            if (testObj != null)
            {
                Destroy(testObj);
            }

//        OnDemandResourcesRequest.Dispose();
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
}