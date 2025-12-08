using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscenaDestino;

    public float posX, posY;

    public LevelLoader Pantalla_carga;   // Tu objeto "SYSTEM_LoadingScreen" (o donde esté el LevelLoader)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EstadoJuego.hayPosicionGuardada = true;
            EstadoJuego.posicionAlVolver = new Vector3(posX, posY, 0f);
            Pantalla_carga.CargarNivel(nombreEscenaDestino);
        }
    }
}