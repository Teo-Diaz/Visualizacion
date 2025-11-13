using UnityEngine;

public class ActivarBoton : MonoBehaviour
{
    public GameObject botonObjetivo;

    public void Activar()
    {
        if (botonObjetivo != null)
            botonObjetivo.SetActive(true);
    }
}