using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuPuzleBloques : MonoBehaviour
{
    [Header("Nombre de la escena a la que volver")]
    public string escenaMundo = "SampleScene";
    public LevelLoader Pantalla_carga; 

    public void ReiniciarNivel()
    {
        Debug.Log("Reiniciando el puzle...");
        Pantalla_carga.CargarNivel(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMundo()
    {
        Debug.Log("Saliendo del puzle...");
        Debug.Log("Guardando posición de retorno (Tipo 2)...");

        // 1. GUARDADO EN PLAYERPREFS (Coordenadas fijas de retorno)
        PlayerPrefs.SetFloat("PosicionX", -5.69f);
        PlayerPrefs.SetFloat("PosicionY", 8.72f);
        PlayerPrefs.SetFloat("PosicionZ", 0f);

        // 2. Establecemos la bandera en 2
        PlayerPrefs.SetInt("VieneDelMinijuego", 2);
        PlayerPrefs.Save();

        // 3. Cargar
        Pantalla_carga.CargarNivel(escenaMundo);
    }
}