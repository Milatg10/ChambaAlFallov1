using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MetaPuzle : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaJuego = "SampleScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EstadoJuego.puzzle1Resuelto = true;
            SceneManager.LoadScene(nombreEscenaJuego);
            EstadoJuego.hayPosicionGuardada = true;
            EstadoJuego.posicionAlVolver = new Vector3(-5.69f, 8.72f, 0);
            SceneManager.LoadScene("SampleScene");
        }
    }
}