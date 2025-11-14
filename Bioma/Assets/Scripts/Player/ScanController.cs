using UnityEngine;
using UnityEngine.Events;

public class ScanController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameObject terrainScanner;
    [SerializeField] private Transform spawnPoint;
    [Space]
    [Header("PARAMETERS")]
    [SerializeField, Min(0)] private float duration = 10;
    [SerializeField, Min(0)] private float distance = 500;
    [Space]
    [Header("Events")]
    public UnityEvent OnScanCompleted;

    public void Scan()
    {
        GameObject terrainScanner = Instantiate(this.terrainScanner, spawnPoint.position, Quaternion.identity, transform);

        if(terrainScanner.transform.GetChild(0).TryGetComponent(out ParticleSystem ps))
        {
            var main = ps.main;

            main.startLifetime = duration;
            main.startSize = distance;
        }

        Invoke("ScanCompleted", duration);
        Destroy(terrainScanner, duration + 1);
    }

    private void ScanCompleted() => OnScanCompleted?.Invoke();
}
