using UnityEngine;
using UnityEngine.Events;

public class ScanController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameObject terrainScanner;
    [SerializeField] private Transform fromPoint;
    [Space]
    [Header("PARAMETERS")]
    [SerializeField, Min(0)] private float duration = 7;
    [SerializeField, Min(0)] private float distance = 16;
    [Space]
    [Header("Events")]
    public UnityEvent OnScanCompleted;

    private Vector3 _spawnPosition;

    public void Scan()
    {
        _spawnPosition = transform.position;

        var dir = (fromPoint.position - transform.position).normalized;

        _spawnPosition += (distance/2) * dir;

        GameObject terrainScanner = Instantiate(this.terrainScanner, _spawnPosition, Quaternion.identity, transform);

        if(terrainScanner.transform.GetChild(0).TryGetComponent(out ParticleSystem ps))
        {
            var main = ps.main;

            main.startLifetime = duration;
            main.startSize = (distance)*2;
        }

        AudioManager.PlayClipOneShot(AudioManager.GetClipData("Scan"));

        Invoke("ScanCompleted", duration);
        Destroy(terrainScanner, duration + 1);
    }

    private void ScanCompleted() => OnScanCompleted?.Invoke();

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        var spawnPosition = transform.position;

        var dir = (fromPoint.position - transform.position).normalized;

        spawnPosition += (distance / 2) * dir;

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(spawnPosition, distance);
    }

#endif
}
