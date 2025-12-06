using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugVida : MonoBehaviour
{
    public int vida = 3;
    public float daño = 10f; // Cuánto quita al tocar

    private Animator miAnimator;
    private bool estaMuerto = false;
    private Color colorOriginal; // Aquí guardaremos si es verde, amarillo, etc.

    void Start()
    {
        miAnimator = GetComponent<Animator>();
        colorOriginal = GetComponent<SpriteRenderer>().color;
    }

    // Para recibir daño de las balas (Trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaMuerto) return;

        if (collision.CompareTag("Weapon"))
        {
            RecibirDañoPublico();
            // Destruimos la bala para que no atraviese
            Destroy(collision.gameObject);
        }
    }

    // Para probar con clic
    private void OnMouseDown()
    {
        if (estaMuerto) return;
        RecibirDañoPublico();
    }

    public void RecibirDañoPublico()
    {
        vida--;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("RestaurarColor", 0.1f);

        if (vida <= 0)
        {
            StartCoroutine(MorirConEstilo());
        }
    }

    void RestaurarColor()
    {
        if(!estaMuerto) GetComponent<SpriteRenderer>().color = colorOriginal;
    }

    IEnumerator MorirConEstilo()
    {
        estaMuerto = true;
        miAnimator.Play("Bug_die");

        var movimiento = GetComponent<BugMovimiento>();
        if (movimiento != null) movimiento.enabled = false;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false; // Para que no estorbe

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}