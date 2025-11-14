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

    public static void ToggleCanvas(CanvasGroup targetCanvas, bool activate, bool interactable = true, bool blockRycasts = true)
    {
        if (!targetCanvas) return;

        targetCanvas.alpha = activate ? 1 : 0;
        targetCanvas.interactable = activate && interactable;
        targetCanvas.blocksRaycasts = activate && blockRycasts;
    }

    public static void TweenCanvas(CanvasGroup targetCanvas, bool activate, float duration)
    {
        duration = Mathf.Abs(duration);

        if(!activate)
        {
            targetCanvas.blocksRaycasts = false;
            targetCanvas.interactable = false;
        }

        LTSeq sequence = LeanTween.sequence(); 
        sequence.append(targetCanvas.LeanAlpha(activate ? 1 : 0, duration));
        sequence.append(() =>
        {
            targetCanvas.blocksRaycasts = activate;
            targetCanvas.interactable = activate;
        });
    }
}