using UnityEngine;
using UnityEngine.Events;

public abstract class BaseModalModel : BaseModel
{
    [Header("BASE")]
    [Header("Events")]
    [Space]
    public UnityEvent OnClose;

    protected RectTransform MyRect {  get; private set; }

    public override void Init()
    {
        base.Init();

        MyRect = transform as RectTransform;

        MyRect.anchoredPosition = new Vector3(MyRect.anchoredPosition.x, Screen.height + MyRect.rect.height);
    }

    public override void Show()
    {
        base.Show();
        
        LeanTween.value(gameObject, MyRect.anchoredPosition.y, 0, 0.5f)
            .setEase(LeanTweenType.easeOutBack)
            .setOnUpdate((float position) =>
            {
                MyRect.anchoredPosition = new Vector3(
                    MyRect.anchoredPosition.x,
                    position);
            });
    }

    public override void Hide()
    {
        LTSeq sequence = LeanTween.sequence();

        sequence.append(LeanTween.value(gameObject, MyRect.anchoredPosition.y, Screen.height + MyRect.rect.height, 0.5f)
            .setEase(LeanTweenType.easeInBack)
            .setOnUpdate((float position) =>
            {
                MyRect.anchoredPosition = new Vector3(
                    MyRect.anchoredPosition.x,
                    position);
            }));

        sequence.append(base.Hide);
        sequence.append(() => OnClose?.Invoke());
    }
}