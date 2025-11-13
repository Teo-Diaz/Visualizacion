using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonInfo
    {
        [SerializeField] private RectTransform targetRect;
        [SerializeField] private Vector3 regularSize;
        [SerializeField] private Vector3 hoverSize;
        [SerializeField] private Vector3 clickedSize;

        public RectTransform TargetRect => targetRect;
        public Vector3 RegularSize => regularSize;
        public Vector3 HoverSize => hoverSize;
        public Vector3 ClickedSize => clickedSize;
    }

    [Header("REFERENCES")]
    [SerializeField] private List<ButtonInfo> buttons = new List<ButtonInfo>();

    private Dictionary<RectTransform, LTSeq> activeSequence = new Dictionary<RectTransform, LTSeq>();
    private RectTransform _focusedButton;

    public void Hover(RectTransform sender)
    {
        if (!sender) return;

        ButtonInfo targetInfo = GetButtonInfo(sender);

        if (!targetInfo.TargetRect) return;

        if (_focusedButton) LeanTween.cancel(_focusedButton);

        sender.LeanScale(targetInfo.HoverSize, 0.1f);

        _focusedButton = sender;
    }

    public void Leave(RectTransform sender)
    {
        if (!sender) return;

        ButtonInfo targetInfo = GetButtonInfo(sender);

        if (!targetInfo.TargetRect) return;

        if (_focusedButton)
        {
            LeanTween.cancel(_focusedButton);
            if(_focusedButton == sender) _focusedButton = null;
        }

        sender.LeanScale(targetInfo.RegularSize, 0.1f);
    }

    public void Click(RectTransform sender)
    {
        if (!sender) return;

        ButtonInfo targetInfo = GetButtonInfo(sender);

        if (!targetInfo.TargetRect) return;

        activeSequence.Remove(sender);
        LeanTween.cancel(sender);

        LTSeq sequence = LeanTween.sequence(true);

        activeSequence.Add(sender, sequence);

        sequence.append(sender.LeanScale(targetInfo.ClickedSize, 0.05f));
        sequence.append(0.1f);
        sequence.append(sender.LeanScale(targetInfo.RegularSize, 0.05f));
        sequence.append(() => CleanSequence(sender));
    }

    private ButtonInfo GetButtonInfo(RectTransform sender) => buttons.FirstOrDefault(b => b.TargetRect == sender);
    private void CleanSequence(RectTransform sender)
    {
        activeSequence.Remove(sender);
        if(_focusedButton == sender) Hover(sender);
    }
}
