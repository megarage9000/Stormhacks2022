using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
namespace YoutubePlayer
{
    public class scr_VideoController : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        VideoPlayer videoPlayer;
        public Button btPlay;
        public Button btPause;
        public Button btReset;
        public Button bt;

        private void Awake()
        {
            btPlay.interactable = false;
            btPause.interactable = false;
            btReset.interactable = false;
            videoPlayer = youtubePlayer.GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;
        }

        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            btPlay.interactable = source.isPrepared;
            btPause.interactable = source.isPrepared;
            btReset.interactable = source.isPrepared;

        }

        public async void Prepare()
        {
            print("loading video..");
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("loading complete");
            }
            catch
            {
                print("error video not loading");
            }

        }

        public void PlayVideo()
        {
            videoPlayer.Play();
        }
        public void PauseVideo()
        {
            videoPlayer.Pause();
        }
        public void ResetVideo()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        private void OnDestroy()
        {
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }
    }

}
