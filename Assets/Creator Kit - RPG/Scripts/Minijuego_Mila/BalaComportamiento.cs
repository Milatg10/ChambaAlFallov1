using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaComportamiento : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoDeVida = 5f; 

    void Start()
    {
        // Se destruye sola a los 2 segundos para no llenar el juego de basura
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        // Moverse hacia "arriba" (su eje Y local)
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Si choca con un enemigo
        if (other.CompareTag("Bug"))
        {
            BugVida vidaScript = other.GetComponent<BugVida>();
            if (vidaScript != null)
            {
                vidaScript.RecibirDañoPublico(); 
            }
            Destroy(gameObject); // La bala desaparece al dar
        }
        // 2. Si choca con cualquier cosa que NO sea el personaje ("Character")
        // AQUÍ ESTÁ EL CAMBIO: Antes ponía "Player", ahora "Character"
        else if (!other.CompareTag("Character")) 
        {
             Destroy(gameObject); // Chocó con una pared o mueble
        }
    }
}