using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugVida : MonoBehaviour
{
    public int vida = 3;
    private Animator miAnimator; // Para controlar las animaciones
    private bool estaMuerto = false; // Para que no le puedas pegar mientras muere

    void Start()
    {
        miAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaMuerto) return; // Si ya está muriendo, ignorar golpes

        if (collision.CompareTag("Player") || collision.CompareTag("Weapon"))
        {
            RecibirDañoPublico();
        }
    }

    private void OnMouseDown()
    {
        if (estaMuerto) return;
        RecibirDañoPublico();
    }

    public void RecibirDañoPublico()
    {
        vida--;
        
        // Opcional: Efecto de color rojo momentáneo
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("RestaurarColor", 0.1f); // Vuelve al color normal en 0.1s

        if (vida <= 0)
        {
            StartCoroutine(MorirConEstilo());
        }
    }

    void RestaurarColor()
    {
        if(!estaMuerto) GetComponent<SpriteRenderer>().color = Color.white; 
    }

    IEnumerator MorirConEstilo()
    {
        estaMuerto = true;
        
        // 1. Decirle al Animator que ponga la animación de muerte
        // Asegúrate de que escribiste el nombre EXACTO "Slime_Die" al crear el clip
        miAnimator.Play("Bug_die");

        // 2. Desactivar el movimiento para que no te persiga mientras muere
        // Intentamos buscar el script de movimiento y apagarlo
        var movimiento = GetComponent<BugMovimiento>();
        if (movimiento != null) movimiento.enabled = false;

        // 3. Quitar el collider para que no puedas chocar con el cadáver
        GetComponent<Collider2D>().enabled = false;

        // 4. Esperar lo que dura la animación (aprox 0.5 o 1 segundo)
        yield return new WaitForSeconds(0.8f);

        // 5. Adiós mundo
        Destroy(gameObject);
    }
}