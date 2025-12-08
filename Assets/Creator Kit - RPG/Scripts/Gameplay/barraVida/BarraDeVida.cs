using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public MundoData vida; // arrastra aquí el VidaData.asset
    public Image barra;
    public float danoPorSegundo = 2f;

    [Header("Colores de la barra")]
    public Color colorCompleto = Color.green;
    public Color colorMedio = Color.yellow;
    public Color colorBajo = Color.red;

    void Update()
    {
        if (vida == null) return;

        // Bajar vida automáticamente
        vida.vidaActual -= danoPorSegundo * Time.deltaTime;
        vida.vidaActual = Mathf.Clamp(vida.vidaActual, 0f, vida.vidaMaxima);

        // Actualizar gráfico de barra
        float porcentaje = vida.vidaActual / vida.vidaMaxima;
        barra.fillAmount = porcentaje;

        // Cambiar color según porcentaje
        if (porcentaje > 0.5f)
            barra.color = colorCompleto;     // Verde
        else if (porcentaje > 0.25f)
            barra.color = colorMedio;        // Amarillo
        else
            barra.color = colorBajo;         // Rojo
    }

    public void RestarVida(float cantidad)
    {
        if (vida == null) return;
        vida.vidaActual = Mathf.Clamp(vida.vidaActual - cantidad, 0f, vida.vidaMaxima);
    }

    public void SumarVida(float cantidad)
    {
        if (vida == null) return;
        vida.vidaActual = Mathf.Clamp(vida.vidaActual + cantidad, 0f, vida.vidaMaxima);
    }
}
