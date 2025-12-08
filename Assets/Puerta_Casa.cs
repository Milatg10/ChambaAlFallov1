using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para el SceneManager por si falla el LevelLoader

public class Puerta_Casa : MonoBehaviour
{
    [Header("1. Datos Globales")]
    public MundoData datosMundo; // Arrastra aquí tu ScriptableObject o componente MundoData

    [Header("2. UI - Interfaz")]
    public GameObject Aviso_Espacio;      // El sprite/texto de "Presiona Espacio"
    public GameObject Mensaje_Bloqueado;  // El panel de "Te faltan objetos"

    [Header("3. Configuración de Carga")]
    public LevelLoader Pantalla_carga;    // Tu script de transición (opcional)
    public string nombreEscenaDestino;    // Nombre exacto de la escena a la que vas
    public int objetosNecesarios = 1;     // Cantidad requerida para entrar

    [Header("4. Sistema de Guardado (Retorno)")]
    [Tooltip("Punto donde aparecerá el jugador al volver. Si se deja vacío, usa la posición actual.")]
    public Transform puntoDeRetorno;      

    // Variables internas
    private bool estoyEnLaPuerta = false;

    void Start()
    {
        // Al empezar, ocultamos los avisos para asegurar que la pantalla esté limpia
        if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
    }

    void Update()
    {   
        // Si el jugador está en el trigger y pulsa Espacio
        if (estoyEnLaPuerta && Input.GetKeyDown(KeyCode.Space))
        {
            // Solo intentamos entrar si no se está mostrando ya el mensaje de error
            if (Mensaje_Bloqueado == null || !Mensaje_Bloqueado.activeSelf)
            {
                IntentarEntrar();
            }
        }
    }

    void IntentarEntrar()
    {
        // 1. Verificación de seguridad: ¿Existe el archivo de datos?
        if (datosMundo != null)
        {
            Debug.Log("Tienes " + datosMundo.objetosRecogidos + " objetos recogidos. Necesitas: " + objetosNecesarios);

            // 2. COMPROBACIÓN: ¿Tienes suficientes objetos?
            if (datosMundo.objetosRecogidos >= objetosNecesarios)
            {
                // --- APROBADO: TIENES LOS OBJETOS ---
                Debug.Log("¡Condición cumplida! Abriendo puerta...");

                // ========================================================
                // CÓDIGO DE GUARDADO (PLAYERPREFS)
                // Guardamos la posición para saber dónde volver después
                // ========================================================
                if (puntoDeRetorno != null)
                {
                    PlayerPrefs.SetFloat("PosicionX", puntoDeRetorno.position.x);
                    PlayerPrefs.SetFloat("PosicionY", puntoDeRetorno.position.y);
                    PlayerPrefs.SetFloat("PosicionZ", puntoDeRetorno.position.z);
                }
                else
                {
                    // Si no definiste un punto manual, usamos la posición actual del jugador
                    GameObject jugador = GameObject.FindGameObjectWithTag("Player");
                    if (jugador != null)
                    {
                        PlayerPrefs.SetFloat("PosicionX", jugador.transform.position.x);
                        // Restamos un poco a la Y para evitar que aparezca flotando o atascado si es necesario
                        PlayerPrefs.SetFloat("PosicionY", jugador.transform.position.y); 
                        PlayerPrefs.SetFloat("PosicionZ", jugador.transform.position.z);
                    }
                }

                // Marcamos que venimos de una puerta/minijuego para la lógica de la siguiente escena
                PlayerPrefs.SetInt("VieneDelMinijuego", 1);
                PlayerPrefs.Save(); // Forzamos el guardado
                // ========================================================

                // 3. CARGAR NIVEL
                if(Pantalla_carga != null)
                {
                    Pantalla_carga.CargarNivel(nombreEscenaDestino);
                }
                else
                {
                    // Fallback: Si no hay LevelLoader, usamos la carga estándar de Unity
                    SceneManager.LoadScene(nombreEscenaDestino);
                }
            }
            else
            {
                // --- DENEGADO: FALTAN OBJETOS ---
                Debug.Log("¡Puerta cerrada! Faltan objetos.");
                StartCoroutine(MostrarAvisoBloqueado());
            }
        }
        else
        {
            Debug.LogError("¡ERROR CRÍTICO! No has asignado el 'MundoData' en el inspector de esta puerta.");
        }
    }

    IEnumerator MostrarAvisoBloqueado()
    {   
        // 1. Ocultamos la indicación de "Espacio" temporalmente
        if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
        
        // 2. Mostramos el mensaje de "Bloqueado"
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(true);
        
        // 3. Esperamos 2 segundos
        yield return new WaitForSeconds(2f); 
        
        // 4. Revertimos los cambios
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        
        // Si el jugador no se ha ido, volvemos a mostrar la opción de pulsar espacio
        if (estoyEnLaPuerta && Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
    }

    // --- DETECCIÓN DE COLISIONES ---

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Aceptamos tanto "Player" como "Character" para evitar errores si cambiaste el Tag
        if (collision.CompareTag("Player") || collision.CompareTag("Character"))
        {
            Debug.Log("Jugador cerca de la puerta: " + collision.name);
            estoyEnLaPuerta = true;
            if(Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Character"))
        {
            estoyEnLaPuerta = false;
            
            // Apagamos toda la UI al alejarse
            if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
            if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        }
    }
}