using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static event Action OnSceneLoading;
    public static event Action OnSceneLoaded;

    [Header("PARAMETERS")]
    [SerializeField, Min(0)] private float minimumDelay = 1.5f;

    private Scene _currentScene;
    private bool _isChangingScene = false;

    private void Awake()
    {
        GameManager.OnGamePhaseChanged += HandlePhaseChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGamePhaseChanged -= HandlePhaseChange;
    }

    private void HandlePhaseChange(GamePhase newPhase)
    {
        switch(newPhase)
        {
            case GamePhase.Menu:
                ChangeScene(1);
                break;
            case GamePhase.Level1:
                ChangeScene(2);
                break;
        }
    }

    public async void ChangeScene(int targetSceneIndex)
    {
        if (_isChangingScene)
            return;

        OnSceneLoading?.Invoke();
        _isChangingScene = true;

        GameManager.LoadingModel?.RequestLoad();

        await Task.Delay((int)(minimumDelay * 1000));

        if (_currentScene.IsValid())
            await UnloadScene(_currentScene);

        _currentScene = await LoadScene(targetSceneIndex);

        SceneManager.SetActiveScene(_currentScene);

        OnSceneLoaded?.Invoke();
        _isChangingScene = false;

        GameManager.LoadingModel?.ReleaseLoad();
    }

    private async Task UnloadScene(Scene scene)
    {
        var op = SceneManager.UnloadSceneAsync(scene);
        await WaitForAsyncOperation(op);
    }

    private async Task<Scene> LoadScene(int targetSceneIndex)
    {
        var op = SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Additive);
        await WaitForAsyncOperation(op);

        Scene loaded = SceneManager.GetSceneByBuildIndex(targetSceneIndex);
        return loaded;
    }

    private async Task WaitForAsyncOperation(AsyncOperation op)
    {
        while (!op.isDone)
            await Task.Yield();
    }
}