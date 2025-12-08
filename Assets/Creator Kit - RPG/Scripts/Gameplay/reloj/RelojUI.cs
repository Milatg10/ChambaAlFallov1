using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RelojUI : MonoBehaviour
{
    public MundoData mundo;
    public TextMeshProUGUI textoReloj;

    [Header("Velocidad del tiempo")]
    public float segundosPorAvance = 1f;   // cada cuántos segundos reales
    public int minutosPorTick = 5;         // cuántos minutos avanza el reloj

    private float temporizador = 0f;

    void Update()
    {
        if (mundo == null || textoReloj == null) return;

        // --- Avance del tiempo ---
        temporizador += Time.deltaTime;

        if (temporizador >= segundosPorAvance)
        {
            temporizador = 0f;
            mundo.tiempoInicio += minutosPorTick;
        }

        // --- Actualizar texto ---
        string inicio = FormatearTiempo(mundo.tiempoInicio);
        string limite = FormatearTiempo(mundo.tiempoLimite);
        textoReloj.text = inicio + " / " + limite;
    }

    void CambiarAScenaFinal()
    {
        SceneManager.LoadScene("FINAL");
    }

    string FormatearTiempo(int minutos)
    {
        int horas = minutos / 60;
        int mins = minutos % 60;
        return horas.ToString("00") + ":" + mins.ToString("00");
    }
}
