using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoParpadea : MonoBehaviour
{
    public float velocidad = 2f;   // Velocidad del parpadeo

    CanvasRenderer rendererUI;
    TextMeshProUGUI texto;
    Image imagen;

    void Start()
    {
        // Intentamos obtener cualquier tipo de componente visual UI
        rendererUI = GetComponent<CanvasRenderer>();
        texto = GetComponent<TextMeshProUGUI>();
        imagen = GetComponent<Image>();
    }

    void Update()
    {
        // Valor entre 0 y 1 en forma de ola (0 → 1 → 0 → 1…)
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * velocidad));

        if (texto != null)
        {
            Color c = texto.color;
            c.a = alpha;
            texto.color = c;
        }

        if (imagen != null)
        {
            Color c = imagen.color;
            c.a = alpha;
            imagen.color = c;
        }
    }
}
