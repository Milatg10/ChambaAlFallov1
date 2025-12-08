using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class InteraccionPuerta : MonoBehaviour
{
    [Header("Configuración UI")]
    public GameObject emotePromptUI; // Arrastra aquí el GameObject de la UI (el emote)
    public GameObject puertaObjeto; // El objeto de la puerta para desactivarla

    private bool jugadorCerca = false;

    [Header("Referencias Puerta")]
    public SpriteRenderer puertaRenderer;
    public GameObject puertaEscena;


    void Start()
    {
        if (emotePromptUI != null) emotePromptUI.SetActive(false);
    }

    void Update()
    {
        // Si el jugador está cerca y pulsa Espacio
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            // Cambiar la imagen de la puerta a abierta
            if (puertaRenderer != null)
            {
                puertaRenderer.enabled = false; // Oculta la puerta visualmente
                puertaObjeto.SetActive(false); // Desactiva la puerta para "abrirla"
                                               // puertaEscena.SetActive(true); // Activa la puerta de la escena siguiente
            }
        }
    }

    // Detecta cuando el jugador entra en el área del Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && EstadoJuego.puzzle1Resuelto && puertaRenderer.enabled)
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

}