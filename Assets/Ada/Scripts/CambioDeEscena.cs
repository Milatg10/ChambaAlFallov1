using UnityEngine;
using UnityEngine.SceneManagement; 

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscenaDestino;
    public float posX, posY; // Coordenadas donde aparecerás en la otra escena
    public LevelLoader Pantalla_carga; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entrando en puerta estándar. Guardando retorno tipo 1...");

            // 1. Guardamos las coordenadas que pusiste en el Inspector
            PlayerPrefs.SetFloat("PosicionX", posX);
            PlayerPrefs.SetFloat("PosicionY", posY);
            PlayerPrefs.SetFloat("PosicionZ", 0f);

            // 2. Establecemos la bandera en 1 (Retorno Normal)
            PlayerPrefs.SetInt("VieneDelMinijuego", 1);
            PlayerPrefs.Save();

            // 3. Cargar
            Pantalla_carga.CargarNivel(nombreEscenaDestino);
        }
    }
}