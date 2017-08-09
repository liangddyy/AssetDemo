using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.iOS;

namespace AssetBundles.Demo
{
    public class AssetTest : MonoBehaviour
    {
        public AudioSource audioSource;
        public Image image;
        
//        public ScriptableTest test;
        public void LoadSceneTest()
        {
            string sceneName = "TestScene";
            string sceneAssetBundle = "scene-bundle";
            StartCoroutine(InitializeLevelAsync(sceneAssetBundle, sceneName, true));
        }
        public void ReleaseSceneTest()
        {
//            OnDemandResourcesRequest.Dispose();
            AssetBundleManager.UnloadAssetBundle("scene-bundle");
        }
        /// <summary>
        /// ---------------
        /// </summary>
        public void LoadPrefab()
        {
            StartCoroutine(InstantiateGameObjectAsync("cube-bundle", "MyCube"));
        }

        public void LoadImg()
        {
            StartCoroutine(LoadSpriteAsync("img-bundle", "UnityLogo", SetSprite));
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void LoadAudio()
        {
            StartCoroutine(LoadAudioAsync("audio-bundle", "audiotest", OnLoadAudio));
        }

        public void OnLoadAudio(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public void LoadTxt()
        {
            StartCoroutine(LoadTxtAsync("txt-bundle", "TxtData", OnLoadTxt));
        }

        public void OnLoadTxt(TextAsset textAsset)
        {
            Debug.Log(textAsset.text);

        }
        public void LoadScriptableData()
        {
            StartCoroutine(LoadScritableAsync("scriptable-bundle", "TestData", OnLoadScriptable));
        }

        public void OnLoadScriptable(ScriptableObject scriptable)
        {
            ScriptableTest scriptableTest = (ScriptableTest) scriptable;
            Debug.Log("数据"+ scriptableTest.data.Length); 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetBundleName">bundle对象</param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        //        protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
        //        {
        //            float startTime = Time.realtimeSinceStartup;
        //
        //            Debug.Log("add:******" + assetBundleName);
        //
        //            // 加载
        //            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName,
        //                typeof(GameObject));
        //            if (request == null)
        //                yield break;
        //            yield return StartCoroutine(request);
        //
        //
        //            //        request.
        //            GameObject prefab = request.GetAsset<GameObject>();
        //            GameObject testObj = null;
        //            if (prefab != null)
        //            {
        //                testObj = GameObject.Instantiate(prefab);
        //            }
        //
        //            // 计算时间
        //            float elapsedTime = Time.realtimeSinceStartup - startTime;
        //            Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
        //                      " seconds");
        //
        //            float time = 5f;
        //            Debug.Log(time + "秒后销毁");
        //            yield return new WaitForSeconds(time);
        //            if (testObj != null)
        //            {
        //                Destroy(testObj);
        //            }
        //
        ////        OnDemandResourcesRequest.Dispose();
        //        }
        protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
        {
            // This is simply to get the elapsed time for this phase of AssetLoading.
            float startTime = Time.realtimeSinceStartup;

            // Load asset from assetBundle.
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName,
                typeof(GameObject));
            if (request == null)
            {
                Debug.LogError("Failed AssetBundleLoadAssetOperation on " + assetName + " from the AssetBundle " +
                               assetBundleName + ".");
                yield break;
            }
            yield return StartCoroutine(request);

            // Get the Asset.
            GameObject prefab = request.GetAsset<GameObject>();

            // Instantiate the Asset, or log an error.
            if (prefab != null)
                GameObject.Instantiate(prefab);
            else
                Debug.LogError("Failed to GetAsset from request");

            // Calculate and display the elapsed time.
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");
        }

        protected IEnumerator LoadSpriteAsync(string bundleName,string assetName,Action<Sprite> action)
        {
            float startTime = Time.realtimeSinceStartup;

            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName,typeof(Sprite));
            if (request == null)
            {
                Debug.LogError("Failed AssetBundleLoadAssetOperation on " + assetName + " from the AssetBundle " +
                               bundleName + ".");
                yield break;
            }
            yield return StartCoroutine(request);
            Sprite sprite = request.GetAsset<Sprite>();

            if (sprite != null)
            {
                if (action != null)
                {
                    action(sprite);
                }
            }
            else
                Debug.LogError("Failed to GetAsset from request");

            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (sprite == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");
            yield return null;
        }

        protected IEnumerator LoadAudioAsync(string bundleName, string assetName, Action<AudioClip> action)
        {
            float startTime = Time.realtimeSinceStartup;

            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName,
                typeof(AudioClip));
            if (request == null)
            {
                Debug.LogError("Failed AssetBundleLoadAssetOperation on " + assetName + " from the AssetBundle " +
                               bundleName + ".");
                yield break;
            }
            yield return StartCoroutine(request);
            AudioClip obj = request.GetAsset<AudioClip>();

            if (obj != null)
            {
                if (action != null)
                {
                    action(obj);
                }
            }
            else
                Debug.LogError("Failed to GetAsset from request");

            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (obj == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");
            yield return null;
        }

        protected IEnumerator LoadTxtAsync(string bundleName, string assetName, Action<TextAsset> action)
        {
            float startTime = Time.realtimeSinceStartup;

            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName,typeof(TextAsset));
            if (request == null)
            {
                Debug.LogError("Failed AssetBundleLoadAssetOperation on " + assetName + " from the AssetBundle " +
                               bundleName + ".");
                yield break;
            }
            yield return StartCoroutine(request);
            TextAsset obj = request.GetAsset<TextAsset>();

            if (obj != null)
            {
                if (action != null)
                {
                    action(obj);
                }
            }
            else
                Debug.LogError("Failed to GetAsset from request");

            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (obj == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");
            yield return null;
        }
        protected IEnumerator LoadScritableAsync(string bundleName, string assetName, Action<ScriptableObject> action)
        {
            float startTime = Time.realtimeSinceStartup;

            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName,
                typeof(ScriptableObject));
            if (request == null)
            {
                Debug.LogError("Failed AssetBundleLoadAssetOperation on " + assetName + " from the AssetBundle " +
                               bundleName + ".");
                yield break;
            }
            yield return StartCoroutine(request);
            ScriptableObject obj = request.GetAsset<ScriptableObject>();

            if (obj != null)
            {
                if (action != null)
                {
                    action(obj);
                }
            }
            else
                Debug.LogError("Failed to GetAsset from request");

            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log(assetName + (obj == null ? " was not" : " was") + " loaded successfully in " + elapsedTime +
                      " seconds");
            yield return null;
        }

        protected IEnumerator InitializeLevelAsync(string bundleName,string levelName, bool isAdditive)
        {
            // This is simply to get the elapsed time for this phase of AssetLoading.
            float startTime = Time.realtimeSinceStartup;

            // Load level from assetBundle.
            AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(bundleName, levelName, isAdditive);
            if (request == null)
                yield break;
            yield return StartCoroutine(request);

            // Calculate and display the elapsed time.
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log("Finished loading scene " + levelName + " in " + elapsedTime + " seconds");
        }
    }
}