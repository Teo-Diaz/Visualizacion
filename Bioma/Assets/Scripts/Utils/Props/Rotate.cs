using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField, Min(0)] private float speed;

    private void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime);
    }
}