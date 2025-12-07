using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisparo : MonoBehaviour
{
    public GameObject balaPrefab;
    private Vector2 ultimaDireccion = Vector2.down; // Mira abajo por defecto

    void Update()
    {
        // 1. Detectar hacia dónde miramos (esto ya lo tienes igual)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0) ultimaDireccion = (x > 0) ? Vector2.right : Vector2.left;
        else if (y != 0) ultimaDireccion = (y > 0) ? Vector2.up : Vector2.down;

        // 2. DISPARAR SIN LÍMITES
        // Al quitar la condición de tiempo, cada vez que la tecla baje, sale bala.
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Disparar();
        }
    }

    void Disparar()
        {
            if (balaPrefab == null)
            {
                Debug.LogError("¡ERROR! ¡NO HAS PUESTO LA BALA EN EL INSPECTOR!");
                return;
            }

           // ... dentro de la función Disparar() ...

            float anguloZ = 0f;

            if (ultimaDireccion == Vector2.right)      anguloZ = 0f;   // Derecha: Se queda normal
            else if (ultimaDireccion == Vector2.up)    anguloZ = 90f;  // Arriba: Gira 90º antihorario
            else if (ultimaDireccion == Vector2.left)  anguloZ = 180f; // Izquierda: al revés (360º es igual a 0º)
            else if (ultimaDireccion == Vector2.down)  anguloZ = -90f; // Abajo: Gira 90º horario

            // Aplicamos esa rotación en el eje Z
            Quaternion rotacion = Quaternion.Euler(0, 0, anguloZ); 

            // EXTRA: Esto asegura que la bala salga en el suelo (Z=0) y no flotando
            Vector3 posicionNacimiento = transform.position;
            posicionNacimiento.z = 0;

            Instantiate(balaPrefab, posicionNacimiento, rotacion);
        }
}
