using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenModal : BaseModel
{
    [Header("REFERENCES")]
    [Header("Panel Control")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private RectTransform settingsPanel;
    [Header("Volume")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private bool _visible; 

    public override void Init()
    {
        settingsButton.onClick.AddListener(ToggleVisibility);

        masterVolumeSlider.value = 0.5f;
        musicVolumeSlider.value = 0.1f;
        sfxVolumeSlider.value = 0.5f;

        masterVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("MasterVolume", value));
        musicVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("MusicVolume", value));
        sfxVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("SFXVolume", value));
    }

    public override void Show()
    {
        LeanTween.cancel(settingsPanel);

        LeanTween.value(settingsPanel.gameObject, settingsPanel.anchoredPosition.x, -settingsPanel.rect.width, 0.5f)
            .setEase(LeanTweenType.easeOutBack)
            .setOnUpdate((float position) =>
            {
                settingsPanel.anchoredPosition = new Vector3(
                    position,
                    settingsPanel.anchoredPosition.y);
            });

        GameManager.SetState(GameState.Paused);

        _visible = true;
    }

    public override void Hide()
    {
        LeanTween.cancel(settingsPanel);

        LeanTween.value(settingsPanel.gameObject, settingsPanel.anchoredPosition.x, 0, 0.5f)
            .setEase(LeanTweenType.easeInBack)
            .setOnUpdate((float position) =>
            {
                settingsPanel.anchoredPosition = new Vector3(
                    position,
                    settingsPanel.anchoredPosition.y);
            });

        GameManager.SetState(GameState.Running);

        _visible = false;
    }

    public override void Release()
    {
        settingsButton.onClick.RemoveAllListeners();
    }

    private void ToggleVisibility()
    {
        if (_visible) Hide();
        else Show();
    }

    private void OnChangeVolume(string volumeChannel, float value)
    {
        AudioManager.ChangeVolume(volumeChannel, value);
    }
}
