using UnityEngine;

public class SistemaAgarre : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaAgarre = 1.0f;
    public LayerMask capasAgarrables;
    public string tagObjeto = "Espejo";

    private GameObject objetoAgarrado;
    private Rigidbody2D rbEspejo; // Variable para recordar el RB del espejo
    private FixedJoint2D unionFisica;

    void Update()
    {
        // Solo intentamos agarrar si pulsamos espacio
        if (Input.GetKey(KeyCode.Space))
        {
            if (objetoAgarrado == null)
            {
                IntentarAgarrar();
            }
        }
        else
        {
            // Si soltamos espacio y teníamos algo, lo soltamos
            if (objetoAgarrado != null)
            {
                Soltar();
            }
        }
    }

    void IntentarAgarrar()
    {
        Collider2D[] colisiones = Physics2D.OverlapCircleAll(transform.position, distanciaAgarre, capasAgarrables);

        foreach (Collider2D col in colisiones)
        {
            if (col.CompareTag(tagObjeto) && col.attachedRigidbody != null)
            {
                objetoAgarrado = col.gameObject;
                CrearUnion(col.attachedRigidbody);
                break;
            }
        }
    }

    void CrearUnion(Rigidbody2D rbObjeto)
    {
        // Guardamos la referencia al Rigidbody del espejo
        rbEspejo = rbObjeto;

        // --- TRUCO DE MAGIA ---
        // Descongelamos la posición X e Y para poder moverlo
        // Solo dejamos congelada la rotación para que no ruede
        rbEspejo.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Creamos la unión física
        unionFisica = gameObject.AddComponent<FixedJoint2D>();
        unionFisica.connectedBody = rbObjeto;
        unionFisica.dampingRatio = 1;
        unionFisica.frequency = 0;
    }

    void Soltar()
    {
        // Destruimos la unión
        if (unionFisica != null)
        {
            Destroy(unionFisica);
        }

        // --- VUELTA A LA NORMALIDAD ---
        // Volvemos a congelar TODO para que nadie lo pueda empujar sin querer
        if (rbEspejo != null)
        {
            rbEspejo.constraints = RigidbodyConstraints2D.FreezeAll;
            rbEspejo = null; // Olvidamos el espejo
        }

        objetoAgarrado = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaAgarre);
    }
}