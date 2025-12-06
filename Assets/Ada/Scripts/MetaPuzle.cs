using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MetaPuzle : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaJuego = "SampleScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si lo que ha entrado es el bloque del Jugador (la bola roja)
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡VICTORIA! Volviendo al juego...");
            SceneManager.LoadScene(nombreEscenaJuego);
        }
    }
}