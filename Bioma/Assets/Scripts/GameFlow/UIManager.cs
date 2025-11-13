using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static void ToggleCanvas(CanvasGroup targetCanvas)
    {
        if(!targetCanvas) return;

        bool targetState = targetCanvas.alpha > 0 ? false : true;

        targetCanvas.alpha = targetState ? 1 : 0;
        targetCanvas.interactable = targetState;
        targetCanvas.blocksRaycasts = targetState;
    }

    public static void ToggleCanvas(CanvasGroup targetCanvas, bool activate, bool interactable, bool blockRycasts = true)
    {
        if (!targetCanvas) return;

        targetCanvas.alpha = activate ? 1 : 0;
        targetCanvas.interactable = activate && interactable;
        targetCanvas.blocksRaycasts = activate && blockRycasts;
    }
}