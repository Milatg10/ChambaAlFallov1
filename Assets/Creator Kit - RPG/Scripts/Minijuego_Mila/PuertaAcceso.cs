using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaAcceso : MonoBehaviour
{
    public string nombreEscenaDestino = "Minijuego_Mila";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Character"))
        {
            // --- NUEVO: GUARDAR POSICIÓN ---
            // Guardamos dónde está el jugador AHORA MISMO
            float x = collision.transform.position.x;
            float y = collision.transform.position.y;
            float z = collision.transform.position.z;

            // TRUCO ANTI-BUCLE: 
            // Si reapareces EXACTAMENTE en la puerta, volverás a entrar al minijuego al instante.
            // Así que vamos a restarle un poquito a la 'Y' para que aparezcas "un paso atrás" (fuera de la puerta).
            PlayerPrefs.SetFloat("PosicionX", x);
            PlayerPrefs.SetFloat("PosicionY", y - 1.0f); // <--- Te muevo 1 metro hacia abajo
            PlayerPrefs.SetFloat("PosicionZ", z);
            
            // Marcamos una casilla para saber que "Venimos del Minijuego"
            PlayerPrefs.SetInt("VieneDelMinijuego", 1);
            PlayerPrefs.Save();
            // -------------------------------

            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}