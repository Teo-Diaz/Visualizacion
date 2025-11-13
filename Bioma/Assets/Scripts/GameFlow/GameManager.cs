using System;
using UnityEngine;

public enum GameState
{
    Unload,
    Running,
    Paused
}

public enum GamePhase
{
    None,
    Menu,
    Level1
}

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<GamePhase> OnGamePhaseChanged;

    [Header("REFERENCES")]
    [SerializeField] private Camera targetCamera;

    public static Camera MainCamera { get; private set; }
    public static GameState CurrentState { get; private set; } = default;
    public static GamePhase CurrentPhase { get; private set; } = default;

    private void Awake()
    {
        if(MainCamera == null) MainCamera = targetCamera ? targetCamera : Camera.main;

        ChangeState();
    }

    private void ChangeState()
    {
        OnGameStateChanged?.Invoke(CurrentState);
    }

    [ContextMenu("Change Phase")]
    private void ChangePhase()
    {
        int index = ((int)CurrentPhase + 1) % Enum.GetValues(typeof(GamePhase)).Length;

        CurrentPhase = (GamePhase)index;

        OnGamePhaseChanged?.Invoke(CurrentPhase);
    }
}