using UnityEngine;

public class ZoomController : MonoBehaviour
{
    public Transform zonaObjetivo;
    public float velocidadZoom = 2f;
    public float distanciaZoom = 5f;

    private bool hacerZoom = false;
    private bool regresarZoom = false;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        // Guardamos la posición y rotación inicial de la cámara
        posicionInicial = Camera.main.transform.position;
        rotacionInicial = Camera.main.transform.rotation;
    }

    void Update()
    {
        if (hacerZoom && zonaObjetivo != null)
        {
            Vector3 destino = zonaObjetivo.position - zonaObjetivo.forward * distanciaZoom;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, destino, Time.deltaTime * velocidadZoom);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.LookRotation(zonaObjetivo.position - Camera.main.transform.position), Time.deltaTime * velocidadZoom);
        }

        if (regresarZoom)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, posicionInicial, Time.deltaTime * velocidadZoom);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rotacionInicial, Time.deltaTime * velocidadZoom);
        }
    }

    public void ZoomToTarget()
    {
        hacerZoom = true;
        regresarZoom = false;
    }

    public void RegresarZoom()
    {
        hacerZoom = false;
        regresarZoom = true;
    }
}