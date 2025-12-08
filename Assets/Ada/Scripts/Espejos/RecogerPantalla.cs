using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RecogerPantalla : MonoBehaviour
{
    public GameObject Mensaje;
    private bool jugadorCerca = false;

    void Start()
    {
        if (Mensaje != null) Mensaje.SetActive(false);
    }
    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (ControlLaser.vitrinaAbierta && !EstadoJuego.puzzle2Resuelto)
            {
                Recoger();
            }
        }
    }

    void Recoger()
    {
        ControlLaser.pantallaRecogida = true;
        EstadoJuego.puzzle2Resuelto = true;

        StartCoroutine(MostrarYEsconder());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }

    IEnumerator MostrarYEsconder()
    {
        // Mostramos el mensaje
        if (Mensaje != null) Mensaje.SetActive(true);

        // Esperamos 3 segundos
        yield return new WaitForSeconds(3f);

        // Ocultamos el mensaje
        if (Mensaje != null) Mensaje.SetActive(false);
    }
}