using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoModalScreenModel : BaseModalModel
{
    private enum ModalType
    {
        Image,
        Video,
        Model
    }

    [Header("REFERENCES")]
    [Header("Modals")]
    [SerializeField] private ImageModalScreenModel imageModal;
    [SerializeField] private VideoModalScreenModel videoModal;
    [SerializeField] private ModelModalScreenModel modelModal;
    [Header("Content")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button videoButton;
    [SerializeField] private Button modelButton;
    [SerializeField] private Button imageButton;

    private BaseModalModel _currentModal;

    public override void Init()
    {
        base.Init();

        imageButton.onClick.AddListener(() => OpenModal(ModalType.Image));
        videoButton.onClick.AddListener(() => OpenModal(ModalType.Video));
        modelButton.onClick.AddListener(() => OpenModal(ModalType.Model));
    }

    public override void Show()
    {
        if (_currentModal) _currentModal.OnClose.RemoveListener(Show);

        var info = ModalScreenModel.CurrentInfoData;

        title.text = info.Title;
        description.text = info.Description;

        imageButton.gameObject.SetActive(info.Image);
        videoButton.gameObject.SetActive(
            !string.IsNullOrWhiteSpace(info.VideoName) && 
            !string.IsNullOrEmpty(info.VideoName));
        modelButton.gameObject.SetActive(info.Model);

        base.Show();
    }

    public override void Hide()
    {
        if (_currentModal)
        {
            _currentModal.OnClose.RemoveListener(Hide);
            _currentModal = null;
        }

        base.Hide();
    }

    public override void Release()
    {
        base.Release();

        imageButton.onClick.RemoveAllListeners();
        videoButton.onClick.RemoveAllListeners();
        modelButton.onClick.RemoveAllListeners();
    }

    private void OpenModal(ModalType type)
    {
        switch (type)
        {
            case ModalType.Image:
                _currentModal = imageModal;
                imageModal.Show();
                imageModal.OnClose.AddListener(Show);
                break;
            case ModalType.Video:
                _currentModal = videoModal;
                videoModal.Show();
                videoModal.OnClose.AddListener(Show);
                break;
            case ModalType.Model:
                _currentModal = modelModal;
                modelModal.Show();
                modelModal.OnClose.AddListener(Show);
                break;
        }
    }
}