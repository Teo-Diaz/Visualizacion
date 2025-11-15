using UnityEngine;

public class ModelModalScreenModel : BaseModalModel
{
    [Header("REFERENCES")]
    [SerializeField] private Transform modelsParent;

    private Transform _activeModel;

    public override void Init()
    {
        base.Init();

        OnClose.AddListener(ClearActiveModel);
    }

    public override void Show()
    {
        var info = ModalScreenModel.CurrentInfoData;

        if(!info.Model) return;

        _activeModel = modelsParent.Find(info.Model.name);

        _activeModel.gameObject.SetActive(true);

        base.Show();
    }

    public override void Release()
    {
        base.Release();

        OnClose.RemoveListener(ClearActiveModel);
    }

    private void ClearActiveModel()
    {
        if (!_activeModel) return;

        _activeModel.rotation = Quaternion.identity;

        _activeModel.gameObject.SetActive(false);

        _activeModel = null;
    }
}