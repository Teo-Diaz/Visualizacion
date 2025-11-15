using UnityEngine;

public class ModalScreenModel : BaseModel
{
    [Header("REFERENCES")]
    [Header("Bank")]
    [SerializeField] private InfoBank bank;
    [Header("Modals")]
    [SerializeField] private InfoModalScreenModel infoModal;
    [SerializeField] private ImageModalScreenModel imageModal;
    [SerializeField] private VideoModalScreenModel videoModal;
    [SerializeField] private ModelModalScreenModel modelModal;

    public static InfoData CurrentInfoData { get; private set; }

    private BaseModalModel _currentModal;

    public override void Hide()
    {
        if (_currentModal)
        {
            _currentModal.OnClose.RemoveListener(Hide);
            _currentModal = null;
        }

        base.Hide();
    }

    public void RequestInfo(string infoKey, InfoType type)
    {
        if (!bank.InfoDatas.ContainsKey(infoKey)) return;

        CurrentInfoData = bank.InfoDatas[infoKey];

        Show();

        switch (type)
        {
            case InfoType.Multiple:
                _currentModal = infoModal;
                infoModal.Show();
                infoModal.OnClose.AddListener(Hide);
                break;
            case InfoType.Image:
                _currentModal = imageModal;
                imageModal.Show();
                imageModal.OnClose.AddListener(Hide);
                break;
            case InfoType.Video:
                _currentModal = videoModal;
                videoModal.Show();
                videoModal.OnClose.AddListener(Hide);
                break;
            case InfoType.Model:
                _currentModal = modelModal;
                modelModal.Show();
                modelModal.OnClose.AddListener(Hide);
                break;
        }
    }
}
