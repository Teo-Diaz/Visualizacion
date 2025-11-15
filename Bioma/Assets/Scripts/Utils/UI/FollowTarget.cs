using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Transform followTarget;

    public Transform Target => followTarget;

    private void Update()
    {
        if (followTarget == null) return;

        transform.position = Camera.main.WorldToScreenPoint(followTarget.position);
    }
}
