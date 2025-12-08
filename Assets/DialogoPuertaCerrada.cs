using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoPuertaCerrada : MonoBehaviour
{
    [Header("1. UI - Arrastra tus objetos")]
    public GameObject Mensaje_Bloqueado;
    private bool estoyEnLaPuerta = false;

    void Start()
    {
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        estoyEnLaPuerta = false;
    }

    void Update()
    {
        if (estoyEnLaPuerta && Input.GetKeyDown(KeyCode.Space) && !EstadoJuego.puzzle1Resuelto) // 
        {
            if (Mensaje_Bloqueado == null || !Mensaje_Bloqueado.activeSelf)
            {
                StartCoroutine(MostrarAvisoBloqueado());
            }
        }
    }


    IEnumerator MostrarAvisoBloqueado()
    {
        // Mostramos el mensaje de "Necesitas el objeto"
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(true);

        // Esperamos 2 segundos leyendo el mensaje
        yield return new WaitForSeconds(2f);

        // Lo quitamos
        if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
    }

    // --- DETECTAR JUGADOR (Triggers) ---

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entró: " + collision.name);
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = false;
            // Apagamos todo al alejarnos
            if (Mensaje_Bloqueado != null) Mensaje_Bloqueado.SetActive(false);
        }
    }
}