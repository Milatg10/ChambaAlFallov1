using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinalSceneController : MonoBehaviour
{
    [Header("CAMARA")]
    public Transform cameraTransform;
    public Transform startPosition;
    public Transform endPosition;
    public Transform tecladoPosition;
    public float speed = 2f;

    [Header("IMAGEN FINAL")]
    public SpriteRenderer imagenFinal;
    public SpriteRenderer imagenFinal2;

    public Sprite spritePerfecto;
    public Sprite spriteComplicado;
    public Sprite spriteMalo;

    [Header("PANELES")]
    public GameObject panelResultado;
    public GameObject panelBotones;

    [Header("TEXTO")]
    public TMP_Text textoResultado;

    [Header("VARIABLES GLOBALES")]
    public MundoData mundo;    // AHORA USAMOS TU SCRIPTABLEOBJECT

    [Header("EFECTOS DIFICULTAD 3")]
    public CanvasGroup flashScreen;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    [Header("EFECTOS SONIDO")]
    public AudioSource sonidoDia;
    public AudioSource sonidoSuspiro;
    public AudioSource sonidoCaida;
    public AudioSource sonidoClic;

    [Header("ESCENA A REINICIAR")]
    public int escenaDeJuego = 1;

    private int dificultadFinal = 1;  // Valor calculado

    void Start()
    {
        panelResultado.SetActive(false);
        panelBotones.SetActive(false);

        // 1) CALCULAR LA DIFICULTAD SEGÚN TUS CONDICIONES
        dificultadFinal = CalcularDificultad();

        // Efecto del flash (solo en dificultad 3)
        if (dificultadFinal == 3)
            flashScreen.alpha = 255f;
        else
            flashScreen.alpha = 0f;

        StartCoroutine(FlujoFinal());
    }

    // ---------------------------------------------------------
    //   CALCULAR LA DIFICULTAD SEGÚN TUS NUEVAS REGLAS
    // ---------------------------------------------------------
    private int CalcularDificultad()
    {
        bool vidaMuerta = mundo.vidaActual <= 0;
        bool tiempoPasado = mundo.tiempoInicio >= mundo.tiempoLimite;
        bool objetosCompletos = mundo.objetosRecogidos >= mundo.objetosMaximos;

        // --- 3: MUERTO ---
        if (vidaMuerta)
            return 3;

        // --- 1: PERFECTO ---
        if (!vidaMuerta && !tiempoPasado && objetosCompletos)
            return 1;

        // --- 2: COMPLICADO ---
        if (objetosCompletos && tiempoPasado && !vidaMuerta)
            return 2;

        // Por defecto, si no cae en ningún caso, lo tomo como complicado
        return 3;
    }

    private IEnumerator FlujoFinal()
    {
        // --- TEXTO E IMAGEN SEGÚN DIFICULTAD ---
        switch (dificultadFinal)
        {
            case 1:
                textoResultado.text = "Has tenido una noche perfecta, todo salió bien.";
                textoResultado.color = Color.green;
                imagenFinal.sprite = spritePerfecto;
                imagenFinal2.sprite = spritePerfecto;
                break;

            case 2:
                textoResultado.text = "La noche fue complicada, se te echó el tiempo encima, lo entregaste... pero fuera de plazo.";
                textoResultado.color = Color.yellow;
                imagenFinal.sprite = spriteComplicado;
                imagenFinal2.sprite = spriteComplicado;
                break;

            default:
                textoResultado.text = "Ha sido una noche dura... tanto que te quedaste dormido sobre el escritorio, no entregaste nada.";
                textoResultado.color = Color.red;
                imagenFinal.sprite = spriteMalo;
                imagenFinal2.sprite = spriteMalo;
                break;
        }

        // --- ANIMACIONES DE CÁMARA ---
        if (dificultadFinal == 3)
        {
            yield return StartCoroutine(PreEfectosDificultad3());
            yield return StartCoroutine(ZoomDesdeTeclado());
        }
        else
        {
            if (sonidoClic != null)
                sonidoClic.Play();
            yield return StartCoroutine(ZoomOut());
        }

        // Mostrar panel
        if (dificultadFinal != 1)
        {
            if (sonidoSuspiro != null)
                sonidoSuspiro.Play();
        }

        panelResultado.SetActive(true);

        // Esperar espacio
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        panelResultado.SetActive(false);

        // Caída solo en dificultad 3
        if (dificultadFinal == 3)
        {
            if (sonidoCaida != null)
                sonidoCaida.Play();
            yield return StartCoroutine(CaidaAlTeclado());
            flashScreen.alpha = 255f;
        }

        panelBotones.SetActive(true);
    }

    // EFECTOS ETC. (lo demás queda igual)
    private IEnumerator PreEfectosDificultad3()
    {
        yield return StartCoroutine(InicioTeclado());
        if (sonidoDia != null)
            sonidoDia.Play();

        yield return StartCoroutine(FlashEffect());
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            yield return StartCoroutine(FadeFlash(0f, 1f, flashDuration));
            yield return StartCoroutine(FadeFlash(1f, 0f, flashDuration));
        }
    }

    private IEnumerator FadeFlash(float start, float end, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            flashScreen.alpha = Mathf.Lerp(start, end, t);
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        return MoveCamera(startPosition, endPosition, speed);
    }

    private IEnumerator InicioTeclado()
    {
        return MoveCamera(tecladoPosition, tecladoPosition, speed);
    }

    private IEnumerator ZoomDesdeTeclado()
    {
        return MoveCamera(tecladoPosition, endPosition, speed);
    }

    private IEnumerator CaidaAlTeclado()
    {
        return MoveCamera(cameraTransform, tecladoPosition, speed * 2f);
    }

    private IEnumerator MoveCamera(Transform from, Transform to, float moveSpeed)
    {
        Vector3 startPos = from.position;
        Quaternion startRot = from.rotation;

        Vector3 endPos = to.position;
        Quaternion endRot = to.rotation;

        float t = 0f;

        cameraTransform.position = startPos;
        cameraTransform.rotation = startRot;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;

            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }
    }

    // BOTONES
    public void Reiniciar()
    {
        SceneManager.LoadScene(escenaDeJuego);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
