using UnityEngine;

public class ControladorPosicion : MonoBehaviour
{
    void Start()
    {
        // Leemos la bandera
        int viene = PlayerPrefs.GetInt("VieneDelMinijuego");
        
        // --- CHIVATO 3 ---
        Debug.Log($"[JUGADOR] He nacido. ¿Vengo del minijuego? (1=Sí, 0=No): {viene}");

        if (viene == 1)
        {
            float x = PlayerPrefs.GetFloat("PosicionX");
            float y = PlayerPrefs.GetFloat("PosicionY");
            float z = PlayerPrefs.GetFloat("PosicionZ");

            // --- CHIVATO 4 ---
            Debug.Log($"[JUGADOR] Moviéndome a coordenadas guardadas: {x}, {y}, {z}");

            transform.position = new Vector3(x, y, z);

            // --- CHIVATO 5 ---
            Debug.Log($"[JUGADOR] Mi posición final es: {transform.position}");

            PlayerPrefs.SetInt("VieneDelMinijuego", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // --- CHIVATO 6 ---
            Debug.Log("[JUGADOR] No vengo del minijuego. Me quedo donde me ponga Unity.");
        }
    }
}