using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageModalScreenModel : BaseModalModel
{
    [Header("REFERENCES")]
    [SerializeField] private Image mainImage;

    public override void Show()
    {
        var info = ModalScreenModel.CurrentInfoData;

        mainImage.sprite = info.Image;

        base.Show();
    }
}