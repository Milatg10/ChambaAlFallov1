using UnityEngine;
using UnityEngine.SceneManagement; 

public class MetaPuzle : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaJuego = "SampleScene";
    public LevelLoader Pantalla_carga;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Lógica del juego (Victoria)
            EstadoJuego.puzzle1Resuelto = true; // Esto lo mantenemos si usas EstadoJuego para misiones
            
            Debug.Log("¡Puzle completado! Guardando retorno tipo 2 (Puzle)...");

            // 2. GUARDADO EN PLAYERPREFS (Sustituye a EstadoJuego.posicionAlVolver)
            PlayerPrefs.SetFloat("PosicionX", -5.69f);
            PlayerPrefs.SetFloat("PosicionY", 8.72f);
            PlayerPrefs.SetFloat("PosicionZ", 0f);
            
            // Aquí decimos que venimos del Puzle (Valor 2)
            PlayerPrefs.SetInt("VieneDelMinijuego", 2);
            PlayerPrefs.Save();

            // 3. Cargar escena
            Pantalla_carga.CargarNivel(nombreEscenaJuego);
        }
    }
}