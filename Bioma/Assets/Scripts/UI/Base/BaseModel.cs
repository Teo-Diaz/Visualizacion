using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseModel : MonoBehaviour
{
    public CanvasGroup MainCanvasGroup {  get; private set; }

    private void Awake()
    {
        MainCanvasGroup = GetComponent<CanvasGroup>();

        Init();
    }

    private void OnDestroy()
    {
        Release();
    }

    public abstract void Init();

    public virtual void Show()
    {
        UIManager.ToggleCanvas(MainCanvasGroup, true);
    }
    public virtual void Hide()
    {
        UIManager.ToggleCanvas(MainCanvasGroup, false);
    }

    public abstract void Release();
}
