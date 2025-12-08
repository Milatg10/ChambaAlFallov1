using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MetaPuzle : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaJuego = "SampleScene";
    public LevelLoader Pantalla_carga;   // Tu objeto "SYSTEM_LoadingScreen" (o donde esté el LevelLoader)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EstadoJuego.puzzle1Resuelto = true;
            EstadoJuego.hayPosicionGuardada = true;
            EstadoJuego.posicionAlVolver = new Vector3(-5.69f, 8.72f, 0);
            Pantalla_carga.CargarNivel(nombreEscenaJuego);
        }
    }
}