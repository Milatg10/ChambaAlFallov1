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
        // CAMBIO AQUÍ: Usamos .right porque el texto se lee de izquierda a derecha.
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Si choca con un enemigo (Slime) -> Lo mata
        if (other.CompareTag("Bug"))
        {
            BugVida vidaScript = other.GetComponent<BugVida>();
            if (vidaScript != null)
            {
                vidaScript.RecibirDañoPublico(); 
            }
            Destroy(gameObject); // Tu bala muere al impactar
        }
        
        // --- NUEVO: DINÁMICA DE CHOQUE DE BALAS ---
        // 2. Si choca con una bala enemiga -> ¡EXPLOSIÓN MUTUA!
        else if (other.CompareTag("BalaEnemiga"))
        {
            Destroy(other.gameObject); // Destruye la bala del Slime (ERROR)
            Destroy(gameObject);       // Destruye tu bala (print)
            
            // (Opcional) Aquí podrías poner un sonido de "chispazo" o efecto visual
            Debug.Log("¡Código depurado en el aire!");
        }
        
        // 3. Ignoramos al jugador y a otras balas propias
        else if (!other.CompareTag("Character") && !other.CompareTag("Weapon")) 
        {
             Destroy(gameObject); // Chocó con pared
        }
    }
}