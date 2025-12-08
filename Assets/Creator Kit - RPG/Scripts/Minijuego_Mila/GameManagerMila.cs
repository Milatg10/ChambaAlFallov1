using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // <--- NECESARIO PARA USAR FLECHAS Y TECLADO

public class GameManagerMila : MonoBehaviour
{
    public static GameManagerMila instance;

    // --- NUEVO: VARIABLES DE MEMORIA (STATIC) ---
    // Al ser static, no se borran al reiniciar el nivel con LoadScene
    private static float vidaGuardada; 
    private static bool vieneDeReintento = false;

    [Header("Configuración de UI")]
    public TMP_Text textoFase;
    public TMP_Text textoDialogo;
    public TMP_Text textoInstrucciones;

    [Header("Game Over UI")]
    public GameObject panelGameOver;
    public GameObject botonInicialGameOver; // ARRASTRA AQUÍ EL BOTÓN "REINTENTAR"

    [Header("Datos del Jugador")]
    public VidaData datosVida;

    [Header("Enemigos (Prefabs)")]
    public GameObject bugAzul;
    public GameObject bugVerde;
    public GameObject bugRojo;

    [Header("Puntos de Aparición")]
    public Transform[] puntosSpawn;

    [Header("Recompensa Final")]
    public GameObject prefabRaton;
    public Transform puntoSpawnRaton;

    [Header("Salida")]
    public string nombreEscenaPrincipal = "SampleScene";

    private int enemigosRestantes = 0;
    private bool juegoIniciado = false;
    private bool juegoTerminado = false;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        textoFase.text = "ARREGLA LOS BUGS";
        textoInstrucciones.text = "Muévete con las flechas del teclado\nDispara con ESPACIO\n\nPulsa ENTER para empezar a compilar...";
                
        if(panelGameOver != null) panelGameOver.SetActive(false);

        // --- NUEVO: LÓGICA DE MEMORIA DE VIDA ---
        if (datosVida != null)
        {
            if (!vieneDeReintento)
            {
                // CASO 1: Acabas de llegar del pueblo (Primera vez)
                // Guardamos una "foto" de la vida que traes
                vidaGuardada = datosVida.vidaActual;
                Debug.Log("Vida guardada al entrar: " + vidaGuardada);
            }
            else
            {
                // CASO 2: Vienes de pulsar "Reintentar"
                // No guardamos nada nuevo, usaremos la vidaGuardada que ya tenemos
                Debug.Log("Reintento detectado. Usaremos la vida guardada: " + vidaGuardada);
            }
        }
    }

    void Update()
    {
        // Inicio del juego con Enter
        if (!juegoIniciado && !juegoTerminado && Input.GetKeyDown(KeyCode.Return))
        {
            juegoIniciado = true;
            StartCoroutine(RutinaJuegoCompleto());
        }

        // Salida con ESC (Solo si ganaste)
        if (juegoTerminado && Input.GetKeyDown(KeyCode.Escape))
        {
            SalirDelMinijuego();
        }
    }

    // --- LÓGICA DE GAME OVER ---
    public void GameOver()
    {
        StopAllCoroutines();
        juegoTerminado = true;

        if (panelGameOver != null) 
        {
            panelGameOver.SetActive(true);

            // 2. ACTIVAR NAVEGACIÓN POR TECLADO
            // Esto le dice a Unity: "Pon el cursor en este botón YA"
            if (botonInicialGameOver != null)
            {
                // Limpiamos selección anterior
                EventSystem.current.SetSelectedGameObject(null);
                // Seleccionamos el botón nuevo
                EventSystem.current.SetSelectedGameObject(botonInicialGameOver);
            }
        }
    }

    public void BotonReintentar()
    {
        // --- CAMBIO IMPORTANTE ---
        // Activamos la bandera para saber que estamos reintentando
        vieneDeReintento = true;

        // En vez de curar al máximo (vidaMaxima), restauramos la vida guardada
        if (datosVida != null) 
        {
            // Seguridad: Si la vida guardada es 0 o menos (error raro), le damos al menos 1 punto
            if (vidaGuardada <= 0) vidaGuardada = 10f; 
            
            datosVida.vidaActual = vidaGuardada;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonSalir()
    {
        // Al salir, reseteamos la memoria para que la próxima vez sea "nueva partida"
        vieneDeReintento = false; 
        SalirDelMinijuego();
    }

    void SalirDelMinijuego()
    {
        SceneManager.LoadScene(nombreEscenaPrincipal);
    }

    public void RegistrarMuerteBug()
    {
        enemigosRestantes--;
    }

    public void RatonRecogido()
    {
        StopAllCoroutines();
        
        textoFase.color = Color.green;
        textoFase.text = "¡ERRORES DEPURADOS!";
        textoDialogo.text = "Has recuperado el control del sistema.";
        textoInstrucciones.text = "Pulsa ESC para volver al mundo real";
        textoInstrucciones.gameObject.SetActive(true);

        juegoTerminado = true;
    }

    IEnumerator RutinaJuegoCompleto()
    {
        // FASE 1
        yield return MostrarFase("FASE 1: HOLA MUNDO", "System.out.println('Mata los bugs azules');", Color.cyan);
        enemigosRestantes = 5; 
        yield return SpawnOleada(bugAzul, 5, 1.5f); 
        yield return new WaitUntil(() => enemigosRestantes <= 0);
        yield return new WaitForSeconds(2f); 

        // FASE 2
        yield return MostrarFase("FASE 2: WARNINGS IGNORADOS", "¡Cuidado! Variable no usada declarada...", Color.yellow);
        enemigosRestantes = 7;
        yield return SpawnOleada(bugVerde, 3, 2f); 
        StartCoroutine(SpawnOleada(bugAzul, 4, 2f)); 
        yield return new WaitUntil(() => enemigosRestantes <= 0);
        yield return new WaitForSeconds(2f);

        // FASE 3
        yield return MostrarFase("FASE 3: SEGMENTATION FAULT", "FATAL ERROR: Segmentation Fault\n¡EL SISTEMA SE CAE!", Color.red);
        enemigosRestantes = 10;
        yield return SpawnOleada(bugRojo, 2, 1f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnOleada(bugAzul, 5, 1f));
        StartCoroutine(SpawnOleada(bugVerde, 3, 2f)); 
        yield return new WaitUntil(() => enemigosRestantes <= 0);

        // FIN
        yield return new WaitForSeconds(2f);
        textoFase.color = Color.green;
        textoFase.text = "COMPILACIÓN COMPLETADA";
        textoDialogo.text = "¡Has salvado el código! Recoge tu recompensa.";
        textoInstrucciones.text = "Busca el ratón en el mapa";

        if (prefabRaton != null)
        {
            Instantiate(prefabRaton, puntoSpawnRaton.position, Quaternion.identity);
        }
    }

    IEnumerator MostrarFase(string titulo, string frase, Color colorTitulo)
    {
        textoFase.color = colorTitulo;
        textoFase.text = titulo;
        textoDialogo.text = frase;
        yield return new WaitForSeconds(4f);
        textoFase.text = ""; 
        textoDialogo.text = "";
        textoInstrucciones.text = "";
    }

    IEnumerator SpawnOleada(GameObject enemigo, int cantidad, float velocidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            Transform puntoAleatorio = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
            Instantiate(enemigo, puntoAleatorio.position, Quaternion.identity);
            yield return new WaitForSeconds(velocidad);
        }
    }
}