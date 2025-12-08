using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{
    public float velocidad = 7f;
    public float daño = 10f; // Cuánto quita

    void Start()
    {
        Destroy(gameObject, 15f); // Se autodestruye a los 3 segundos
    }

    void Update()
    {
        // Se mueve hacia su derecha (su frente)
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Si choca con el JUGADOR -> Daño
        if (other.CompareTag("Character"))
        {
            JugadorSalud salud = other.GetComponent<JugadorSalud>();
            if (salud != null) salud.RecibirGolpe(daño);
            Destroy(gameObject);
        }
        
        // 2. Si choca con la bala del JUGADOR ("Weapon") -> Destrucción mutua
        else if (other.CompareTag("Weapon"))
        {
            Destroy(other.gameObject); // Rompemos la bala del jugador (print)
            Destroy(gameObject);       // Nos rompemos nosotros
            Debug.Log("¡Choque de balas!");
        }

        // 3. SI CHOCA CON OTRA BALA ENEMIGA... ¡NO HACEMOS NADA!
        // (Antes aquí tenías un código que las destruía, bórralo o usa este 'else if' vacío)
        else if (other.CompareTag("BalaEnemiga"))
        {
            // Ignorar. Son hermanas.
            return;
        }

        // 4. Paredes (Cualquier otra cosa que no sea Bug)
        else if (!other.CompareTag("Bug"))
        {
            Destroy(gameObject);
        }
    }
}
