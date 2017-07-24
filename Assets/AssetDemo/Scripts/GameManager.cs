using UnityEngine;
using System.Collections;
using AssetBundles;

namespace AssetBundles.Demo
{
    public class GameManager : MonoBehaviour
    {       
        public GameObject debbugerT;
        // Use this for initialization
        IEnumerator Start()
        {
            DontDestroyOnLoad(debbugerT);
            yield return StartCoroutine(Initialize());
        }

        #region ResourceInit

        protected IEnumerator Initialize()
        {
            // Don't destroy the game object as we base on it to run the loading script.
            DontDestroyOnLoad(gameObject);

            InitializeSourceURL();

            // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
            var request = AssetBundleManager.Initialize();

            if (request != null)
                yield return StartCoroutine(request);
        }

        void InitializeSourceURL()
        {
            // 如果开启ODR/配置ODR
#if ENABLE_IOS_ON_DEMAND_RESOURCES
            if (UnityEngine.iOS.OnDemandResources.enabled)
            {
                AssetBundleManager.SetSourceAssetBundleURL("odr://");
                return;
            }
#endif
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            // With this code, when in-editor or using a development builds: Always use the AssetBundle Server
            // (This is very dependent on the production workflow of the project.
            //      Another approach would be to make this configurable in the standalone player.)
            AssetBundleManager.SetDevelopmentAssetBundleServer();
            return;
#else
// 如果资源包放在StreamingAssets目录下，使用此地址:
        AssetBundleManager.SetSourceAssetBundleURL(Application.dataPath + "/");
        // 或者从网址下载
        //AssetBundleManager.SetSourceAssetBundleURL("http://www.MyWebsite/MyAssetBundles");
        return;
#endif
        }
        #endregion
    }
}