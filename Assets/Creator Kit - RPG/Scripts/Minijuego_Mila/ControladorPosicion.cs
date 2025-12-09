using UnityEngine;

public class ControladorPosicion : MonoBehaviour
{
    void Start()
    {
        // 1. Leemos la bandera
        int viene = PlayerPrefs.GetInt("VieneDelMinijuego");
        
        Debug.Log($"[CONTROLADOR] He nacido. ¿Vengo de algún sitio? (0=No, 1=Puerta, 2=Puzle): {viene}");

        // CASO 1: Vengo de una puerta normal
        if (viene == 1)
        {
            CargarPosicion();
        }
        // CASO 2: Vengo del Puzle (Tu nueva condición)
        else if(viene == 2)
        {
            Debug.Log("[CONTROLADOR] ¡Vengo del Puzle! Aplicando lógica especial si fuera necesaria.");
            CargarPosicion(); 
        }
        // CASO 0: Inicio normal del juego
        else
        {
            Debug.Log("[CONTROLADOR] Inicio normal o sin datos guardados.");
        }

        // Importante: Reseteamos la variable para que si recargas la escena normal no te teletransporte
        if (viene != 0)
        {
            PlayerPrefs.SetInt("VieneDelMinijuego", 0);
            PlayerPrefs.Save();
        }
    }

    // Función auxiliar para no repetir código
    void CargarPosicion()
    {
        float x = PlayerPrefs.GetFloat("PosicionX");
        float y = PlayerPrefs.GetFloat("PosicionY");
        float z = PlayerPrefs.GetFloat("PosicionZ");

        Debug.Log($"[CONTROLADOR] Moviéndome a coordenadas guardadas: {x}, {y}, {z}");
        transform.position = new Vector3(x, y, z);
    }
}