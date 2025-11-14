using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private CinemachineInputAxisController cinemachineInputController;

    private void Awake()
    {
        inputProvider.OnFirePerformed += EnableControl;
        inputProvider.OnFireCanceled += DisableControl;
    }

    private void EnableControl() => cinemachineInputController.enabled = true;
    private void DisableControl() => cinemachineInputController.enabled = false;
}