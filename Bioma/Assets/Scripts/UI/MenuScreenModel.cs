using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MenuScreenModel : BaseModel
{
    [Header("REFERENCES")]
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [Header("Teleport Sequence")]
    [SerializeField] private CinemachineCamera princiaplCamera;
    [SerializeField] private CanvasGroup teleportBurst;

    public override void Init()
    {
        startButton.onClick.AddListener(StartTeleportSequence);
    }

    public override void Release()
    {
        startButton.onClick.RemoveAllListeners();
    }

    private void StartTeleportSequence()
    {
        StartCoroutine(TeleportSequence());
    }

    private IEnumerator TeleportSequence()
    {
        UIManager.TweenCanvas(MainCanvasGroup, false, 0.5f);

        yield return new WaitForSeconds(0.5f);

        AudioManager.PlayClipOneShot(AudioManager.GetClipData("Teleport"));

        LeanTween.value(gameObject, 60f, 179f, 2f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnUpdate((float fov) =>
            {
                princiaplCamera.Lens.FieldOfView = fov;
            });

        yield return new WaitForSeconds(0.5f);

        UIManager.TweenCanvas(teleportBurst, true, 1.5f);

        yield return new WaitForSeconds(1.5f);

        GameManager.NextPhase();
    }
}
