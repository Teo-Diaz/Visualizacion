using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public static class SceneCleaner
{
    public static Task CleanScene(Scene scene)
    {
        var roots = scene.GetRootGameObjects();

        foreach (var go in roots)
        {
            var cams = go.GetComponentsInChildren<Camera>(true);

            foreach (var cam in cams)
            {
                if(!cam.CompareTag("MainCamera")) continue;

                if(cam != GameManager.MainCamera)
                    Object.Destroy(cam.gameObject);
            }

            var systems = go.GetComponentsInChildren<EventSystem>(true);
            foreach (var es in systems)
            {
                if(es != GameManager.MainEventSystem) 
                    Object.Destroy(es.gameObject);
            }
        }

        return Task.CompletedTask;
    }
}