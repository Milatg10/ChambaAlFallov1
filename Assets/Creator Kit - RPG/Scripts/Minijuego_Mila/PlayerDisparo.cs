using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisparo : MonoBehaviour
{
    public GameObject balaPrefab;
    private Vector2 ultimaDireccion = Vector2.down; // Mira abajo por defecto

    void Update()
    {
        // LEER DIRECCIÓN (Para que no dispare siempre abajo)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        if (x != 0) ultimaDireccion = (x > 0) ? Vector2.right : Vector2.left;
        else if (y != 0) ultimaDireccion = (y > 0) ? Vector2.up : Vector2.down;

        // PRUEBA CON BARRA ESPACIADORA (Es más fácil que el Enter)
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("¡ESPACIO PULSADO! INTENTANDO DISPARAR");
            Disparar();
        }
        
        // MANTENEMOS EL ENTER POR SI ACASO
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("¡ENTER PULSADO! INTENTANDO DISPARAR");
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

        float angulo = 0f;
        if (ultimaDireccion == Vector2.up) angulo = 0f;
        else if (ultimaDireccion == Vector2.down) angulo = 180f;
        else if (ultimaDireccion == Vector2.left) angulo = 90f;
        else if (ultimaDireccion == Vector2.right) angulo = -90f;

        Quaternion rotacion = Quaternion.Euler(0, 0, angulo);
        Instantiate(balaPrefab, transform.position, rotacion);
    }
}
