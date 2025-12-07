using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MetaPuzle : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaJuego = "SalidaPuzzle";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaJuego);
        }
    }
}