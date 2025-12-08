using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaAcceso : MonoBehaviour
{
    public string nombreEscenaDestino = "Minijuego_Mila";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Character"))
        {
            float x = collision.transform.position.x;
            float y = collision.transform.position.y;
            float z = collision.transform.position.z;

            // --- CHIVATO 1 ---
            Debug.Log($"[PUERTA] Jugador detectado en: X={x}, Y={y}. Guardando...");

            PlayerPrefs.SetFloat("PosicionX", x);
            PlayerPrefs.SetFloat("PosicionY", y - 1.5f); // Restamos para no volver a entrar
            PlayerPrefs.SetFloat("PosicionZ", z);
            
            PlayerPrefs.SetInt("VieneDelMinijuego", 1);
            PlayerPrefs.Save();

            // --- CHIVATO 2 ---
            Debug.Log("[PUERTA] Datos guardados. ¡Cambiando de escena!");

            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}