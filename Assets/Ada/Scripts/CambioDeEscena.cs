using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscenaDestino;

    public float posX, posY;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
            EstadoJuego.hayPosicionGuardada = true;
            EstadoJuego.posicionAlVolver = new Vector3(posX, posY, 0f);
        }
    }
}