using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;   // ← TEXTMESH PRO

public class MeenuSystem2 : MonoBehaviour
{
    [Header("MENU")]
    public GameObject menuPanel;

    [Header("CAMARA")]
    public Transform cameraTransform;
    public Transform targetPosition;
    public float speed = 2f;

    [Header("RULETA")]
    public GameObject panelRuleta;
    public RectTransform ruletaObjetivo;
    public float tiempoGiro = 8f;
    public float velocidadMax = 1000f;
    public AudioSource sonidoGiro;

    [Header("SECTORES")]
    public List<string> nombresOpciones;
    public List<int> valoresOpciones;

    // Datos del resultado
    public bool FinRuleta { get; private set; } = false;
    public string NombreGanador { get; private set; }
    public int ValorGanador { get; private set; }

    [Header("RESULTADO")]
    public GameObject panelResultado;
    public TMP_Text textoResultado;

    [Header("ESCENA SIGUIENTE")]
    public int sceneToLoad = 1;

    [Header("DIFICULTAD GLOBAL")]
    public DificultadData dificultadGlobal;

    [Header("Variables mundo")]
    public MundoData mundoData;


    // VARIABLES INTERNAS RULETA
    private bool girando = false;


    // --- BOTONES ---
    public void Jugar()
    {
        menuPanel.SetActive(false);
        StartCoroutine(FlujoCompleto());
    }

    public void Salir()
    {
        Application.Quit();
    }


    // --- FLUJO COMPLETO ---
    private IEnumerator FlujoCompleto()
    {
        // 1. Mover cámara (tu animación original)
        yield return StartCoroutine(MoverCamara());

        // 2. Esperar antes del giro
        yield return new WaitForSeconds(1f);

        // 3. Iniciar ruleta
        GirarRuleta();

        // 4. Esperar a que termine
        yield return new WaitUntil(() => FinRuleta == true);

        // 5. Mostrar panel resultado
        panelRuleta.SetActive(false);
        panelResultado.SetActive(true);

        switch (ValorGanador)
        {
            case 1:
                textoResultado.text =
                    "Esta noche hay una entrega que acabar, no hay problema :3.\n" +
                    "Has llegado temprano al piso, va todo bien, te has tomado un buen cafe " +
                    "y te has sentado a terminar con el proyecto.";

                textoResultado.color = Color.green;   // VERDE
                break;

            case 2:
                textoResultado.text =
                    "Esta noche hay una entrega que acabar y la cosa pinta un poco justa.\n" +
                    "Has llegado al piso algo mas tarde de lo que esperabas, respira... aun hay tiempo.\n" +
                    "Te has tomado un buen cafe y te has sentado a terminar con el proyecto.";

                textoResultado.color = Color.yellow;  // AMARILLO
                break;

            default:
                textoResultado.text =
                    "Esta noche hay una entrega que acabar y LA COSA VA MUY MAL :(.\n" +
                    "Has llegado al piso tarde, has estado 30 MINUTOS BUSCANDO DONDE APARCAR.\n" +
                    "Respira... aun se puede... ¿verdad?";

                textoResultado.color = Color.red;     // ROJO
                break;
        }
        // Guardar dificultad global
        dificultadGlobal.dificultadActual = ValorGanador;

        /// Aplicar configuración según dificultad
        if (mundoData != null)
        {
            switch (dificultadGlobal.dificultadActual)
            {
                case 1:
                    mundoData.tiempoInicio = 18 * 60;
                    mundoData.tiempoLimite = 24 * 60;
                    mundoData.objetosMaximos = 3;
                    break;

                case 2:
                    mundoData.tiempoInicio = 19 * 60;
                    mundoData.tiempoLimite = 24 * 60;
                    mundoData.objetosMaximos = 3;
                    break;

                case 3:
                    mundoData.tiempoInicio = 20 * 60;
                    mundoData.tiempoLimite = 24 * 60;
                    mundoData.objetosMaximos = 3;
                    break;
            }

            // Reset por seguridad
            mundoData.objetosRecogidos = 0;
            mundoData.vidaActual = mundoData.vidaMaxima;
        }


        // 6. Esperar a que el usuario pulse Espacio
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) );

        // 7. Cambiar de escena
        SceneManager.LoadScene(sceneToLoad);

    }


    // --- ANIMACIÓN CÁMARA ---
    private IEnumerator MoverCamara()
    {
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        Vector3 endPos = targetPosition.position;
        Quaternion endRot = targetPosition.rotation;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }
    }


    // --- RULETA: GIRO ---
    public void GirarRuleta()
    {
        if (!girando)
            StartCoroutine(GiroControlado());
    }

    IEnumerator GiroControlado()
    {
        girando = true;
        FinRuleta = false;

        panelRuleta.SetActive(true);

        // 1. Elegimos sector al azar
        int sectorAleatorio = Random.Range(0, nombresOpciones.Count);
        float gradosPorSector = 360f / nombresOpciones.Count;

        float anguloObjetivo = sectorAleatorio * gradosPorSector + gradosPorSector / 2f;

        // 3 vueltas completas
        float vueltasExtra = 1080f; 
        float anguloFinal = vueltasExtra + anguloObjetivo;

        float anguloInicial = ruletaObjetivo.eulerAngles.z;

        // Duraciones
        float durAcel = tiempoGiro * 0.25f;
        float durConst = tiempoGiro * 0.35f;
        float durFreno = tiempoGiro * 0.40f;

        float tiempo = 0f;

        if (sonidoGiro != null)
            sonidoGiro.Play();

        // --- ACELERACIÓN ---
        while (tiempo < durAcel)
        {
            float t = tiempo / durAcel;
            float aceleracion = Mathf.SmoothStep(0f, 1f, t);

            float angulo = Mathf.Lerp(
                anguloInicial,
                anguloInicial + (anguloFinal * 0.25f),
                aceleracion
            );

            ruletaObjetivo.rotation = Quaternion.Euler(0, 0, angulo);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // --- VELOCIDAD CONSTANTE ---
        float anguloAcel = anguloInicial + (anguloFinal * 0.25f);
        float anguloConst = anguloInicial + (anguloFinal * 0.75f);

        tiempo = 0f;
        while (tiempo < durConst)
        {
            float t = tiempo / durConst;

            float angulo = Mathf.Lerp(anguloAcel, anguloConst, t);

            ruletaObjetivo.rotation = Quaternion.Euler(0, 0, angulo);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // --- FRENADO ---
        tiempo = 0f;
        while (tiempo < durFreno)
        {
            float t = tiempo / durFreno;
            float frenado = 1f - Mathf.Pow(1f - t, 2f);

            float angulo = Mathf.Lerp(anguloConst, anguloInicial + anguloFinal, frenado);

            ruletaObjetivo.rotation = Quaternion.Euler(0, 0, angulo);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Resultado final
        NombreGanador = nombresOpciones[sectorAleatorio];
        ValorGanador = valoresOpciones[sectorAleatorio];

        FinRuleta = true;
        girando = false;
    }

    // --- CALCULAR RESULTADO ---
    void DeterminarResultado()
    {
        float angulo = ruletaObjetivo.eulerAngles.z % 360f;

        float gradosPorSector = 360f / nombresOpciones.Count;

        int sector = Mathf.FloorToInt(angulo / gradosPorSector);
        sector = Mathf.Clamp(sector, 0, nombresOpciones.Count - 1);

        NombreGanador = nombresOpciones[sector];
        ValorGanador = valoresOpciones[sector];

        FinRuleta = true;
    }
}