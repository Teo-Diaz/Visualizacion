using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    private static Action<GameState> OnGameStateHelper;
    private static Action<GamePhase> OnGamePhaseHelper;
    private static Action OnGameNextPhaseHelper;

    [Header("REFERENCES")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LoadingScreenModel loadingModel;

    public static GameState CurrentState { get; private set; } = default;
    public static GamePhase CurrentPhase { get; private set; } = default;

    public static Camera MainCamera { get; private set; }
    public static EventSystem MainEventSystem { get; private set; }

    public static LoadingScreenModel LoadingModel { get; private set; }

    private void Awake()
    {
        if(MainCamera == null) MainCamera = targetCamera ? targetCamera : Camera.main;
        if(MainEventSystem == null) MainEventSystem = EventSystem.current;

        if(LoadingModel  == null) LoadingModel = loadingModel;

        SceneManager.activeSceneChanged += HandleSceneChange;

        OnGameStateHelper += ChangeState;
        OnGamePhaseHelper += ChangePhase;
        OnGameNextPhaseHelper += ChangePhase;

        ChangeState(GameState.Running);
        ChangePhase();

        AudioManager.PlayClip(AudioManager.GetClipData("Theme"));
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= HandleSceneChange;

        OnGameStateHelper -= ChangeState;
        OnGamePhaseHelper -= ChangePhase;
        OnGameNextPhaseHelper -= ChangePhase;
    }

    public static void SetState(GameState newState) => OnGameStateHelper?.Invoke(newState);
    public static void SetPhase(GamePhase newPhase) => OnGamePhaseHelper?.Invoke(newPhase);
    public static void NextPhase() => OnGameNextPhaseHelper?.Invoke();

    #region Behaviors

    private void ChangeState(GameState targetState)
    {
        if (targetState == CurrentState) return;

        CurrentState = targetState;

        OnGameStateChanged?.Invoke(CurrentState);
    }

    private void ChangePhase()
    {
        int index = ((int)CurrentPhase + 1) % Enum.GetValues(typeof(GamePhase)).Length;

        ChangePhase((GamePhase)index);
    }

    private void ChangePhase(GamePhase targetPhase)
    {
        if (targetPhase == CurrentPhase) return;

        CurrentPhase = targetPhase;

        OnGamePhaseChanged?.Invoke(CurrentPhase);
    }

    #endregion

    #region Utility

    private async void HandleSceneChange(Scene oldScene, Scene newScene)
    {
        if(newScene.buildIndex == 0) return;

        LoadingModel?.RequestLoad();

        await SceneCleaner.CleanScene(newScene);

        LoadingModel?.ReleaseLoad();
    }

    #endregion
}