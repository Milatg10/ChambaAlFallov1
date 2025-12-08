using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_Casa : MonoBehaviour
{
    [Header("1. UI - Arrastra tus objetos")]
    public GameObject Aviso_Espacio;      // El sprite de la tecla Espacio
    public GameObject Mensaje_Bloqueado;  // El Panel/Texto que dice "Necesitas el objeto"

    [Header("2. Configuración")]
    public LevelLoader Pantalla_carga;    // Tu script de carga
    public string nombreEscenaDestino;    // Nombre exacto de la escena a cargar
    public int objetosNecesarios = 1;     // Cuántos objetos necesitas (1)

    public MundoData mundoData;                    // Tu script de contador global
    private bool estoyEnLaPuerta = false;

    void Start()
    {
        // Al empezar, ocultamos los avisos para que no molesten
        if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
    }

    void Update()
    {   
        // Si estoy en la puerta y pulso Espacio...
        if (estoyEnLaPuerta && Input.GetKeyDown(KeyCode.Space))
        {
            // ...y no se está mostrando ya el mensaje de error...
            if (Mensaje_Bloqueado == null || !Mensaje_Bloqueado.activeSelf)
            {
                IntentarEntrar();
            }
        }
    }

    void IntentarEntrar()
    {

        if (mundoData != null)
        {
            Debug.Log("Tienes " + mundoData.objetosRecogidos + " objetos.");
            // 2. DECISIÓN AUTOMÁTICA
            if (mundoData.objetosRecogidos >= objetosNecesarios)
            {
                // TIENES EL OBJETO -> ¡ADENTRO!
                Debug.Log("¡Puerta abierta! Entrando...");
                if(Pantalla_carga != null)
                {
                    Pantalla_carga.CargarNivel(nombreEscenaDestino);
                }
            }
            else
            {
                // NO TIENES EL OBJETO -> Muestra mensaje de error
                Debug.Log("¡Cerrado! Faltan objetos.");
                StartCoroutine(MostrarAvisoBloqueado());
            }
        }
        else
        {
            Debug.LogError("¡ERROR! No encuentro el script 'ContadorItems' en la escena 'GameManager'.");
        }
    }

    IEnumerator MostrarAvisoBloqueado()
    {   
        Debug.Log("Mostrando aviso de puerta bloqueada.");
        
        // Ocultamos la barra espacio un momento
        if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
        
        // Mostramos el mensaje de "Necesitas el objeto"
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(true);
        
        // Esperamos 2 segundos leyendo el mensaje
        yield return new WaitForSeconds(2f); 
        
        // Lo quitamos
        if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);

        // Si seguimos en la puerta, vuelve a salir la barra espacio
        if (estoyEnLaPuerta && Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
    }

    // --- DETECTAR JUGADOR (Triggers) ---

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("¡CHOQUE DETECTADO con: " + collision.name + "!");
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = true;
            if(Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = false;
            // Apagamos todo al alejarnos
            if(Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
            if(Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        }
    }
}