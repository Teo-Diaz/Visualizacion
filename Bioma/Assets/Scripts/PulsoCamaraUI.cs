using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PulsoCamaraUI : MonoBehaviour
{
    public Image pulsoAzul;         
    public Button botonExtra;       
    public Button botonExtra1;
    public Button botonExtra2;
    public Button botonPlaneta;
    public float duracionPulso = 0.01f;

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
        botonExtra1.gameObject.SetActive(true);
        botonExtra2.gameObject.SetActive(true);
        botonPlaneta.gameObject.SetActive(true);
    }
}