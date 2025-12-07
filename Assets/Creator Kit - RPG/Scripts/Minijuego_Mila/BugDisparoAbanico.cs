using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugDisparoAbanico : MonoBehaviour
{
    public GameObject balaPrefab; // Aquí pones la Bala_Warning
    public float tiempoEntreDisparos = 3f; // Un poco más lento porque dispara muchas
    
    // Configuración del abanico
    public int cantidadBalas = 3;  // Cuántas balas salen (3, 5, etc.)
    public float anguloApertura = 30f; // Cuánto se abre el abanico (grados)

    private float contadorTiempo;
    private Transform jugador;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Character");
        if (obj != null) 
        {
            jugador = obj.transform;
            Debug.Log("ESCUPETA: ¡He visto al Character!"); // <--- MENSAJE 1
        }
        else
        {
            Debug.LogError("ESCOPETA: ¡No encuentro ningún objeto con tag 'Character'!"); // <--- MENSAJE ERROR
        }
        
        contadorTiempo = tiempoEntreDisparos;
    }

    void Update()
    {
        if (jugador == null) return;

        contadorTiempo += Time.deltaTime;

        if (contadorTiempo >= tiempoEntreDisparos)
        {
            Debug.Log("ESCOPETA: ¡Intentando disparar!"); // <--- MENSAJE 2
            DispararEscopeta();
            contadorTiempo = 0;
        }
    }

    void DispararEscopeta()
    {
        if (balaPrefab == null) 
        {
             Debug.LogError("ESCOPETA: ¡Socorro! No tengo bala asignada en el Inspector"); // <--- MENSAJE ERROR DE BALA
             return;
        }

        // 1. Calcular la dirección central hacia el jugador
        Vector2 direccionCentral = (jugador.position - transform.position).normalized;
        float anguloBase = Mathf.Atan2(direccionCentral.y, direccionCentral.x) * Mathf.Rad2Deg;

        // 2. Calcular dónde empieza el arco del abanico
        // Si el ángulo total es 30, empezamos en -15 (izquierda) y terminamos en +15 (derecha)
        float anguloInicial = anguloBase - (anguloApertura / 2f);
        float pasoAngulo = anguloApertura / (cantidadBalas - 1);

        // 3. Bucle para crear las balas una a una
        for (int i = 0; i < cantidadBalas; i++)
        {
            // Calculamos el ángulo de ESTA bala concreta
            float anguloActual = anguloInicial + (pasoAngulo * i);
            Quaternion rotacion = Quaternion.Euler(0, 0, anguloActual);

            // Calculamos posición de salida (adelantada 0.8m para no chocarse con el slime)
            // Usamos trigonometría básica para convertir el ángulo en vector
            Vector3 direccionSalida = rotacion * Vector3.right; 
            Vector3 posicionSalida = transform.position + (direccionSalida * 0.8f);

            Instantiate(balaPrefab, posicionSalida, rotacion);
        }
    }
}