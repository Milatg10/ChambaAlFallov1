using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaHabitacion : MonoBehaviour
{
    [Header("1. Arrastra aquí los objetos de la Jerarquía")]
    public GameObject Aviso_puerta;       // Tu objeto "Aviso_puerta"
    public GameObject DialogController;      // Tu objeto "DialogController" (la caja azul)
    public LevelLoader Pantalla_carga;    // Tu objeto "SYSTEM_LoadingScreen" (o donde esté el LevelLoader)

    [Header("2. Configuración")]
    public string nombreEscenaDestino;  // Escribe aquí a qué escena vas (ej: Pueblo)

    [Header("3. Teclas para decidir")]
    public KeyCode teclaConfirmar = KeyCode.A; // Tecla para el SÍ
    public KeyCode teclaCancelar = KeyCode.S;  // Tecla para el NO

    private bool estoyEnLaPuerta = false;

    void Start()
    {
        // Nos aseguramos de que al empezar el juego los menús estén ocultos
        if(Aviso_puerta != null) Aviso_puerta.SetActive(false);
        if(DialogController != null) DialogController.SetActive(false);
    }

    void Update()
    {   
        if (estoyEnLaPuerta && !DialogController.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AbrirMenu();
            }
        }

        else if (DialogController.activeSelf)
        {
            // Opción SÍ (Tecla A)
            if (Input.GetKeyDown(teclaConfirmar))
            {
                PulsarSi();
            }
            // Opción NO (Tecla S)
            else if (Input.GetKeyDown(teclaCancelar))
            {
                PulsarNo();
            }
        }
    }

    void AbrirMenu()
    {
        Aviso_puerta.SetActive(false);
        DialogController.SetActive(true);
    }

    // Al entrar en la puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = true;
            Aviso_puerta.SetActive(true); // Muestra "Presiona Espacio"
        }
    }

    // Al alejarse de la puerta
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("¡CHOQUE DETECTADO con: " + collision.name + "!");
        if (collision.CompareTag("Player"))
        {
            estoyEnLaPuerta = false;
            // Ocultamos todo por si acaso se va corriendo con el menú abierto
            Aviso_puerta.SetActive(false);
            DialogController.SetActive(false);
        }
    }

    // --- FUNCIONES PARA LOS BOTONES SÍ / NO ---

    public void PulsarSi()
    {
        Debug.Log("¡Vámonos!");
        // Llama a tu sistema de pantalla de carga
        Pantalla_carga.CargarNivel(nombreEscenaDestino);
    }

    public void PulsarNo()
    {   
        
        DialogController.SetActive(false); // Cierra menú
        
        /*// Solo mostramos el aviso si seguimos en la puerta
        if (estoyEnLaPuerta)
        {
            DialogController.SetActive(true);
        }*/
    }
}