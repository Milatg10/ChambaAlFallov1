using UnityEngine;

using UnityEngine;

public class RatonPremio : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            // 1. Avisar al GameManager de la victoria
            if (GameManagerMila.instance != null)
            {
                GameManagerMila.instance.RatonRecogido();
            }

            // 2. Efecto de sonido (opcional) o partículas aquí

            // 3. Desaparecer
            Destroy(gameObject);
        }
    }
}