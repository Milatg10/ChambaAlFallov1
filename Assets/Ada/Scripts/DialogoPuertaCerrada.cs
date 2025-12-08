using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoPuertaCerrada : MonoBehaviour
{
    [Header("1. UI - Arrastra tus objetos")]
    public GameObject Aviso_Espacio;      // El sprite de la tecla Espacio
    public GameObject Mensaje_Bloqueado;  // El Panel/Texto que dice "Necesitas el objeto"
    private bool estoyEnLaPuerta = false;

    void Start()
    {
        if (Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
    }

    void Update()
    {
        // Si estoy en la puerta y pulso Espacio...
        if (estoyEnLaPuerta && Input.GetKeyDown(KeyCode.Space))
        {
            // ...y no se está mostrando ya el mensaje de error...
            if (Mensaje_Bloqueado == null || !Mensaje_Bloqueado.activeSelf)
            {
                StartCoroutine(MostrarAvisoBloqueado());
            }
        }
    }


    IEnumerator MostrarAvisoBloqueado()
    {
        Debug.Log("Mostrando aviso de puerta bloqueada.");

        // Ocultamos la barra espacio un momento
        if (Aviso_Espacio != null) Aviso_Espacio.SetActive(false);

        // Mostramos el mensaje de "Necesitas el objeto"
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(true);

        // Esperamos 2 segundos leyendo el mensaje
        yield return new WaitForSeconds(2f);

        // Lo quitamos
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);

        // Si seguimos en la puerta, vuelve a salir la barra espacio
        if (estoyEnLaPuerta && Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
    }

    // --- DETECTAR JUGADOR (Triggers) ---

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = true;
            if (Aviso_Espacio != null) Aviso_Espacio.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = false;
            // Apagamos todo al alejarnos
            if (Aviso_Espacio != null) Aviso_Espacio.SetActive(false);
            if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        }
    }
}