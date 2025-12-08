using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable] 
public class LineaDialogo
{
    [TextArea(2, 5)] public string texto;
    [Header("Tiempos Exactos")]
    public float tiempoInicio;  // Ej: 0
    public float tiempoFin;     // Ej: 6
}

public class IntroDosFases : MonoBehaviour
{
    [Header("UI")]
    public GameObject Panel_Historia;       
    public GameObject dialogBox;       
    public TextMeshProUGUI Texto_Historia;     
    public float velocidadLetras = 0.05f;

    [Header("Jugador")]
    public MonoBehaviour scriptMovimiento;

    [Header("Audio")]
    public AudioSource miAudioSource;
    public AudioClip audioCompleto;

    [Header("Diálogos")]
    public LineaDialogo[] faseOscura;
    public LineaDialogo[] faseLuz;

    private int indice = 0;
    private bool enFaseOscura = true;
    private bool escribiendo = false;

    void Start()
    {
        // Configuración de Audio
        if (miAudioSource == null) miAudioSource = GetComponent<AudioSource>();
        if (miAudioSource != null)
        {
            miAudioSource.playOnAwake = false;
            if (audioCompleto != null) miAudioSource.clip = audioCompleto;
        }

        if (scriptMovimiento != null) scriptMovimiento.enabled = false;

        Panel_Historia.SetActive(true);
        if(dialogBox != null) dialogBox.SetActive(true); 
        
        Texto_Historia.text = "";
        
        if (faseOscura.Length > 0)
            ProcesarNuevaFrase(faseOscura[0]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (escribiendo) ForzarFinalFrase();
            else SiguientePaso();
        }
    }

    void ProcesarNuevaFrase(LineaDialogo linea)
    {
        StopAllCoroutines(); // Detenemos texto anterior

        // --- LÓGICA DE AUDIO "RELOJ ATÓMICO" ---
        if (miAudioSource != null && miAudioSource.clip != null)
        {
            miAudioSource.Stop(); // Reseteamos todo
            
            double duracion = (double)(linea.tiempoFin - linea.tiempoInicio);

            if (duracion > 0)
            {
                Debug.Log($"Sonando desde {linea.tiempoInicio} hasta {linea.tiempoFin} (Duración: {duracion})");
                
                // 1. Ponemos la aguja en el sitio
                miAudioSource.time = linea.tiempoInicio;
                
                // 2. Le damos al Play
                miAudioSource.Play();
                
                // 3. PROGRAMAMOS SU MUERTE exacta usando el reloj interno de Unity
                // Esto es mucho más preciso que usar corutinas o updates
                miAudioSource.SetScheduledEndTime(AudioSettings.dspTime + duracion);
            }
            else
            {
                Debug.LogWarning("¡CUIDADO! La duración del audio es 0 o negativa. Revisa Inicio y Fin.");
            }
        }
        // ---------------------------------------

        StartCoroutine(EscribirFrase(linea.texto));
    }

    void SiguientePaso()
    {
        indice++;
        if (enFaseOscura)
        {
            if (indice < faseOscura.Length) ProcesarNuevaFrase(faseOscura[indice]);
            else CambiarAFaseLuz();
        }
        else
        {
            if (indice < faseLuz.Length) ProcesarNuevaFrase(faseLuz[indice]);
            else TerminarIntro();
        }
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

    void ForzarFinalFrase()
    {
        // Al saltar, paramos el audio inmediatamente
        if (miAudioSource != null) miAudioSource.Stop();
        
        StopAllCoroutines();
        escribiendo = false;
        
        if (enFaseOscura) Texto_Historia.text = faseOscura[indice].texto;
        else Texto_Historia.text = faseLuz[indice].texto;
    }

    void CambiarAFaseLuz()
    {
        enFaseOscura = false;
        indice = 0;
        Panel_Historia.SetActive(false); 
        
        if (faseLuz.Length > 0) ProcesarNuevaFrase(faseLuz[0]);
        else TerminarIntro();
    }

    void TerminarIntro()
    {
        StopAllCoroutines();
        if (miAudioSource != null) miAudioSource.Stop();
        
        if(dialogBox != null) dialogBox.SetActive(false); 
        Panel_Historia.SetActive(false);
        
        if (scriptMovimiento != null) scriptMovimiento.enabled = true;
        this.enabled = false;
    }
}