using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenModal : BaseModel
{
    [Header("REFERENCES")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private RectTransform settingsPanel;

    private bool _visible; 

    public override void Init()
    {
        settingsButton.onClick.AddListener(ToggleVisibility);
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
}
