using UnityEngine;
using UnityEngine.Video;

public class VideoModalScreenModel : BaseModalModel
{
    [Header("REFERENCES")]
    [SerializeField] private VideoPlayer videoPlayer;

    public override void Show()
    {
        var info = ModalScreenModel.CurrentInfoData;

#if UNITY_EDITOR

        string videoPath = $"{Application.streamingAssetsPath}/{info.VideoName}.mp4";

#else

        string videoPath = $"https://teo-diaz.github.io/Visualizacion/videos/{info.VideoName}.mp4";

#endif

        videoPlayer.url = videoPath;

        base.Show();

        videoPlayer.Play();
    }

    public override void Hide()
    {
        videoPlayer.Stop();

        base.Hide();
    }
}