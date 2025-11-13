using UnityEngine;

public class OcultarImagen : MonoBehaviour
{
    public GameObject imagenUI;

    public void Ocultar()
    {
        if (imagenUI != null)
            imagenUI.SetActive(false);
    }
}