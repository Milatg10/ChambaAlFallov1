using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class MenuPuzleBloques : MonoBehaviour
{
    [Header("Nombre de la escena a la que volver")]
    public string escenaMundo = "SampleScene";


    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMundo()
    {
        Debug.Log("Saliendo del puzle...");
        SceneManager.LoadScene(escenaMundo);
    }
}