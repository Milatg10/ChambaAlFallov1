using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSalud : MonoBehaviour
{
    // Arrastra aquí el archivo VidaData azul de la carpeta Data
    public VidaData datosVida; 

    [Header("Invencibilidad / Parpadeo")]
    public float tiempoInvencible = 1.0f; // Cuánto dura el parpadeo
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
        
        if (datosVida.vidaActual <= 0)
        {
                Debug.Log("¡JUGADOR ELIMINADO!");
                // Aquí iría tu SceneManager.LoadScene...
        }


        StartCoroutine(InvulnerabilidadTemporada());
    }

    IEnumerator InvulnerabilidadTemporada()
    {
        esInvencible = true; // Activamos escudo para no recibir mil golpes a la vez

        // Bucle rápido: Apaga y enciende el dibujo cada 0.1 segundos
        for (float i = 0; i < tiempoInvencible; i += 0.1f)
        {
            if (misGraficos != null) 
            {
                misGraficos.enabled = !misGraficos.enabled; // Interruptor ON/OFF
            }
            yield return new WaitForSeconds(0.1f);
        }

        // Al terminar, asegurarnos de que el dibujo se queda VISIBLE
        if (misGraficos != null) misGraficos.enabled = true;
        
        esInvencible = false; // Quitamos escudo
    }
}