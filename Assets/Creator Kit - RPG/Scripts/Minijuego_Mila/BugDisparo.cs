using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugDisparo : MonoBehaviour
{
    public GameObject balaErrorPrefab; // Arrastra aquí la Bala Roja/Amarilla/Azul
    public float tiempoEntreDisparos = 2f; // Cuánto tarda en recargar

    private float contadorTiempo;
    private Transform jugador;

    void Start()
    {
        // Buscamos al jugador una sola vez al nacer
        GameObject obj = GameObject.FindGameObjectWithTag("Character");
        if (obj != null) jugador = obj.transform;
        
        // TRUCO PRO: Para que no disparen todos A LA VEZ (sincronizados como robots),
        // iniciamos el contador con un número aleatorio. Así cada uno dispara a su ritmo.
        contadorTiempo = Random.Range(0f, tiempoEntreDisparos);
    }

    void Update()
    {
        if (jugador == null) return;

        // --- YA NO COMPROBAMOS DISTANCIA ---
        // El slime dispara siempre, aunque estés en la otra punta del mapa.

        contadorTiempo += Time.deltaTime;

        if (contadorTiempo >= tiempoEntreDisparos)
        {
            Disparar();
            contadorTiempo = 0;
        }
    }

    void Disparar()
    {
        if (balaErrorPrefab == null) return;

        // 1. Calculamos la dirección
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // 2. Calculamos la rotación
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotacion = Quaternion.Euler(0, 0, angulo);

        // 3. EL TRUCO: Calculamos una posición adelantada (0.8 metros hacia el jugador)
        // Así la bala nace FUERA del cuerpo del Slime y no se chocan.
        Vector3 posicionSalida = transform.position + (Vector3)(direccion * 0.8f);

        Instantiate(balaErrorPrefab, posicionSalida, rotacion);
    }
}
