using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PulsoCamaraUI : MonoBehaviour
{
    public Image pulsoAzul;         // Asigna el Image azul en el Inspector
    public Button botonExtra;       // Asigna el botón oculto
    public float duracionPulso = 0.5f;

    public void ActivarPulso()
    {
        StartCoroutine(PulsoYBoton());
    }

    private IEnumerator PulsoYBoton()
    {
        pulsoAzul.gameObject.SetActive(true);

        // Fade in
        for (float t = 0; t < duracionPulso; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 0.6f, t / duracionPulso);
            pulsoAzul.color = new Color(0f, 0.48f, 1f, alpha);
            yield return null;
        }

        // Fade out
        for (float t = 0; t < duracionPulso; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0.6f, 0f, t / duracionPulso);
            pulsoAzul.color = new Color(0f, 0.48f, 1f, alpha);
            yield return null;
        }

        pulsoAzul.gameObject.SetActive(false);
        botonExtra.gameObject.SetActive(true);
    }
}