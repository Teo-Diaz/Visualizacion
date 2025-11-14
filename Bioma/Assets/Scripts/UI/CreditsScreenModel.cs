using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CreditsScreenModel : BaseModel
{
    [Header("REFERENCES")]
    [SerializeField] private Volume globalVolume;

    private DepthOfField _dof;

    public override void Init()
    {
        globalVolume.profile.TryGet(out _dof);
    }

    public override void Show()
    {
        base.Show();

        _dof.active = true;
    }

    public override void Hide()
    {
        base.Hide();

        _dof.active = false;
    }

    public override void Release()
    {
    }
}