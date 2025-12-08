using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionBuzon : MonoBehaviour
{
    [Header("Configuración UI")]
    public GameObject emotePromptUI;

    [Header("Configuración de Escena")]
    public string nombreEscenaPuzle1 = "EscenaPuzleBloques";

    private bool jugadorCerca = false;

    void Start()
    {
        // Aseguramos que la UI esté oculta al inicio
        if (emotePromptUI != null) emotePromptUI.SetActive(false);

        // AQUÍ PODRÍAS COMPROBAR SI EL PUZLE YA SE RESOLVIÓ (Ver Paso 2)
        // puzleYaResuelto = GameManager.instance.buzonResuelto;
    }

    void Update()
    {
        // Si el jugador está cerca, no ha resuelto el puzle y pulsa Espacio
        if (jugadorCerca && !EstadoJuego.puzzle1Resuelto && Input.GetKeyDown(KeyCode.Space))
        {
            EntrarAlPuzle();
        }
    }

    // Detecta cuando el jugador entra en el área del Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !EstadoJuego.puzzle1Resuelto)
        {
            jugadorCerca = true;
            if (emotePromptUI != null) emotePromptUI.SetActive(true);
        }
    }

    // Detecta cuando el jugador sale del área del Trigger
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (emotePromptUI != null) emotePromptUI.SetActive(false);
        }
    }

    private void EntrarAlPuzle()
    {
        Debug.Log("Cargando Puzle 1...");
        SceneManager.LoadScene(nombreEscenaPuzle1);
    }
}