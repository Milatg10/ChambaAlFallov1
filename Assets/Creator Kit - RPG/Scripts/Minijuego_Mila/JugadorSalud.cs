using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSalud : MonoBehaviour
{
    // Arrastra aquí el archivo VidaData azul de la carpeta Data
    public VidaData datosVida; 

    public float tiempoInvencible = 1.0f;
    private bool esInvencible = false;
    private SpriteRenderer misGraficos;

    void Start()
    {
        misGraficos = GetComponent<SpriteRenderer>();
        // Opcional: Curar al personaje al entrar en tu minijuego
        // datosVida.vidaActual = datosVida.vidaMaxima; 
    }

    // Cambiamos 'int' por 'float' porque VidaData usa floats
    public void RecibirGolpe(float daño)
    {
        if (esInvencible) return;

        if (datosVida != null)
        {
            // Restamos vida a la variable global compartida
            datosVida.vidaActual -= daño; 
            Debug.Log("Vida actual: " + datosVida.vidaActual);
        }
        else
        {
            Debug.LogError("¡ERROR! Falta asignar VidaData en el inspector del Jugador.");
        }

        StartCoroutine(InvulnerabilidadTemporada());
    }

    IEnumerator InvulnerabilidadTemporada()
    {
        esInvencible = true;
        // Parpadeo
        for (float i = 0; i < tiempoInvencible; i += 0.1f)
        {
            misGraficos.enabled = !misGraficos.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        misGraficos.enabled = true;
        esInvencible = false;
    }
}