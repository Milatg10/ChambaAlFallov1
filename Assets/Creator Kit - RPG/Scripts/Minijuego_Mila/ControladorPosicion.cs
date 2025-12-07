using UnityEngine;

public class ControladorPosicion : MonoBehaviour
{
    void Start()
    {
        // 1. Preguntamos: ¿Venimos del minijuego? (1 = Sí, 0 = No)
        if (PlayerPrefs.GetInt("VieneDelMinijuego") == 1)
        {
            // 2. Recuperamos las coordenadas guardadas
            float x = PlayerPrefs.GetFloat("PosicionX");
            float y = PlayerPrefs.GetFloat("PosicionY");
            float z = PlayerPrefs.GetFloat("PosicionZ");

            // 3. ¡TELETRANSPORTE!
            transform.position = new Vector3(x, y, z);

            // 4. Borramos la marca para que si reinicias el juego normal, empieces en el inicio normal
            PlayerPrefs.SetInt("VieneDelMinijuego", 0);
            PlayerPrefs.Save();
            
            Debug.Log("Posición restaurada frente a la casa.");
        }
    }
}