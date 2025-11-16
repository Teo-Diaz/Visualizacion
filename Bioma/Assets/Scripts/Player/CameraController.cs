using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private InputProvider inputProvider;
    [SerializeField] private CinemachineInputAxisController cinemachineInputController;

    bool _pointerOnUI;

    private void Awake()
    {
        inputProvider.OnFirePerformed += EnableControl;
        inputProvider.OnFireCanceled += DisableControl;
    }

    private void Update()
    {
        _pointerOnUI = EventSystem.current.IsPointerOverGameObject();
    }

    private void OnDestroy()
    {
        inputProvider.OnFirePerformed -= EnableControl;
        inputProvider.OnFireCanceled -= DisableControl;
    }

    private void EnableControl()
    {
        if (!_pointerOnUI) 
            cinemachineInputController.enabled = true; 
    }

    private void DisableControl() => cinemachineInputController.enabled = false;
}