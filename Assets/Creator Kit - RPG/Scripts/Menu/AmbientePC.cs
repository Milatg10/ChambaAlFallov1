using System.Collections;
using UnityEngine;

public class AmbientePC : MonoBehaviour
{
    [Header("Sonidos")]
    public AudioSource sonidoTeclado;
    public AudioSource sonidoClicks;

    [Header("Tiempo entre sonidos")]
    public Vector2 pausaEntreTeclado = new Vector2(0.5f, 1.5f);
    public Vector2 pausaEntreClicks = new Vector2(0.5f, 1.5f);

    void Start()
    {
        StartCoroutine(ReproducirAmbiente());
    }

    IEnumerator ReproducirAmbiente()
    {
        while (true)
        {
            // --- 1. Sonido de TECLADO ---
            if (sonidoTeclado != null)
                sonidoTeclado.Play();

            // Espera un tiempo al azar
            yield return new WaitForSeconds(Random.Range(pausaEntreTeclado.x, pausaEntreTeclado.y));

            // --- 2. Sonido de CLICKS ---
            if (sonidoClicks != null)
                sonidoClicks.Play();

            // Espera otro tiempo al azar
            yield return new WaitForSeconds(Random.Range(pausaEntreClicks.x, pausaEntreClicks.y));
        }
    }
}