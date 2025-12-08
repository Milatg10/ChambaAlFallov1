using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class MenuPuzleBloques : MonoBehaviour
{
    [Header("Nombre de la escena a la que volver")]
    public string escenaMundo = "SampleScene";
    public LevelLoader Pantalla_carga;   // Tu objeto "SYSTEM_LoadingScreen" (o donde esté el LevelLoader)


    public void ReiniciarNivel()
    {
        Debug.Log("Reiniciando el puzle...");
        Pantalla_carga.CargarNivel(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMundo()
    {
        Debug.Log("Saliendo del puzle...");
        EstadoJuego.hayPosicionGuardada = true;
        EstadoJuego.posicionAlVolver = new Vector3(-5.69f, 8.72f, 0);
        Pantalla_carga.CargarNivel(escenaMundo);
    }
}