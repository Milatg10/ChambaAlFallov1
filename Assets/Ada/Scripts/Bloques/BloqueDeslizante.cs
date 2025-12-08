using UnityEngine;

public class BloqueDeslizante : MonoBehaviour
{
    [Header("Configuración")]
    public float tamañoPaso = 1.0f;
    public LayerMask obstaculos;
    public float velocidad = 15f;

    private Vector3 destino;
    private bool moviendose = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        destino = transform.position;
    }

    void OnMouseDown()
    {
        if (!moviendose)
        {
            // Detectar hacia dónde arrastra el jugador el ratón
            StartCoroutine(ArrastrarInput());
        }
    }

    System.Collections.IEnumerator ArrastrarInput()
    {
        Vector3 inicioRat = Input.mousePosition;

        // Esperamos a que el jugador mueva un poco el ratón
        while (Input.GetMouseButton(0) && !moviendose)
        {
            Vector3 diferencia = Input.mousePosition - inicioRat;

            // Si ha arrastrado más de 40 píxeles, decidimos dirección
            if (diferencia.magnitude > 40)
            {
                if (Mathf.Abs(diferencia.x) > Mathf.Abs(diferencia.y))
                {
                    if (diferencia.x > 0) IntentarMover(Vector2.right);
                    else IntentarMover(Vector2.left);
                }
                else
                {
                    if (diferencia.y > 0) IntentarMover(Vector2.up);
                    else IntentarMover(Vector2.down);
                }
                yield break; // Dejamos de escuchar el input
            }
            yield return null;
        }
    }

    void Update()
    {
        // Mueve visualmente el bloque hacia su destino
        if (moviendose)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
            if (Vector3.Distance(transform.position, destino) < 0.01f)
            {
                transform.position = destino; // Ajuste final preciso
                moviendose = false;
            }
        }
    }

    void IntentarMover(Vector2 direccion)
    {
        // 1. Preparamos el array de resultados
        RaycastHit2D[] resultados = new RaycastHit2D[1];

        // 2. Creamos un "Filtro de Contacto"
        // Unity necesita esto para traducir tu LayerMask a algo que rb.Cast entienda
        ContactFilter2D filtro = new ContactFilter2D();
        filtro.SetLayerMask(obstaculos); // Aquí metemos tu variable 'obstaculos'
        filtro.useTriggers = false;      // Importante: ignoramos triggers (como la meta) para no chocarnos con ellos

        // 3. Ahora sí funciona: pasamos el 'filtro' en el segundo argumento
        int choques = rb.Cast(direccion, filtro, resultados, tamañoPaso - 0.05f);

        if (choques == 0)
        {
            // Camino libre
            destino = (Vector2)transform.position + (direccion * tamañoPaso);
            moviendose = true;
        }
        else
        {
            Debug.Log("Bloqueado por: " + resultados[0].collider.name);
        }
    }
}