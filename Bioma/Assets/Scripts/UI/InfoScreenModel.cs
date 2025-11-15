using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InfoType
{
    Multiple,
    Video,
    Model,
    Image
}

public class InfoScreenModel : BaseModel
{
    [System.Serializable]
    private struct InfoButton
    {
        [SerializeField] private string infoKey;
        [SerializeField] private InfoType infoType;
        [SerializeField] private Button button;

        public string InfoKey => infoKey;
        public InfoType InfoType => infoType;
        public Button Button => button;
    }

    [Header("REFERENCES")]
    [Header("Modals")]
    [SerializeField] private ModalScreenModel modalScreen;
    [Header("Buttons")]
    [SerializeField] private Transform mainWorldButtonsParent;
    [SerializeField] private List<InfoButton> infoButtons = new List<InfoButton>();

    private List<FollowTarget> _items = new List<FollowTarget>();

    public override void Init()
    {
        base.Init();

        mainWorldButtonsParent.GetComponentsInChildren(true, _items);

        foreach (var button in infoButtons)
        {
            if (!button.Button || string.IsNullOrEmpty(button.InfoKey))
                continue;

            button.Button.onClick.AddListener(() => modalScreen.RequestInfo(button.InfoKey, button.InfoType));
        }
    }

    private void LateUpdate()
    {
        SortByDistance();
    }

    public override void Release()
    {
        base.Release();

        foreach (var button in infoButtons)
        {
            if (!button.Button)
                continue;

            button.Button.onClick.RemoveAllListeners();
        }
    }

    private void SortByDistance()
    {
        if (Camera.main == null) return;

        _items.Sort((a, b) =>
        {
            float distA = Vector3.SqrMagnitude(a.Target.position - Camera.main.transform.position);
            float distB = Vector3.SqrMagnitude(b.Target.position - Camera.main.transform.position);

            return distB.CompareTo(distA);
        });

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].transform.SetSiblingIndex(i);
        }
    }
}