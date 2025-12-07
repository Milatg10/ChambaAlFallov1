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
    private bool estaMuerto = false;
    

    void Start()
    {
        misGraficos = GetComponent<SpriteRenderer>();
        // Opcional: Curar al personaje al entrar en tu minijuego
        // datosVida.vidaActual = datosVida.vidaMaxima; 
        estaMuerto = false;
    }

    void Update()
    {
        // Si ya estamos muertos, no hacemos nada
        if (estaMuerto || datosVida == null) return;

        // 2. VIGILANTE DE MUERTE POR TIEMPO
        // Si la vida llega a cero (por el tiempo o lo que sea), nos morimos
        if (datosVida.vidaActual <= 0)
        {
            Morir();
        }
    }

    // Cambiamos 'int' por 'float' porque VidaData usa floats
    public void RecibirGolpe(float daño)
    {
        // SEGURIDAD: Si por lo que sea el objeto ya está apagado, no hacemos nada
        if (!gameObject.activeInHierarchy) return;

        if (esInvencible) return;

        if (datosVida != null)
        {
            datosVida.vidaActual -= daño;

            // Comprobar muerte
            if (datosVida.vidaActual <= 0)
            {
                Debug.Log("¡JUGADOR ELIMINADO!");
                
                if (GameManagerMila.instance != null)
                {
                    Morir();
                    return;
                }
                
                // Apagamos al personaje
                gameObject.SetActive(false); 
                
            }
        }

        // --- SOLUCIÓN DEL ERROR ---
        // Como hemos puesto el 'return' arriba si morimos,
        // esta línea solo se ejecutará si SEGUIMOS VIVOS.
        StartCoroutine(InvulnerabilidadTemporada());
    }

    // --- FUNCIÓN ÚNICA DE MUERTE ---
    // Da igual si te mata una bala o el reloj, ambos llaman a esto.
    void Morir()
    {
        if (estaMuerto) return; // Seguridad para no morir 2 veces
        estaMuerto = true;

        Debug.Log("¡JUGADOR ELIMINADO!");
                
        // 1. Avisar al Manager
        if (GameManagerMila.instance != null)
        {
            GameManagerMila.instance.GameOver();
        }
                
        // 2. Apagar al personaje
        gameObject.SetActive(false); 
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