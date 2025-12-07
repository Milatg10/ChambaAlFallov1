using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerMila : MonoBehaviour
{
    // --- NUEVO: CREAMOS UNA INSTANCIA PARA QUE LOS BUGS PUEDAN HABLARNOS ---
    public static GameManagerMila instance;

    [Header("Configuración de UI")]
    public TMP_Text textoFase;
    public TMP_Text textoDialogo;
    public TMP_Text textoInstrucciones;

    [Header("Enemigos (Prefabs)")]
    public GameObject bugAzul;
    public GameObject bugVerde;
    public GameObject bugRojo;

    [Header("Puntos de Aparición")]
    public Transform[] puntosSpawn;

    [Header("Recompensa Final")]
    public GameObject prefabRaton; // Arrastra aquí tu ratón
    public Transform puntoSpawnRaton; // Dónde quieres que salga (ej: en el centro)

    [Header("Salida")]
    public string nombreEscenaPrincipal = "SampleScene"; // <--- 2. ESCRIBE AQUÍ EL NOMBRE DE TU JUEGO

    private int enemigosRestantes = 0;
    private bool juegoIniciado = false;
    private bool juegoTerminado = false; // <--- 3. PARA SABER SI YA TENEMOS EL RATÓN

    void Awake()
    {
        // Configuramos la instancia para que sea accesible desde cualquier script
        if (instance == null) instance = this;
    }

    void Start()
    {
        textoFase.text = "ARREGLA LOS BUGS";
        textoInstrucciones.text = "Muévete con las flechas del teclado\nDispara con ESPACIO\n\nPulsa ENTER para empezar a compilar...";
    }

    void Update()
    {
        // Inicio del juego
        if (!juegoIniciado && !juegoTerminado && Input.GetKeyDown(KeyCode.Return))
        {
            juegoIniciado = true;
            StartCoroutine(RutinaJuegoCompleto());
        }

        // --- 4. SALIDA DEL JUEGO (ESC) ---
        // Solo funciona si ya has ganado (tienes el ratón)
        if (juegoTerminado && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Volviendo al juego principal...");
            SceneManager.LoadScene(nombreEscenaPrincipal);
        }
    }

    // --- NUEVO: FUNCIÓN QUE LLAMAN LOS BUGS AL MORIR ---
    public void RegistrarMuerteBug()
    {
        enemigosRestantes--;
        
        // (Opcional) Debug para ver cuántos quedan
        Debug.Log("Quedan " + enemigosRestantes + " bugs para pasar de fase.");
    }

    // --- 5. FUNCIÓN QUE LLAMA EL RATÓN AL RECOGERLO ---
    public void RatonRecogido()
    {
        StopAllCoroutines(); // Paramos cualquier cosa que estuviera pasando
        
        textoFase.color = Color.green;
        textoFase.text = "¡ERRORES DEPURADOS!";
        textoDialogo.text = "Has recuperado el control del sistema.";
        
        // El mensaje importante:
        textoInstrucciones.text = "Pulsa ESC para volver al mundo real";
        textoInstrucciones.gameObject.SetActive(true); // Aseguramos que se vea

        juegoTerminado = true; // Activamos la bandera para que funcione el ESC
    }
    IEnumerator RutinaJuegoCompleto()
    {
        // ================= FASE 1: EL HOLA MUNDO (AZULES) =================
        yield return MostrarFase("FASE 1: HOLA MUNDO", "System.out.println('Mata los bugs azules');", Color.cyan);
        
        // CONFIGURAMOS LA META: Hay que matar 5
        enemigosRestantes = 5; 
        
        // Lanzamos la oleada (5 azules)
        yield return SpawnOleada(bugAzul, 5, 1.5f); 
        
        // --- NUEVO: ESPERAMOS HASTA QUE MATES A TODOS ---
        // El código se queda "congelado" en esta línea hasta que enemigosRestantes sea 0
        yield return new WaitUntil(() => enemigosRestantes <= 0);
        
        // Pequeño descanso para celebrar antes de la siguiente
        yield return new WaitForSeconds(2f); 

        // ================= FASE 2: WARNINGS IGNORADOS (VERDES + AZULES) =================
        yield return MostrarFase("FASE 2: WARNINGS IGNORADOS", "¡Cuidado! Variable no usada declarada...", Color.yellow);

        // META: 3 Verdes + 4 Azules = 7 enemigos
        enemigosRestantes = 7;

        // Lanzamos las oleadas
        yield return SpawnOleada(bugVerde, 3, 2f); 
        StartCoroutine(SpawnOleada(bugAzul, 4, 2f)); // Salen a la vez
        
        // ESPERAMOS A QUE LIMPIES LA PANTALLA
        yield return new WaitUntil(() => enemigosRestantes <= 0);
        yield return new WaitForSeconds(2f);

        // ================= FASE 3: SEGMENTATION FAULT (TODOS) =================
        yield return MostrarFase("FASE 3: SEGMENTATION FAULT", "FATAL ERROR: Segmentation Fault\n¡EL SISTEMA SE CAE!", Color.red);

        // META: 2 Rojos + 5 Azules + 3 Verdes = 10 enemigos
        enemigosRestantes = 10;

        yield return SpawnOleada(bugRojo, 2, 1f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnOleada(bugAzul, 5, 1f));
        StartCoroutine(SpawnOleada(bugVerde, 3, 2f)); 
        
        // ESPERAMOS A QUE SOBREVIVAS AL CAOS
        yield return new WaitUntil(() => enemigosRestantes <= 0);

        // ================= FIN DEL JUEGO =================
        yield return new WaitForSeconds(2f);
        textoFase.color = Color.green;
        textoFase.text = "COMPILACIÓN COMPLETADA";
        textoDialogo.text = "¡Has salvado el código! Recoge tu recompensa.";
        textoInstrucciones.text = "Busca el ratón en el mapa";

        // --- NUEVO: CREAR EL RATÓN ---
        if (prefabRaton != null)
        {
            // La creamos en el punto que elijas (o en el centro 0,0,0 si prefieres)
            // Si usas puntoSpawnManzana, asegúrate de asignarlo en Unity o dará error.
            // Si no quieres crear un punto, cambia "puntoSpawnManzana.position" por "Vector3.zero"
            Instantiate(prefabRaton, puntoSpawnRaton.position, Quaternion.identity);
        }
    }

    IEnumerator MostrarFase(string titulo, string frase, Color colorTitulo)
    {
        textoFase.color = colorTitulo;
        textoFase.text = titulo;
        textoDialogo.text = frase;
        
        // Sonido opcional aquí si quisieras
        
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