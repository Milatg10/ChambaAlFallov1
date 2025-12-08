using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo_espejos : MonoBehaviour
{
    public GameObject Mensaje;

    // Variable para recordar si ya hemos pasado por aquí
    private bool yaSeMostro = false;

    void Start()
    {
        // Nos aseguramos de que el mensaje empiece oculto
        if (Mensaje != null) Mensaje.SetActive(false);
    }

    // Solo usamos esto, nada de Update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Comprobamos si es el Player Y si NO se ha mostrado antes
        if (collision.CompareTag("Player") && !yaSeMostro)
        {
            // 2. Activamos la "memoria" para que no vuelva a entrar aquí nunca más
            yaSeMostro = true;

            // 3. Iniciamos la cuenta atrás
            if (!EstadoJuego.puzzle2Resuelto)
            {
                StartCoroutine(MostrarYEsconder());
            }

        }
    }

    IEnumerator MostrarYEsconder()
    {
        // Mostramos el mensaje
        if (Mensaje != null) Mensaje.SetActive(true);

        // Esperamos 3 segundos
        yield return new WaitForSeconds(3f);

        // Ocultamos el mensaje
        if (Mensaje != null) Mensaje.SetActive(false);

        // OPCIONAL: Destruimos este script o el trigger para limpiar la escena
        // Destroy(this); // Esto borra el script para ahorrar memoria
    }
}