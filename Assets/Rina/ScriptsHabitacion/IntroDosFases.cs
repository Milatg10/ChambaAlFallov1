using System.Collections;
using UnityEngine;
using TMPro;

public class IntroDosFases : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject Panel_Historia;       // El fondo negro
    public GameObject dialogBox;       // <--- ¡NUEVO! Tu imagen bonita (Sprite)
    public TextMeshProUGUI Texto_Historia;     // El texto
    public float velocidadLetras = 0.05f;

    [Header("Referencias Jugador")]
    public MonoBehaviour scriptMovimiento;

    [Header("FASE 1: Diálogos en Negro")]
    [TextArea(2, 5)]
    public string[] dialogosOscuros;

    [Header("FASE 2: Diálogos con Luz")]
    [TextArea(2, 5)]
    public string[] dialogosLuz;

    private int indice = 0;
    private bool enFaseOscura = true;
    private bool escribiendo = false;

    void Start()
    {
        if (scriptMovimiento != null) scriptMovimiento.enabled = false;

        // ENCENDEMOS TODO AL EMPEZAR
        Panel_Historia.SetActive(true);
        if(dialogBox != null) dialogBox.SetActive(true); // Mostramos la cajita
        
        Texto_Historia.text = "";
        StartCoroutine(EscribirFrase(dialogosOscuros[0]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (escribiendo)
            {
                StopAllCoroutines();
                CompletarFraseActual();
            }
            else
            {
                SiguientePaso();
            }
        }
    }

    void SiguientePaso()
    {
        indice++;

        if (enFaseOscura)
        {
            if (indice < dialogosOscuros.Length)
                StartCoroutine(EscribirFrase(dialogosOscuros[indice]));
            else
                CambiarAFaseLuz();
        }
        else
        {
            if (indice < dialogosLuz.Length)
                StartCoroutine(EscribirFrase(dialogosLuz[indice]));
            else
                TerminarIntro();
        }
    }

    void CambiarAFaseLuz()
    {
        enFaseOscura = false;
        indice = 0;
        Panel_Historia.SetActive(false); // Quitamos lo negro, PERO DEJAMOS LA CAJITA
        
        if (dialogosLuz.Length > 0)
            StartCoroutine(EscribirFrase(dialogosLuz[0]));
        else
            TerminarIntro();
    }

    IEnumerator EscribirFrase(string frase)
    {
        escribiendo = true;
        Texto_Historia.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            Texto_Historia.text += letra;
            yield return new WaitForSeconds(velocidadLetras);
        }
        escribiendo = false;
    }

    void CompletarFraseActual()
    {
        escribiendo = false;
        if (enFaseOscura) Texto_Historia.text = dialogosOscuros[indice];
        else Texto_Historia.text = dialogosLuz[indice];
    }

    void TerminarIntro()
    {
        // APAGAMOS TODO AL TERMINAR
        if(dialogBox != null) dialogBox.SetActive(false); // Adiós cajita
        Panel_Historia.SetActive(false);
        
        if (scriptMovimiento != null) scriptMovimiento.enabled = true;
        this.enabled = false;
    }
}