using UnityEngine;
using UnityEngine.UI;

public class ToggleImagenUI : MonoBehaviour
{
    public Image imagenObjetivo; // Asigna la imagen en el Inspector

    private bool imagenActiva = false;

    public void AlternarImagen()
    {
        imagenActiva = !imagenActiva;
        imagenObjetivo.gameObject.SetActive(imagenActiva);
    }
}