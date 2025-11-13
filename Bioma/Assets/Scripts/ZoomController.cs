using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera camPrincipal;
    public Camera camObjetivo1;
    public Camera camObjetivo2;
    public Camera camObjetivo3;

    [Header("Botones UI")]
    public GameObject botonPrincipal;
    public GameObject botonObjetivo1;
    public GameObject botonObjetivo2;
    public GameObject botonObjetivo3;

    public void ActivarCamPrincipal()
    {
        ActivarCamara(camPrincipal);
        MostrarBoton(botonPrincipal, true); // Siempre visible
        // No se reactiva ningún botón oculto
    }

    public void ActivarCamObjetivo1()
    {
        ActivarCamara(camObjetivo1);
        MostrarBoton(botonPrincipal, true);
        MostrarBoton(botonObjetivo1, false);
    }

    public void ActivarCamObjetivo2()
    {
        ActivarCamara(camObjetivo2);
        MostrarBoton(botonPrincipal, true);
        MostrarBoton(botonObjetivo2, false);
    }

    public void ActivarCamObjetivo3()
    {
        ActivarCamara(camObjetivo3);
        MostrarBoton(botonPrincipal, true);
        MostrarBoton(botonObjetivo3, false);
    }

    private void ActivarCamara(Camera camActiva)
    {
        if (camPrincipal != null) camPrincipal.enabled = (camActiva == camPrincipal);
        if (camObjetivo1 != null) camObjetivo1.enabled = (camActiva == camObjetivo1);
        if (camObjetivo2 != null) camObjetivo2.enabled = (camActiva == camObjetivo2);
        if (camObjetivo3 != null) camObjetivo3.enabled = (camActiva == camObjetivo3);
    }

    private void MostrarBoton(GameObject boton, bool estado)
    {
        if (boton != null)
            boton.SetActive(estado);
    }
}