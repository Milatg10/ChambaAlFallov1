using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscenaDestino = "SalidaCasa";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado. Viajando a: " + nombreEscenaDestino);
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}