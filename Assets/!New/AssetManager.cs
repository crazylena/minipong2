using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
#if UNITY_ANDROID
using Google.Play.AssetDelivery;
#endif

//https://developer.android.com/guide/playcore/asset-delivery/integrate-unity

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    public AssetReference[] loadableAssets;
    public string[] loadableAssetNames;
    public List<VideoClip> _videoClips = new();
    public bool isLoadingFinished;

    private void Awake()
    {
        Instance = this;

        StartCoroutine(LoadAll());
    }

    private IEnumerator LoadAll()
    {
#if !UNITY_ANDROID // && !UNITY_EDITOR
        for (var i = 0; i < loadableAssetNames.Length; i++)
        {
            var assetPackRequest = PlayAssetDelivery.RetrieveAssetBundleAsync(loadableAssetNames[i]);
            yield return assetPackRequest;
            if (assetPackRequest.Error != AssetDeliveryErrorCode.NoError)
            {
                Debug.LogError("Failed to retrieve " + loadableAssetNames[i] + " asset pack: {assetPackRequest.Error}");
                yield break;
            }

            var videoClip = assetPackRequest.AssetBundle.LoadAsset<VideoClip>(loadableAssetNames[i]);
            _videoClips.Add(videoClip);
        }
#else
        for (int i = 0; i < loadableAssets.Length; i++)
        {
            AsyncOperationHandle<VideoClip> handle = loadableAssets[i].LoadAssetAsync<VideoClip>();
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                VideoClip videoClip = handle.Result;
                _videoClips.Add(videoClip);
                Addressables.Release(handle);
            }
        }
#endif
        Debug.LogError("All video are loaded");
        isLoadingFinished = true;
    }

    public VideoClip GetVideo(int index)
    {
        if (_videoClips.Count == 0)
            return null;
        return _videoClips[index % _videoClips.Count];
    }
}