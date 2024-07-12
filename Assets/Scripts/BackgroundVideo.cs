using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        StartCoroutine(WaitAndPlayVideo());
    }

    private IEnumerator WaitAndPlayVideo()
    {
        yield return new WaitUntil(() => AssetManager.Instance.isLoadingFinished);
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.clip = AssetManager.Instance.GetVideo(PickRandomVideoClipIndex());
        _videoPlayer.Play();
    }

    private int PickRandomVideoClipIndex()
    {
        return Random.Range(0, AssetManager.Instance.loadableAssets.Length);
    }
}