using UnityEngine;
using UnityEngine.UI;

public class HUDScreenModel : BaseModel
{
    [Header("REFERENCES")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button scanButton;
    [SerializeField] private RectTransform hintPanel;

    private bool _visible;

    public override void Init()
    {
        returnButton.onClick.AddListener(Return);
        hintButton.onClick.AddListener(ToggleVisibility);
    }

    public override void Show()
    {
        LeanTween.cancel(hintPanel);

        LeanTween.value(hintPanel.gameObject, hintPanel.anchoredPosition.y, -hintPanel.rect.height, 0.5f)
            .setEase(LeanTweenType.easeOutBack)
            .setOnUpdate((float position) =>
            {
                hintPanel.anchoredPosition = new Vector3(
                    hintPanel.anchoredPosition.x,
                    position);
            });

        _visible = true;
    }

    public override void Hide()
    {
        LeanTween.cancel(hintPanel);

        LeanTween.value(hintPanel.gameObject, hintPanel.anchoredPosition.y, 0, 0.5f)
            .setEase(LeanTweenType.easeInBack)
            .setOnUpdate((float position) =>
            {
                hintPanel.anchoredPosition = new Vector3(
                    hintPanel.anchoredPosition.x,
                    position);
            });

        _visible = false;
    }

    public override void Release()
    {
        returnButton.onClick.RemoveAllListeners();
        hintButton.onClick.RemoveAllListeners();
    }

    private void Return()
    {
        GameManager.SetPhase(GamePhase.Menu);
    }

    private void ToggleVisibility()
    {
        if (_visible) Hide();
        else Show();
    }
}