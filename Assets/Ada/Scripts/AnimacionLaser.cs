using UnityEngine;

public class AnimacionLaser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float velocidadAnimacion = 5f;
    public float velocidadRuido = 0.5f;

    // NUEVO: Control del grosor base desde el Inspector
    public float grosorBase = 0.5f;

    void Start()
    {
        // Si se te olvida arrastrarlo, lo busca solo
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (lineRenderer == null) return;

        // 1. Mover la textura (Scroll)
        float offset = Time.time * velocidadAnimacion;

        // Verificación de seguridad por si el material no tiene textura
        if (lineRenderer.material != null)
        {
            lineRenderer.material.mainTextureOffset = new Vector2(-offset, 0);
        }

        // 2. Hacer vibrar el ancho
        // Ahora usamos 'grosorBase' en vez de el 0.1 fijo
        float ruido = Mathf.PerlinNoise(Time.time * velocidadRuido, 0);

        // El ruido variará un poquito arriba y abajo del grosor base
        lineRenderer.widthMultiplier = grosorBase + (ruido * 0.05f);
    }
}