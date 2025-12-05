using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovimiento : MonoBehaviour
{
    public Transform objetivo; // Aquí pondremos al Player
    public float velocidad = 1.5f; // Velocidad lenta (es un slime)
    public float distanciaMinima = 0.5f; // Para que no se te suba encima

    private SpriteRenderer spriteRenderer; // Para girar el dibujo

    void Start()
    {
        // Buscamos el componente que dibuja el sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // TRUCO AUTOMÁTICO:
        // Si se nos olvida poner el objetivo manual, el script busca al objeto con tag "Player"
        if (objetivo == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                objetivo = jugador.transform;
            }
        }
    }

    void Update()
    {
        // Solo nos movemos si tenemos un objetivo vivo
        if (objetivo != null)
        {
            // Calculamos la distancia
            float distancia = Vector2.Distance(transform.position, objetivo.position);

            // Si estamos lejos, nos acercamos
            if (distancia > distanciaMinima)
            {
                transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
            }

            // GIRAR EL SPRITE (FLIP)
            // Si el jugador está a la derecha (x mayor), el slime mira a la derecha
            if (objetivo.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true; // O false, depende de tu dibujo original
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
