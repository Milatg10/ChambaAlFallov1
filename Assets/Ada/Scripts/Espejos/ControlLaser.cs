using UnityEngine;

public class ControlLaser : MonoBehaviour
{
    // Variable estática para saber si la vitrina está abierta o no
    public static bool vitrinaAbierta = false;
    // Variable estática para saber si la pantalla ha sido recogida
    public static bool pantallaRecogida = false;

    [Header("Configuración")]
    public int rebotesMaximos = 5;
    public float distanciaMaxima = 20f;
    public LayerMask capasReflectoras;

    [Header("Referencias")]
    public LineRenderer lineRenderer;
    public Transform puntoDisparo;

    // VARIABLE DE COLOR (Puedes cambiarla en el inspector si quieres otro tono)
    public Color colorVictoria = Color.green;

    private Ray2D rayo;
    private RaycastHit2D impacto;

    // Guardará la bola que coloreamos en el frame anterior
    private SpriteRenderer ultimoObjetivoGolpeado;

    [Header("Configuración Vitrina (Puerta)")]
    public SpriteRenderer vitrinaRenderer;
    public Sprite spriteVitrinaAbierta;    // Imagen de la vitrina abierta
    public Sprite spriteVitrinaCerrada;    // Imagen de la vitrina cerrada

    public Sprite spriteVitrinaVacia;     // Imagen de la vitrina sin pantalla abierta

    public Sprite spriteVitrinaVaciaCerrada; // Imagen de la vitrina sin pantalla cerrada

    void Update()
    {
        LanzarLaser();
    }

    void LanzarLaser()
    {
        vitrinaAbierta = false;

        // PASO 1: RESETEO 
        if (ultimoObjetivoGolpeado != null)
        {
            ultimoObjetivoGolpeado.color = Color.white;
            ultimoObjetivoGolpeado = null;
        }

        // --- LÓGICA DEL LÁSER ---

        Vector3 origen = (puntoDisparo != null) ? puntoDisparo.position : transform.position;
        Vector3 direccion = (puntoDisparo != null) ? puntoDisparo.right : transform.right;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, origen);

        rayo = new Ray2D(origen, direccion);

        for (int i = 0; i < rebotesMaximos; i++)
        {
            impacto = Physics2D.Raycast(rayo.origin, rayo.direction, distanciaMaxima, capasReflectoras);
            lineRenderer.positionCount += 1;

            if (impacto.collider != null)
            {
                if (vitrinaRenderer != null)
                {
                    if (spriteVitrinaVacia != null && EstadoJuego.puzzle2Resuelto)
                    {
                        vitrinaRenderer.sprite = spriteVitrinaVaciaCerrada; // Cerrada pero vacía
                    }
                    else if (spriteVitrinaAbierta != null)
                    {
                        vitrinaRenderer.sprite = spriteVitrinaCerrada; // Cerrada con pantalla 
                    }
                }
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, impacto.point);

                if (impacto.collider.CompareTag("Espejo"))
                {

                    Vector2 direccionReflejada = Vector2.Reflect(rayo.direction, impacto.normal);
                    // Mantenemos el margen de 0.5f que arreglamos antes
                    rayo = new Ray2D(impacto.point + (direccionReflejada * 0.1f), direccionReflejada);
                }
                else if (impacto.collider.CompareTag("Objetivo"))
                {
                    vitrinaAbierta = true;
                    SpriteRenderer bolaRenderer = impacto.collider.GetComponent<SpriteRenderer>();

                    if (bolaRenderer != null)
                    {
                        // PASO 2: CAMBIAR A VERDE
                        bolaRenderer.color = colorVictoria;

                        // Guardamos la referencia para destenirla luego si el láser se quita
                        ultimoObjetivoGolpeado = bolaRenderer;
                    }
                    // 2. ABRIR LA VITRINA 
                    if (vitrinaRenderer != null)
                    {
                        if (EstadoJuego.puzzle2Resuelto && spriteVitrinaVacia != null)
                        {
                            vitrinaRenderer.sprite = spriteVitrinaVacia; // Abierta pero vacía
                        }
                        else if (spriteVitrinaAbierta != null)
                        {
                            vitrinaRenderer.sprite = spriteVitrinaAbierta; // Abierta con pantalla
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, rayo.origin + (rayo.direction * distanciaMaxima));
                break;
            }
        }
    }
}