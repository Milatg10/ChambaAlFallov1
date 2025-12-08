using UnityEngine;
using System.Collections;

public class ControladorPosicion : MonoBehaviour
{
    // Usamos IEnumerator Start para que sea una Corrutina automática
    IEnumerator Start()
    {
        // 1. Preguntamos si venimos del minijuego
        if (PlayerPrefs.GetInt("VieneDelMinijuego") == 1)
        {
            float x = PlayerPrefs.GetFloat("PosicionX");
            float y = PlayerPrefs.GetFloat("PosicionY");
            float z = PlayerPrefs.GetFloat("PosicionZ");
            
            Vector3 posicionDestino = new Vector3(x, y, z);
            
            Debug.Log($"[FUERZA BRUTA] Iniciando teletransporte a: {posicionDestino}");

            // 2. EL TRUCO: Forzamos la posición durante 5 fotogramas seguidos.
            // Esto vence a cualquier otro script que intente moverte al principio.
            for (int i = 0; i < 5; i++)
            {
                transform.position = posicionDestino;
                yield return null; // Espera al siguiente fotograma
            }
            
            // 3. Un último ajuste por seguridad después de esperar un poco más
            yield return new WaitForSeconds(0.1f);
            transform.position = posicionDestino;

            Debug.Log("[FUERZA BRUTA] Posición fijada definitivamente.");

            // 4. Borramos la marca
            PlayerPrefs.SetInt("VieneDelMinijuego", 0);
            PlayerPrefs.Save();
        }
    }
}