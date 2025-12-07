using UnityEngine;

public class RecogerPantalla : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool yaRecogido = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (ControlLaser.vitrinaAbierta && !yaRecogido)
            {
                Recoger();
            }
        }
    }

    void Recoger()
    {
        Debug.Log("¡Has cogido la pantalla!");

        ControlLaser.pantallaRecogida = true;

        yaRecogido = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }
}