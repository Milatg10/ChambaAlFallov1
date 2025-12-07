using UnityEngine;
using System.Collections.Generic;

public class Ruleta : MonoBehaviour
{
    [Header("Imagen que gira")]
    public RectTransform ruletaObjetivo;

    [Header("Giro (8 segundos aprox)")]
    public float tiempoGiro = 8f;
    private float tiempoRestante = 0f;

    private float velocidadActual = 0f;
    private bool girando = false;

    [Header("Velocidad")]
    public float velocidadMax = 1000f;

    [Header("Sonido (solo al pulsar)")]
    public AudioSource sonidoGiro;

    [Header("Opciones de la ruleta (cualquier cantidad)")]
    public List<string> nombresOpciones;
    public List<int> valoresOpciones;

    private int sectorGanador = 0;

    void Update()
    {
        if (girando && ruletaObjetivo != null)
        {
            // Reducir tiempo
            tiempoRestante -= Time.deltaTime;

            // Girar con frenado progresivo
            float t = 1f - (tiempoRestante / tiempoGiro); // 0 → 1
            float factorFrenado = Mathf.Lerp(1f, 0f, t * t); // frenado suave

            velocidadActual = velocidadMax * factorFrenado;

            ruletaObjetivo.Rotate(0f, 0f, velocidadActual * Time.deltaTime);

            if (tiempoRestante <= 0)
            {
                girando = false;
                velocidadActual = 0f;
                DeterminarResultado();
            }
        }
    }

    public void GirarRuleta()
    {
        if (!girando)
        {
            girando = true;
            tiempoRestante = tiempoGiro;

            if (sonidoGiro != null)
                sonidoGiro.Play();
        }
    }

    void DeterminarResultado()
    {
        float angulo = ruletaObjetivo.eulerAngles.z % 360f;

        // AHORA ES AUTOMÁTICO
        float gradosPorSector = 360f / nombresOpciones.Count;

        sectorGanador = Mathf.FloorToInt(angulo / gradosPorSector);
        sectorGanador = Mathf.Clamp(sectorGanador, 0, nombresOpciones.Count - 1);

        string nombre = nombresOpciones[sectorGanador];
        int valorReal = valoresOpciones[sectorGanador];

        Debug.Log($"Resultado: Sector {sectorGanador + 1} → {nombre} (valor: {valorReal})");
    }
}
