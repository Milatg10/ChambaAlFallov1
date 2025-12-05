using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class InteraccionBuzon : MonoBehaviour
{
    [Header("Configuración UI")]
    public GameObject emotePromptUI; // Arrastra aquí el GameObject de la UI (el emote)

    [Header("Configuración de Escena")]
    public string nombreEscenaPuzle1 = "EscenaPuzleBloques"; // El nombre exacto de tu escena del puzle 1

    private bool jugadorCerca = false;
    private bool puzleYaResuelto = false; // Para evitar que se repita si vuelves a esta escena

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
        if (jugadorCerca && !puzleYaResuelto && Input.GetKeyDown(KeyCode.Space))
        {
            EntrarAlPuzle();
        }
    }

    // Detecta cuando el jugador entra en el área del Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzleYaResuelto)
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
        // Importante: Debes haber añadido la escena en File -> Build Settings
        SceneManager.LoadScene(nombreEscenaPuzle1);
    }
}