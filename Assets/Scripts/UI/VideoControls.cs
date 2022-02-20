using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class VideoControls : MonoBehaviour
{

    public YoutubePlayer.YoutubePlayer VideoPlayer;

    // UI
    public Button Reset;
    public Button Play;
    public Button Pause;
    public Button Prepare;

    public InputField YoutubeLink;

    public Button GetPrepare()
    {
        return Prepare;
    }

    public InputField GetYoutubeLinkInput()
    {
        return YoutubeLink;
    }

}
